using Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class ServerImpl
    {
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public static List<Measurement> Datas = new List<Measurement>();

        public void Start()
        {
            string GetLocalIPAddress()
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }

            IPAddress ipAddress = IPAddress.Parse(GetLocalIPAddress());
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 12345);

            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                allDone.Reset();

                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                allDone.WaitOne();
            }
        }

        public void Close()
        {
            allDone.Close();
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket client = listener.EndAccept(ar);

            while (true)
            {
                byte[] buff = new byte[1024];
                int bytesReads = client.Receive(buff);

                if (bytesReads == 0)
                    break;
                else if (bytesReads < buff.Length)
                {
                    Console.WriteLine("{0}", Encoding.ASCII.GetString(buff, 0, bytesReads));
                    Datas.Add(ConvertToObject(Encoding.ASCII.GetString(buff, 0, bytesReads)));
                    DataHandler.Serialize(Datas);

                }
            }
        }

        private static  Measurement ConvertToObject(string stringdata)
        {
            string [] split = stringdata.Split("\n");
            string temp = "";
            bool check = false;

            foreach (string item in split)
            {
                foreach (char character in item)
                {
                    if (character.Equals(' '))
                    {
                        check = true;
                        continue;
                    }
                    if (check)
                    {
                        temp += character;
                    }
                }
                temp += ",";
                check = false;
            }

            return new Measurement(temp.Split(","));
        }
    }
}
