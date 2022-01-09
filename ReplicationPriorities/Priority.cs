using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using DBAccess;

namespace ReplicationPriorities
{
    //[Serializable()]
    public class Priority
    {
        [XmlAttribute("Name")]
        public String Name;
        [XmlAttribute("Frequency")]
        public Int32 Frequency;
        [XmlIgnore]
        private string _Query = "";
        [XmlAttribute(AttributeName ="Query")]
        public String Query
        {
            set { _Query = value; }
            get { return _Query; }
        }

        public string RunQuery(DateTime Now)
        {
            return _Query.Replace("@LastRunTime", "'" + LastRunTime.AddSeconds(1).ToString("yyyy-MM-dd HH:mm:ss") + "'").Replace("@Now","'" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
        }

        [XmlIgnore]
        public Boolean HasAllSettings
        {
            get
            {
                if (Name != "" && Frequency > 0 && _Query != "")
                    return true;
                else
                    return false;
            }
        }
        [XmlAttribute("LastRunTime")]
        public DateTime LastRunTime;
        [XmlIgnore]
        public DateTime NextRunTime
        {
            get { return LastRunTime.AddMinutes(Frequency); }
        }

        public Priority()
        {
            Frequency = 1;
            LastRunTime = DateTime.Now;
        }

        public string ExecutePriority(DataAccess DB)
        {
            if(DB.Connect())
            {
                DateTime ExecutionTime = DateTime.Now;

                DataSet ds = DB.Select(RunQuery(ExecutionTime));

                if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
                {
                    LastRunTime = ExecutionTime;
                }
                else if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LastRunTime = ExecutionTime;
                    return ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }             
            }

            //No results found
            return "";
        }

    }
}
