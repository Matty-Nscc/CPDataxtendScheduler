using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CPDataxtendCommands;
using DBAccess;
using ReplicationPriorities;

namespace CPDataxtendPriorityClient
{
    [XmlRoot("Settings")]
    public class Settings
    {
        //Database Settings
        [XmlElement("DBSettings")]
        public DataAccess DB;

        //Server
        [XmlElement("Server")]
        public Client client;

        public Settings()
        {
            DB = new DataAccess();
            client = new Client();
        }

        //PriorityReplication
        [XmlIgnore]
        public PriorityReplications PriorityList = new PriorityReplications();
    }
}
