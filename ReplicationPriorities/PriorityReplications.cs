using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DBAccess;
using Logs;

namespace ReplicationPriorities
{
    [XmlRoot("PriorityReplications")]
    public class PriorityReplications
    {
        [XmlElement("Priority")]
        public List<Priority> Priorities;

        private bool _PriorityListVerified = false;

        public Boolean PriorityListVerified
        {
            get { return _PriorityListVerified;  }
        }

        public PriorityReplications()
        {
            Priorities = new List<Priority>();
        }

        public void VerifyPriorities(DataAccess da, LogWriter log)
        {
            if (Priorities != null)
            {
                //Remove Priorities missing important information
                for (int i = Priorities.Count - 1; i >= 0; i--)
                {
                    if (Priorities[i].HasAllSettings == false)
                    {
                        log.WriteError(string.Format("Removed Priority ({0}), missing information.", Priorities[i].Name));
                        Priorities.RemoveAt(i);
                    }       
                }

                //Remove Priorities that don't have a valid SQL Script.
                if(da.Connect())
                {
                    for (int i = Priorities.Count - 1; i >= 0; i--)
                    {
                        log.WriteInfo(string.Format("Testing Priority ({0}).", Priorities[i].Name));

                        try
                        {
                            da.SelectWithError(Priorities[i].RunQuery(DateTime.Now));
                        }
                        catch(System.Data.SqlClient.SqlException sx)
                        {
                            log.WriteError(string.Format("Priority failed validation. {0}.", sx.ToString()));
                            log.WriteInfo(string.Format("Priority ({0}) removed.", Priorities[i].Name));
                            Priorities.RemoveAt(i);
                        }
                    }

                    _PriorityListVerified = true;
                } 
                else
                {
                    log.WriteError("Failed to connect to the database to verify priorities. Will try again later.");
                }
            }
        }

        public void SavePriorities()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PriorityReplications));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            StringBuilder sb = new StringBuilder();
            XmlWriter xm = XmlWriter.Create("Priorities.ini", new XmlWriterSettings() { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Auto, Indent = false });
            serializer.Serialize(xm, this, ns);
            xm.Close();
        }
    }
}
