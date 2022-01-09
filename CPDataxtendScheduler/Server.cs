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
using Logs;

namespace CPDataxtendScheduler
{
    [XmlRoot("Server")]
    public class Server
    {
        public event EventHandler ReceivedPriorityReplication;
        public event EventHandler StatusChange;
        private string __IP;
        private LogWriter log;

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

        public Server() { }

        public Server(LogWriter log) { this.log = log; }

        public void SetLogWriter(LogWriter log) { this.log = log; }

        private TcpListener server;
        public delegate void MessageReceived(object sender, EventArgs e);
        public event MessageReceived OnMessageReceived;
        private string _sendMessage = "";
        private bool AcceptClients = false;

        public void ReadWrite()
        {
            try
            {
                server = new TcpListener(IPAddress, Port);
                server.Start();
                StatusChange(true, null);

                AcceptClients = true;

                while (AcceptClients)
                {
                    Socket client = null;

                    //Accept a new client
                    try
                    {
                        if (server.Pending())
                        {
                            client = server.AcceptSocket();
                            client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                            client.NoDelay = true;
                        }
                        else
                            Thread.Sleep(100);
                    }
                    catch (Exception ex) { log.WriteError(ex.ToString()); }

                    //Read Message
                    if(client != null && client.Connected)
                    {
                        Command cmd = null;

                        do
                        {
                            cmd = Read(client);

                            //A command was recieved
                            if (cmd != null)
                            {
                                log.WriteInfo(string.Format("Command Received from {0} to replicate {1}.", new object[] { cmd.MySiteID, cmd.MainSiteID }));

                                ReceivedPriorityReplication(cmd.MainSiteID, null);

                                //Close socket
                                client.Close();
                                break;
                            }
                            else
                                Thread.Sleep(100);

                        } while (cmd == null);
                    }
                }

                server.Stop();
                StatusChange(true, null);
            }
            catch (Exception ex) 
            { 
                log.WriteError(ex.ToString());
                StatusChange(false, null);
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

            if (_sendMessage == "quit")
                AcceptClients = false;
        }

        public void Disconnect()
        {
            AcceptClients = false;
        }
    }
}
