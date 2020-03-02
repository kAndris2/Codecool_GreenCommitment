using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Common;

namespace Client
{
    public class ClientImpl
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 12345);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            sender.Connect(remoteEP);

            Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

            Random random = new Random();
            Console.WriteLine("Enter your value type:\n");
            Measurement measure = new Measurement(random.Next(1000, 9999),
                                                  random.Next(-150, 150),
                                                  Console.ReadLine(),
                                                  DateTimeOffset.Now.ToUnixTimeMilliseconds());

            // Encode the data string into a byte array.  
            byte[] msg = Encoding.ASCII.GetBytes(measure.ToString());

            // Send the data through the socket.  
            int bytesSent = sender.Send(msg);
            Console.WriteLine("Sent shit: {0}", bytesSent);

            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
    }
}
