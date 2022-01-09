using System;
using System.Collections.Generic;

namespace CPDataxtendCommands
{
    public class Site
    {
        private string _SiteID;
        private string _SiteName;
        private string _SiteHost;
        private DateTime _LastReplicationDate = DateTime.Now;
        private DateTime _LastAttemptReplicationDate;
        private int _FailedReplicationCount;
        private bool _ChangesMade = false; //Changes have been made so it will need to replicate again.

        public Site() { }

        public Site(string SiteName)
        {
            _SiteName = SiteName;
        }
        public Site(string SiteID, string SiteName)
        {
            _SiteID = SiteID;
            _SiteName = SiteName;
        }
        public Site(string SiteID, string SiteName, string SiteHost)
        {
            _SiteID = SiteID;
            _SiteName = SiteName;
            _SiteHost = SiteHost;
        }
        public Site(string SiteName, DateTime LastAttemptReplicationDate)
        {
            _SiteName = SiteName;
            _LastAttemptReplicationDate = LastAttemptReplicationDate;
        }

        public String SiteID
        {
            set
            {
                _SiteID = value;
            }

            get
            {
                return _SiteID;
            }
        }

        public String SiteName
        {
            set
            {
                _SiteName = value;
            }
            get
            {
                return _SiteName;
            }
        }

        public String SiteHost
        {
            set
            {
                _SiteHost = value;
            }
            get
            {
                return _SiteHost;
            }
        }

        public DateTime LastReplicationDate
        {
            set
            {
                _LastReplicationDate = value;
            }
            get
            {
                return _LastReplicationDate;
            }
        }

        public DateTime LastAttemptReplicationDate
        {
            set { _LastAttemptReplicationDate = value; }
            get { return _LastAttemptReplicationDate; }
        }

        public Int32 FailedReplicationCount
        {
            set
            {
                _FailedReplicationCount = value;
            }

            get
            {
                return _FailedReplicationCount;
            }
        }

        public Boolean ChangesMade
        {
            set
            {
                _ChangesMade = value;
            }
            get
            {
                return _ChangesMade;
            }
        }

        public static int FindSiteBySiteID(List<Site> sites, string siteID)
        {
            return sites.FindIndex(delegate (Site site) { return site.SiteID.Equals(siteID); });
        }

        public static int FindSiteBySiteName(List<Site> sites, string siteName)
        {
            return sites.FindIndex(delegate (Site site) { return site.SiteName.Equals(siteName); });
        }

        public static string FindSiteNameBySiteID(List<Site> sites, string siteID)
        {
            int index = sites.FindIndex(delegate (Site site) { return site.SiteID.Equals(siteID); });

            if (index != -1)
                return sites[index].SiteName;
            else
                return "";
        }

    }
}
