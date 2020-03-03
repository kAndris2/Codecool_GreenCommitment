using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class ServerImpl
    {
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void Main(string[] args)
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

        private static void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();
            

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket client = listener.EndAccept(ar);

            //WHILE loop

            byte[] buff = new byte[1024];
            int bytesReads = client.Receive(buff);

            if (bytesReads < buff.Length)
            {
                Console.WriteLine("{0}", Encoding.ASCII.GetString(buff, 0, bytesReads));
            }
            client.Close();
        }
    }
}
