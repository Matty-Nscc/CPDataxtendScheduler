using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CPDataxtendCommands;
using ReplicationPriorities;
using Logs;

namespace CPDataxtendPriorityClient
{
    public class Client
    {
        private string __IP;

        [XmlElement(ElementName = "IP", IsNullable = false)]
        public String IP
        {
            set
            {
                setIP(value);
                __IP = value;
            }

            get { return __IP; }
        }

        [XmlIgnore]
        public IPAddress IPAddress;

        private void setIP(string ip)
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
        public Int32 Port;

        public bool HasAllSettings()
        {
            if (Port > 1 && IPAddress != null)
                return true;
            else
                return false;
        }

        public Client() { }

        public delegate void MessageReceived(object sender, EventArgs e);
        public event MessageReceived OnMessageReceived;
        private string _sendMessage = "";

        public void Start(Settings settings, LogWriter log)
        {
            string siteID = "";

            //Execute the priorities
            foreach(Priority p in settings.PriorityList.Priorities)
            {
                string result = p.ExecutePriority(settings.DB);

                if (result != "")
                    siteID = result;
            }

            settings.DB.Disconnect();

            //A priority has determined that this site should replicate ASAP.
            if(siteID != "")
            {
                Socket client = null;

                try
                {
                    client = new Socket(IPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    client.Connect(IPAddress, Port);
                    log.WriteInfo("Connected to the server.");
                }
                catch (Exception ex)
                {
                    log.WriteError(ex.ToString());
                }

                //Connected to the server.
                if (client != null && client.Connected)
                {
                    Command cmd = new Command();
                    cmd.MySiteID = siteID;
                    cmd.MainSiteID = siteID;

                    Write(client, cmd);
                    Thread.Sleep(100);
                    client.Close();
                    try
                    {
                        settings.PriorityList.SavePriorities();
                    }
                    catch(Exception ex)
                    {
                        log.WriteError(ex.ToString());
                    }
                }
                else
                    log.WriteError("Failed to connect to server.");

                OnMessageReceived("Disconnected from telnet", null);
            }
        }

        private byte[] bytes = new byte[1024];
        public Command Read(Socket socket)
        {
            string msg = "";
            int bytesReceived = socket.Receive(bytes);

            //Received something
            if (bytesReceived > 0)
            {
                msg = Encoding.ASCII.GetString(bytes, 0, bytesReceived);
                var serializer = new XmlSerializer(typeof(Command));
                return (Command)serializer.Deserialize(new StringReader(msg));
            }

            return null;
        }

        private void Write(Socket socket, Command cmd)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Command));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            StringBuilder sb = new StringBuilder();
            XmlWriter xm = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true, ConformanceLevel = ConformanceLevel.Auto, Indent = false });
            serializer.Serialize(xm, cmd, ns);

            byte[] msg = Encoding.ASCII.GetBytes(sb.ToString());
            socket.Send(msg);
        }

        public void WriteMessage(string msg)
        {
            _sendMessage = msg;
        }
    }
}
