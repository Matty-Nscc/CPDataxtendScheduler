using System;
using System.Net;
using System.Xml.Serialization;

namespace CPDataxtendScheduler
{
    [XmlRoot("Dataxtend")]
    public class Dataxtend
    {
        private string __IP;

        [XmlElement(ElementName = "IP", IsNullable = false)]
        public String IP
        {
            set {
                setIP(value);
                __IP = value;
            }

            get { return __IP; }
        }

        [XmlIgnore]
        public IPAddress IPAddress;

        public void setIP(string ip)
        {
            if (!IPAddress.TryParse(ip, out IPAddress))
            {
                IPHostEntry iphost = System.Net.Dns.GetHostEntry(ip);
                IPAddress[] addresses = iphost.AddressList;

                for (int i = addresses.Length - 1; i >= 0; i--)
                {
                    if (addresses[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IPAddress = addresses[i];
                        break;
                    }
                }
            }
        }

        [XmlElement(ElementName = "Port", IsNullable = false)]
        public Int32 Port = 2584;

        [XmlElement(ElementName = "PDName", IsNullable = false)]
        public String PDName;

        public bool HasAllSettings()
        {
            if (Port > 1 && PDName != "" && IPAddress != null)
                return true;
            else
                return false;
        }
    }
}
