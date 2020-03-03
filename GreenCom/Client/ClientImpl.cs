using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Common;

namespace Client
{
    public class ClientImpl
    {
        TcpClient Client;
        NetworkStream Ns;
        Random Random;

        public ClientImpl(string ip, int port)
        {
            //Client = new TcpClient(GetLocalIPAddress(), 12345);
            Client = new TcpClient(ip, port);
            Ns = Client.GetStream();
            Random = new Random();
        }

        public void SendMeasurement(string name)
        {
            Measurement measure = new Measurement(Random.Next(1000, 9999),
                                                  Random.Next(-200, 200),
                                                  name,
                                                  DateTimeOffset.Now.ToUnixTimeMilliseconds());

            byte[] msg = Encoding.ASCII.GetBytes(measure.ToString());
            Ns.Write(msg);
        }

        public void CloseClient()
        {
            Client.Close();
        }

        private string GetLocalIPAddress()
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
    }
}