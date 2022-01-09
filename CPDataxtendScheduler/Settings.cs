using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using CPDataxtendCommands;
using DBAccess;
using ReplicationPriorities;

namespace CPDataxtendScheduler
{
    [XmlRoot("Settings")]
    public class Settings
    {
        //Database Settings
        [XmlElement("DBSettings")]
        public DataAccess DB;

        public event EventHandler SitesLoaded;

        //Dataxtend
        [XmlElement("Dataxtend")]
        public Dataxtend Dataxtender;

        //Sites
        [XmlIgnore]
        public List<Site> Sites;

        //Server
        [XmlElement("Server")]
        public Server server;

        //Options
        [XmlElement("Options")]
        public Options options;

        public Settings()
        {
            DB = new DataAccess();
            Dataxtender = new Dataxtend();
            Sites = new List<Site>();
            server = new Server();
            options = new Options();
        }

        public bool LoadSites()
        {
            //Connected to the database.
            if (DB.Connect())
            {
                DataSet ds = DB.Select("SELECT S.SiteID, S.NameStr, S.NetAddr, dbo.fnBase36ToDateTime(L.LastLnk) AS 'LastReplication', CASE L.LastAtt WHEN L.LastLnk THEN 'Successful' ELSE 'Check log files' END AS 'ReplicationStatus' FROM pduser.dSite S LEFT JOIN pduser.dSiteLnk L ON S.SiteID = L.Partner WHERE S.Condemn IS NULL And S.Spine = 1");

                //Table existed
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //Empty the old list of sites
                        Sites.Clear();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Sites.Add(new Site()
                            {
                                SiteID = dr.ItemArray[0].ToString(),
                                SiteName = dr.ItemArray[1].ToString(),
                                SiteHost = dr.ItemArray[2].ToString(),
                                LastReplicationDate = Convert.ToDateTime(dr.ItemArray[3])
                            });
                        }

                        //Sort List by Last Replication Date
                        Sites.Sort(delegate (Site x, Site y)
                        {
                            return x.LastReplicationDate.CompareTo(y.LastReplicationDate);
                        });

                        SitesLoaded(null, null); 
                    }

                    return true;
                }

                DB.Disconnect();
            }

            return false;
        }

        public int FindSiteBySiteID(string SiteID)
        {
            return Sites.FindIndex(delegate (Site site) { return site.SiteID.Equals(SiteID); }); ;
        }

        public int FindSiteBySiteName(string SiteName)
        {
            return Sites.FindIndex(delegate (Site site) { return site.SiteName.Equals(SiteName); }); ;
        }

        //PriorityReplication
        [XmlIgnore]
        public PriorityReplications Priorities = new PriorityReplications();
    }
}
