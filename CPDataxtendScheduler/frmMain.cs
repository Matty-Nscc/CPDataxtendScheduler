using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using CPDataxtendCommands;
using Logs;
using ReplicationPriorities;

namespace CPDataxtendScheduler
{
    public partial class frmMain : Form
    {
        Thread ttelnet = null;
        Thread tserver = null;
        Thread ttelnetreder = null;
        Telnet telnet = null;
        Settings settings = null;
        List<ReplicationStatus> ReplicationStatuses = new List<ReplicationStatus>();
        List<Site> SiteAttemptReplication = new List<Site>();
        LogWriter log;
        private bool SuppressMessages = false;

        public frmMain(String[] args)
        {
            InitializeComponent();

            for(int i = 0; i < args.Length; i++)
                switch (args[i]) 
                {
                    case "-SuppressMessages":
                        SuppressMessages = true;
                        break;
                    default:
                        break;
                }

            //Close the program if one is already opened.
            if (Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                if(!SuppressMessages)
                    MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            log = new LogWriter(Application.StartupPath + @"\Logs");
            log.WriteInfo("Starting the " + Application.ProductName + ".");

            //Load Settings
            if (!LoadSettings())
            {
                if(!SuppressMessages)
                    tsmiOptions_Click(null, null);
                this.Close();
            } 
            else
            {
                SetOptions();
                ConnectToTelnet();
                SetNextPriorityTimers();
            }
        }

        private void SetOptions()
        {
            nudMaxSitesToReplicate.Value = settings.options.DefaultSites2Replicate;
            chkReplicationDetails.Checked = settings.options.DefaultReplicateDetails;
        }

        private void Server_ReceivedPriorityReplication(object sender, EventArgs e)
        {
            int index = CPDataxtendCommands.Site.FindSiteBySiteID(settings.Sites, sender.ToString());
            UpdateReplicationList(index, false);
        }

        private void SetNextPriorityTimers()
        {
            //There are priorities
            if (settings.Priorities.PriorityListVerified && settings.Priorities.Priorities.Count > 0)
            {
                //Sort list to find next runtime if more then 1 priority.
                if(settings.Priorities.Priorities.Count > 1)
                    settings.Priorities.Priorities.Sort(delegate (Priority x, Priority y)
                    {
                        return x.NextRunTime.CompareTo(y.NextRunTime);
                    });

                //Run in the future.
                if ((settings.Priorities.Priorities[0].NextRunTime - DateTime.Now).TotalSeconds > 0)
                {
                    tPriority.Interval = (int)(settings.Priorities.Priorities[0].NextRunTime - DateTime.Now).TotalSeconds * 100;
                    tPriority.Enabled = true;
                }
                //Time was missed or it should be 
                else
                {
                    tPriority.Enabled = false;
                    tPriority_Tick(null, null);
                }
            }
        }

        private void Settings_SitesLoaded(object sender, EventArgs e)
        {
            lbNextSite.Items.Clear();

            foreach(Site site in settings.Sites)
                lbNextSite.Items.Add(site.SiteName);
        }

        private void Telnet_OnMessageReceived(object sender, EventArgs e)
        {
            if(txtTelnet.InvokeRequired)
            {
                try
                {
                    this.BeginInvoke(new MethodInvoker(delegate () { ReadTelnet(sender.ToString()); }));
                }
                catch (System.InvalidOperationException) { }
            }
            else
                ReadTelnet(sender.ToString());
        }

        private void ReadTelnet(string msg)
        {
            if(chkReplicationDetails.Checked)
            {
                txtTelnet.Text += msg + Environment.NewLine;
                txtTelnet.Select(txtTelnet.Text.Length, 0);
                txtTelnet.ScrollToCaret();
                //log.WriteInfo(msg);
            }
            
            //Status of a replication cycle
            ReplicationStatus rs = new ReplicationStatus();

            //Telnet closing
            if(msg == "Disconnected from telnet")
            {
                ReplicationStatuses.Clear();
                SiteAttemptReplication.Clear();
                dgvReplicationProgress.Rows.Clear();
                log.WriteError("Disconnecting from telnet.");
            }
            //The connection to the telnet was lost suddenly. Try to reconnect
            else if(msg == "crashed")
            {
                log.WriteInfo("The telnet crashed. Attempting to reconnect to it.");
                
                if (!ConnectToTelnet())
                    tReconnect.Enabled = true;
            }
            //This is the start of a replication but we don't know for what site.
            else if(rs.IsStartOfReplication(msg))
            {
                log.WriteInfo(string.Format("Starting of replication node received: {0}.", rs.PDRESessionID));
                ReplicationStatuses.Add(rs);
            }
            //This is the status of a replication cycle
            else if (rs.IsReplicationStatus(msg, settings))
            {
                bool NewSession = true;

                //Is the replication on file?
                int rsIndex = ReplicationStatus.FindReplicationStatusBySessionID(ReplicationStatuses, rs.SessionID);

                //Replication status on file.
                if(rsIndex > -1)
                {
                    if (rs.SiteName != String.Empty)
                    {
                        if(ReplicationStatuses[rsIndex].Status == "NEWREPLICATION")
                        {
                            log.WriteInfo(string.Format("SessionID: {0} is {1}. New Replication.", new object[] { ReplicationStatuses[rsIndex].PDRESessionID, rs.SiteName }));
                        }

                        rs.SetPreviousStatus(ReplicationStatuses[rsIndex].Status);
                        rs.PDRESessionID = ReplicationStatuses[rsIndex].PDRESessionID;
                        ReplicationStatuses[rsIndex] = rs;

                        //Remove off the waiting list if it was on it
                        SiteAttemptReplication.Remove(new Site(rs.SiteName));
                    }
                } 
                //Replication status not on file and isn't completed.
                else if(rs.Status != "COMPLETE")
                {
                    ReplicationStatuses.Add(rs);
                }

                //Check to see if the session is already in the grid.
                for (int i = 0; i < dgvReplicationProgress.Rows.Count; i++)
                {
                    if (dgvReplicationProgress.Rows[i].Cells[0].Value.ToString() == rs.SessionID)
                    {
                        if (rs.Status == "COMPLETE")
                        {
                            try
                            {
                                dgvReplicationProgress.Rows.RemoveAt(i);
                                int index = ReplicationStatus.FindReplicationStatusBySessionID(ReplicationStatuses, rs.SessionID);
                                
                                if(index > -1)
                                    ReplicationStatuses.RemoveAt(index);
                            }
                            catch(IndexOutOfRangeException iex)
                            {
                                log.WriteError(string.Format("Grid Rows: {0}, index: {1} ", new object[] { dgvReplicationProgress.Rows.Count, i }));
                                log.WriteError(string.Format("Row Statuses Count: {0}", new object[] { ReplicationStatuses.Count }));
                                log.WriteError(iex.ToString());
                            }

                            //Remove off the waitlist for sites that don't replicate
                            if (rs.SiteName == "")
                            {
                                int index = settings.FindSiteBySiteName(SiteAttemptReplication[0].SiteName);
                                settings.Sites[index].FailedReplicationCount += 1;

                                if(SiteAttemptReplication.Count > 0)
                                {
                                    SiteAttemptReplication.RemoveAt(0);
                                    log.WriteInfo("Removing a site off the site attempting replication list.");
                                }
                                
                                if(settings.Sites[index].FailedReplicationCount >= 4)
                                {
                                    log.WriteInfo(string.Format("Site ({0}) has failed to replicate 3 times.", settings.Sites[index].SiteName));
                                    UpdateReplicationList(index, true);
                                }     
                            } 
                            else
                            {
                                int index = settings.FindSiteBySiteName(rs.SiteName);

                                //Query the database to figure out if the site successfully replicated or not.
                                if (settings.DB.Connect() && index < settings.Sites.Count)
                                {
                                    Thread.Sleep(100);
                                    DataSet ds = settings.DB.Select(string.Format("SELECT Status FROM pduser.PDAudit WHERE SessionID = '{0}'", rs.PDRESessionID));

                                    //Found the audit
                                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                    {
                                        switch (ds.Tables[0].Rows[0].ItemArray[0].ToString())
                                        {
                                            //Success
                                            case "0":
                                                settings.Sites[index].FailedReplicationCount = 0;
                                                log.WriteInfo(string.Format("Site ({0}) has successfully replicated in {1} seconds.", settings.Sites[index].SiteName, rs.ElapsedTime));

                                                //No changes have been made since the replication started
                                                if (!settings.Sites[index].ChangesMade)
                                                    UpdateReplicationList(index, true);
                                                //Changes were made. Keep current spot so it will replicate again.
                                                else
                                                    settings.Sites[index].ChangesMade = false;
                                                break;
                                            //Failed
                                            case "1":
                                                settings.Sites[index].FailedReplicationCount += 1;
                                                log.WriteInfo(string.Format("Site ({0}) has failed replication in {1} seconds.", settings.Sites[index].SiteName, rs.ElapsedTime));

                                                if (settings.Sites[index].FailedReplicationCount >= 4)
                                                    UpdateReplicationList(index, true);
                                                break;
                                                //User Cancelled
                                                //case "4":
                                                //settings.Sites[index].FailedReplicationCount += 1;
                                                //break;
                                        }
                                    }
                                    //Didn't find it. Counts as a failed replication
                                    else
                                    {
                                        settings.Sites[index].FailedReplicationCount += 1;

                                        if (settings.Sites[index].FailedReplicationCount >= 4)
                                            UpdateReplicationList(index, true);
                                    }

                                    settings.DB.Disconnect();
                                }
                                else
                                    MessageBox.Show("Failed to connect to the database.");
                            }

                            //Start replication for a new site
                            ReplicationNewSiteTimerChange();

                            break;
                        }
                        else if(rs.PreviousStatus == "NEWREPLICATION")
                        {
                            dgvReplicationProgress.Rows[i].Cells["CSite"].Value = rs.SiteName;
                            dgvReplicationProgress.Rows[i].Cells["CHost"].Value = rs.Host;
                            dgvReplicationProgress.Rows[i].Cells["CElapsedTime"].Value = rs.ElapsedTime;
                            dgvReplicationProgress.Rows[i].Cells["CPercent"].Value = rs.Percent;

                            //Remove it from the waiting list
                            int index = CPDataxtendCommands.Site.FindSiteBySiteName(SiteAttemptReplication, rs.SiteName);

                            if(index > -1)
                                SiteAttemptReplication.RemoveAt(index);
                        }
                        else
                        {
                            dgvReplicationProgress.Rows[i].Cells["CElapsedTime"].Value = rs.ElapsedTime;

                            if (dgvReplicationProgress.Rows[i].Cells["CPercent"].Value != (object)rs.Percent)
                                dgvReplicationProgress.Rows[i].Cells["CPercent"].Value = rs.Percent;   
                        }

                        NewSession = false;
                        break;
                    }
                }

                //New Session
                if (NewSession && rs.Status != "COMPLETE")
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.Cells.AddRange(
                        new DataGridViewTextBoxCell() { Value = rs.SessionID },
                        new DataGridViewTextBoxCell() { Value = rs.SiteName },
                        new DataGridViewTextBoxCell() { Value = rs.ElapsedTime },
                        new DataGridViewTextBoxCell() { Value = rs.Host },
                        new DataGridViewProgressCell() { Value = rs.Percent }
                        );

                    //Figure out if its already in the datagridview or if a new line has to be added.
                    dgvReplicationProgress.Rows.Add(dr);
                }
            }

            lblStats.Text = String.Format("Sites Replicating: {0} Sites Waiting: {1}", ReplicationStatuses.Count, SiteAttemptReplication.Count);
        }

        private void ReplicateSites()
        {
            //Clear out old sites
            for (int i = SiteAttemptReplication.Count - 1; i > -1; i--)
            {
                if ((DateTime.Now - SiteAttemptReplication[i].LastAttemptReplicationDate).TotalSeconds > 120)
                {
                    //Increment failed Replication Count.
                    settings.Sites[settings.Sites.FindIndex(delegate (Site site) { return site.SiteID.Equals(SiteAttemptReplication[i].SiteID); })].FailedReplicationCount += 1;
                    log.WriteInfo(string.Format("Site ({0}) has been removed from the Sites attempting to replicate. Been longer then 2 minutes.", SiteAttemptReplication[i].SiteName));
                    SiteAttemptReplication.RemoveAt(i);
                }
            }

            //How many people are replicating?
            if (ReplicationStatuses.Count < nudMaxSitesToReplicate.Value)
            {
                log.WriteInfo("Looking for a new site to replicate.");
                //Find the next site to replicate
                for(int i = 0; i < settings.Sites.Count; i++)
                {
                    //Site id is not currently replicating
                    if(ReplicationStatuses.FindIndex(delegate (ReplicationStatus frs) { return frs.SiteName.Equals(settings.Sites[i].SiteName); }) == -1)
                    {
                        //Site is not waiting to be replicated
                        if(SiteAttemptReplication.FindIndex(delegate (Site s) { return s.SiteName.Equals(settings.Sites[i].SiteName); }) == -1)
                        {
                            settings.Sites[i].LastAttemptReplicationDate = DateTime.Now;

                            //Add to the list of sites waiting to replicate
                            SiteAttemptReplication.Add(settings.Sites[i]);

                            this.BeginInvoke(new MethodInvoker(delegate () { telnet.WriteMessage("start " + settings.Dataxtender.PDName + " " + settings.Sites[i].SiteID.ToString()); }));
                            break;
                        }
                    }
                }            
                
                Thread.Sleep(100);
            }
        }

        private void UpdateReplicationList(int index, bool bottom)
        {
            if(index > -1)
            {
                //Top of the list
                if (!bottom)
                {
                    settings.Sites.Insert(0, settings.Sites[index]);
                    settings.Sites.RemoveAt(index + 1);
                }     
                //Bottom of the list
                else
                {
                    settings.Sites.Add(settings.Sites[index]);
                    settings.Sites.RemoveAt(index);
                }
                
                Settings_SitesLoaded(null, null);
            }      
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(delegate () { telnet.WriteMessage("quit"); }));

            if(chkPriorityReplications.Checked)
                settings.server.Disconnect();
        }

        private void dgvReplicationProgress_SelectionChanged(object sender, EventArgs e)
        {
            dgvReplicationProgress.ClearSelection();
        }

        private void nudMaxSitesToReplicate_ValueChanged(object sender, EventArgs e)
        {
            ReplicationNewSiteTimerChange();
        }

        private void ReplicationNewSiteTimerChange()
        {
            if (ReplicationStatuses.Count < nudMaxSitesToReplicate.Value && telnet != null && telnet.GetStatus())
            {
                if (!tReplicateNewSite.Enabled)
                {
                    log.WriteInfo("Enabled looking for new sites to replicate.");
                    tReplicateNewSite.Enabled = true;
                }      
            }
            else
            {
                if (tReplicateNewSite.Enabled)
                {
                    log.WriteInfo("Disabled looking for new sites to replicate.");
                    tReplicateNewSite.Enabled = false;
                }   
            }
        }

        private void tReplicateNewSite_Tick(object sender, EventArgs e)
        {
            ReplicateSites();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTelnet.Text = "";
        }

        private void tsmiOptions_Click(object sender, EventArgs e)
        {
            frmOptions frm = new frmOptions(settings, log);

            //Options were saved. Reload the screen
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //Load the file
                if (!LoadSettings())
                    this.Close();
                else
                {
                    if (!ConnectToTelnet())
                        tReconnect.Enabled = true;
                }
            }
        }

        private bool LoadSettings()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Settings));

                using (var reader = XmlReader.Create("Settings.ini"))
                {
                    settings = (Settings)serializer.Deserialize(reader);
                    log.WriteInfo("Settings have been read.");

                    if(!settings.DB.HasAllSettings())
                    {
                        log.WriteError("Settings file is missing some database settings that are required.");
                        return false;
                    }

                    if (!settings.Dataxtender.HasAllSettings())
                    {
                        log.WriteError("Settings file is missing some dataxtend settings that are required.");
                        return false;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                log.WriteError("The Settings.ini file does not exist.");
                return false;
            }
            catch (Exception ex)
            {
                log.WriteError(ex.ToString());
                return false;
            }

            //Load Priorities if they exist
            try
            {
                var serializer = new XmlSerializer(typeof(PriorityReplications));

                using (var reader = XmlReader.Create("Priorities.ini"))
                {
                    settings.Priorities = (PriorityReplications)serializer.Deserialize(reader);
                    log.WriteInfo(string.Format("Priorities were provided. {0}", settings.Priorities.Priorities.Count));
                    settings.Priorities.VerifyPriorities(settings.DB, log);
                }
            }
            catch (System.IO.FileNotFoundException) { log.WriteInfo("No Priorities.ini file exists."); }
            catch (Exception ex)
            {
                log.WriteError("An error occurred while trying to read the Priorities.ini.");
                log.WriteError(ex.ToString());
            }

            settings.SitesLoaded += Settings_SitesLoaded;
            settings.LoadSites();
            return true;
        }

        public bool ConnectToTelnet()
        {
            Thread.Sleep(100);

            if (telnet != null)
            {
                log.WriteInfo("Disconnecting from telnet.");
                telnet.WriteMessage("quit");

                do
                {
                    Thread.Sleep(100);
                } while (telnet.GetStatus());

                telnet.OnMessageReceived -= Telnet_OnMessageReceived;
                ReplicationNewSiteTimerChange();
            }

           try
            {
                log.WriteInfo("Attempting to connecting to telnet.");
                telnet = new Telnet(new IPEndPoint(settings.Dataxtender.IPAddress, settings.Dataxtender.Port));
                telnet.OnMessageReceived += Telnet_OnMessageReceived;
            }
            catch (System.Net.Sockets.SocketException se)
            {
                log.WriteError("Failed to connect to the telnet.");
                log.WriteError(se.ToString());
                //Failed to connect to telnet
                txtTelnet.Text += string.Format("Failed to connect to telnet. Reconnecting in {0} seconds.\r\n", tReconnect.Interval / 1000);
                tReconnect.Enabled = true;
                telnet = null;
            }

            //Connected to telnet
            if (telnet != null)
            {
                log.WriteInfo("Connected to telnet.");
                ttelnet = new Thread(telnet.ReadWrite);
                ttelnet.IsBackground = true;
                ttelnet.Start();
                ReplicationNewSiteTimerChange();
            }
            else
                return false;

            return true;
        }

        private void tPriority_Tick(object sender, EventArgs e)
        {
            //Execute the priority at the beginning of the list.
            log.WriteInfo(string.Format("Starting Priority: {0}.", settings.Priorities.Priorities[0].Name));

            try
            {
                string SiteID = settings.Priorities.Priorities[0].ExecutePriority(settings.DB);

                //Updated the xml file
                XmlSerializer serializer = new XmlSerializer(typeof(PriorityReplications));
                Stream fs = new FileStream("Priorities.ini", FileMode.Create);
                XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
                serializer.Serialize(writer, settings.Priorities);
                writer.Close();

                //SiteID found, move to the top of the list.
                if (SiteID != "")
                {
                    int index = settings.FindSiteBySiteID(SiteID);

                    if(index == -1)
                        log.WriteError(string.Format("Site ID: {0}, not on file.", SiteID));
                    else
                    {
                        //If site is already replicating mark it so the system knows not to move it to the bottom of the list. (Do one more replication)
                        index = CPDataxtendCommands.Site.FindSiteBySiteID(settings.Sites, SiteID);

                        if(index != -1)
                        {
                            settings.Sites[index].ChangesMade = true;
                        }                  

                        UpdateReplicationList(index, false);
                    }                          
                }

                SetNextPriorityTimers();
            }
            catch (Exception ex)
            {
                log.WriteError(ex.ToString());
            }
        }

        private void tReconnect_Tick(object sender, EventArgs e)
        {
            if (ConnectToTelnet())
                tReconnect.Enabled = false;
        }

        private bool PriorityReplicationsAction = true;

        private void chkPriorityReplications_CheckedChanged(object sender, EventArgs e)
        {
            //Start server
            if(PriorityReplicationsAction)
                if(chkPriorityReplications.Checked)
                {
                    chkPriorityReplications.Enabled = false;
                    settings.server.SetLogWriter(log);
                    settings.server.ReceivedPriorityReplication += Server_ReceivedPriorityReplication;
                    settings.server.StatusChange += Server_StatusChange;
                    tserver = new Thread(settings.server.ReadWrite);
                    tserver.IsBackground = true;
                    tserver.Start();
                }
                else
                {
                    chkPriorityReplications.Enabled = false;
                    settings.server.Disconnect();
                } 
        }

        private void Server_StatusChange(object sender, EventArgs e)
        {
            if(chkPriorityReplications.InvokeRequired)
            {
                chkPriorityReplications.Invoke(new MethodInvoker(delegate
                {
                    chkPriorityReplications.Enabled = true;
                }));
            }
            else
                chkPriorityReplications.Enabled = true;

            PriorityReplicationsAction = (bool)sender;

            if (!PriorityReplicationsAction)
                chkPriorityReplications.Checked = false;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.WriteInfo("Starting the Dataxtender.");
            Services.StartDataxtendServices();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.WriteInfo("Stopping the Dataxtender.");
            Services.StopDataxtendServices();
        }

        private void dataxtendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            log.WriteInfo("Checking the dataxtend service status.");

            if(Services.CheckServiceStatus(log, "PDRESvc"))
            {
                startToolStripMenuItem.Enabled = false;
                stopToolStripMenuItem.Enabled = true;
            }
            else
            {
                startToolStripMenuItem.Enabled = true;
                stopToolStripMenuItem.Enabled = false;
            }
        }

        
    }
}
