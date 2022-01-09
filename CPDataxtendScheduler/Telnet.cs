using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CPDataxtendScheduler
{
    class Telnet
    {
        private TcpClient client = new TcpClient();
        public delegate void MessageReceived(object sender, EventArgs e);
        public event MessageReceived OnMessageReceived;
        private string _sendMessage = "";
        private bool _Running = false;

        public bool GetStatus()
        {
            return _Running;
        }
        
        public Telnet(IPEndPoint server)
        {
            client.Connect(server);
        }

        public void ReadWrite()
        {
            _Running = true;

            try
            {
                NetworkStream stream = client.GetStream();
                StreamWriter sw = new StreamWriter(stream);
                sw.AutoFlush = true;

                //Get the status
                if (client.Connected)
                {
                    OnMessageReceived("Connected to telnet!", null);
                    sw.Write("show status update\n");
                    OnMessageReceived("show status update", null);
                }

                while (client.Connected)
                {
                    string receivedMsg = string.Empty;

                    //Read
                    while (stream.DataAvailable)
                    {
                        Byte[] data = new Byte[client.ReceiveBufferSize];
                        int bytes = stream.Read(data, 0, data.Length);
                        receivedMsg += Encoding.ASCII.GetString(data, 0, bytes);
                    }

                    //Read data found
                    if (receivedMsg != string.Empty)
                    {
                        string[] messages = receivedMsg.Split('+');

                        if (messages.Length > 1)
                        {
                            for (int i = 0; i < messages.Length; i++)
                                if (messages[i] != "")
                                    OnMessageReceived("+" + messages[i], null);
                        }
                        else
                            OnMessageReceived(receivedMsg, null);

                        receivedMsg = string.Empty;
                    }

                    //write
                    if (_sendMessage != "")
                    {
                        try
                        {
                            sw.Write(_sendMessage + "\n");
                        }
                        catch (System.IO.IOException ex)
                        {
                            _sendMessage = "crashed";
                        }

                        OnMessageReceived(_sendMessage, null);

                        if (_sendMessage == "quit" || _sendMessage == "crashed")
                        {
                            try
                            {
                                stream.Close();
                            }
                            catch (Exception) { }
                            try
                            {
                                stream.Dispose();
                            }
                            catch (Exception) { }

                            try
                            {
                                client.Close();
                            }
                            catch (Exception) { }

                            break;
                        }

                        _sendMessage = "";
                    }

                    Thread.Sleep(10);
                }

                OnMessageReceived("Disconnected from telnet", null);
            }
            catch(Exception ex)
            {
                _Running = false;
                throw ex;
            }

            _Running = false;
        }

        public void WriteMessage (string msg)
        {
            _sendMessage = msg;
        }
    }
}
