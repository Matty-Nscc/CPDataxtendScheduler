using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Logs;
using DBAccess;

namespace CPDataxtendScheduler
{
    public partial class frmOptions : Form
    {
        private Settings settings;
        private LogWriter log;

        public frmOptions(Settings settings, LogWriter log)
        {
            InitializeComponent();

            if (settings == null)
                this.settings = new Settings();
            else
                this.settings = settings;

            this.log = log;
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            txtServer.Text = settings.DB.Server;
            txtUserName.Text = settings.DB.User;

            try
            {
                mtxtPassword.Text = Encryption.ToDecryptMD5(settings.DB.Password, null, true);
            }
            //Not encrypted of broken
            catch (Exception) { }

            cbDatabase.Text = settings.DB.Database;
            chkWindowsAuthentication.Checked = settings.DB.WindowsAuthentication;

            txtDataxtendIP.Text = settings.Dataxtender.IP;
            txtDataxtendPort.Text = settings.Dataxtender.Port.ToString();
            txtPDName.Text = settings.Dataxtender.PDName;

            nudDefaultSites2Rep.Value = settings.options.DefaultSites2Replicate;
            chkReplicatioonDetails.Checked = settings.options.DefaultReplicateDetails;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            settings.DB.Server = txtServer.Text;
            settings.DB.Database = cbDatabase.Text;
            settings.DB.User = txtUserName.Text;
            settings.DB.Password = Encryption.ToEncryptMD5(mtxtPassword.Text, null, true);

            settings.Dataxtender.IP = txtDataxtendIP.Text;
            settings.Dataxtender.Port = Convert.ToInt32(txtDataxtendPort.Text);
            settings.Dataxtender.PDName = txtPDName.Text;

            settings.options.DefaultSites2Replicate = Convert.ToInt32(nudDefaultSites2Rep.Value);
            settings.options.DefaultReplicateDetails = chkReplicatioonDetails.Checked;

            //Write to file
            var serializer = new XmlSerializer(typeof(Settings));

            using (var writer = XmlWriter.Create("Settings.ini"))
            {
                settings.DB.Password = Encryption.ToEncryptMD5(mtxtPassword.Text, null, true);
                serializer.Serialize(writer, settings);
                //log.WriteInfo(string.Format("Priorities were provided. {0}", settings.Priorities.Priorities.Count));
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            DataAccess da = null;
            
            if(chkWindowsAuthentication.Checked)
                da = new DataAccess(txtServer.Text, cbDatabase.Text);
            else
                da = new DataAccess(txtServer.Text, cbDatabase.Text, txtUserName.Text, mtxtPassword.Text);

            if (da.TestConnection())
                MessageBox.Show("Connected to the database successfully.", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Failed to connect to the database.", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
