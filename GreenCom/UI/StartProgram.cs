using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace UI
{
    class StartProgram
    {

       static string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        static void Main(string[] args)
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

            Console.WriteLine($"The IP address is: {GetLocalIPAddress()}, and the port is: 12345");

            Console.WriteLine($"{Environment.NewLine}Press enter to start server...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine($"{Environment.NewLine}Server is starting, please wait.");
            Thread.Sleep(1500);
            Process.Start(@"C:\Users\Fsociety\Desktop\master\GreenCom\Server\obj\Debug\netcoreapp3.1\Server.exe");
            
            Console.Clear();
            Console.WriteLine($"{Environment.NewLine}Server started!");

            

        }
    }
}
