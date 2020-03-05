using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    Console.WriteLine($"{Environment.NewLine}Trying to connect to the server, please wait! It will take some time...");
                    tcpClient.Connect(ip, port);
                    Console.WriteLine($"{Environment.NewLine}Port is open, connected!");
                    Thread.Sleep(1500);
                    Console.Clear();
                    Client = new TcpClient(ip, port);
                    Ns = Client.GetStream();
                    Random = new Random();
                    
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Oops an error occurred! We are not able to connect to the server. Server is offline!");
                    Console.WriteLine($"{Environment.NewLine}Shutting down...");
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                    
                }
            }
           
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
    }
}