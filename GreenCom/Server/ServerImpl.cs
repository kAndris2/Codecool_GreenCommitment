﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class ServerImpl
    {
        private static ManualResetEvent allDone = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.150.9");
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
                Console.WriteLine("PITE: {0} {1}", bytesReads, Encoding.ASCII.GetString(buff, 0, bytesReads));
            }
            else
            {
                Console.WriteLine("KEX");

            }
            client.Close();

        }
    }
}
