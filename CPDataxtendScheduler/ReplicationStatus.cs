using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPDataxtendCommands;

namespace CPDataxtendScheduler
{
    class ReplicationStatus
    {
        private string _PDRESessionID = "";
        private string _SessionID = "";
        //private string _PDName = "";
        private string _SiteName = "";
        private string _Host = "";
        private string _Status = "";
        private string _PreviousStatus = "";
        private int _Percent = 0;
        private int _ElapsedTime = 0;

        public String PDRESessionID { set { _PDRESessionID = value; } get { return _PDRESessionID; } }
        public String SessionID { get { return _SessionID; } }
        //public String PDName { get { return _PDName; } }
        public String SiteName { get { return _SiteName; } }
        public String Host { get { return _Host; } }
        public String Status { get { return _Status; } }
        public String PreviousStatus { get { return _PreviousStatus; } }
        public Int32 Percent { get { return _Percent; } }
        public Int32 ElapsedTime { get { return _ElapsedTime; } }

        public ReplicationStatus() { }

        public void SetPreviousStatus(String Status) 
        {
            _PreviousStatus = Status;
        }

        public bool IsStartOfReplication(string message)
        {
            if(message.StartsWith("+MAP "))
            {
                string[] data = message.Split(',');

                _SessionID = data[0].Split(' ')[1];
               // _PDName = data[1].Trim();
                _PDRESessionID = data[1].Trim();
                _Status = "NEWREPLICATION";
                return true;
            }

            return false;
        }

        public bool IsReplicationStatus(string message, Settings settings)
        {
            if(message.StartsWith("+STAT "))
            {
                string[] data = message.Split(',');
                //_PDName = data[1].Trim();

                //if (settings.Dataxtender.PDName == PDName)
                //{
                    _SessionID = data[0].Split(' ')[1];
                    _SiteName = data[4].Trim();
                    _Host = data[5].Trim();
                    _PreviousStatus = _Status;
                    _Status = data[7].Trim();
                    _Percent = Convert.ToInt32(data[8].Trim());
                    _ElapsedTime = Convert.ToInt32("0"+data[11].Trim().Replace("\n", ""));
                    return true;
               // }
            }

            return false;
        }

        public void UpdateLastReplication(ref List<Site> Sites)
        {
            for (int i = 0; i < Sites.Count; i++)
            {
                if (Sites[i].SiteName == SiteName && Sites[i].SiteHost == Host)
                {
                    Sites[i].LastReplicationDate = DateTime.Now;
                    break;
                }
            }
        }

        public static int FindReplicationStatusBySessionID(List<ReplicationStatus> replicationStatuses, string SessionID)
        {
            return replicationStatuses.FindIndex(delegate (ReplicationStatus _rs) { return _rs.SessionID.Equals(SessionID); });
        }

        public static int FindReplicationStatusByPDRESessionID(List<ReplicationStatus> replicationStatuses, string SessionID)
        {
            return replicationStatuses.FindIndex(delegate (ReplicationStatus _rs) { return _rs.PDRESessionID.Equals(SessionID); });
        }

        public static int FindReplicationStatusBySiteName(List<ReplicationStatus> replicationStatuses, string SiteName)
        {
            return replicationStatuses.FindIndex(delegate (ReplicationStatus _rs) { return _rs.SiteName.Equals(SiteName); });
        }

    }
}
