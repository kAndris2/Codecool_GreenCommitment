using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Common;
using Client;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Common
{
    class Ui
    {

        public void PrintMenu(string title, string[] list, string exitmessage)
        {
            Console.WriteLine(title + $":{Environment.NewLine}");
            int counter = 0;

            foreach (string option in list)
            {
                counter++;
                Console.WriteLine(" (" + Convert.ToString(counter) + ") " + option);
            }

            Console.WriteLine(" (0) " + exitmessage);
        }

        /// <summary>
        /// Handling menu options nad print it out
        /// </summary>
        public void HandleMenu()
        {
            Console.WriteLine($"{Environment.NewLine}Welcome to GreenCom!{Environment.NewLine}");
            string[] options = new string[]
            {
                    "Start Server",
                    "Stop Server",
                    "Add a Client",
                    "Show Graph",
                    "Show IP and Port for Server"
            };

            PrintMenu("Main menu", options, "Exit program");
        }

        /// <summary>
        /// Helps us get input from user and choose a menu option depends on the input
        /// </summary>
        public void Choose()
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

            Console.WriteLine("\nPlease enter a number: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("Explicit:");
                int x = 0;
                float y = 3.8f;

                x += (int)y;
                Console.WriteLine(x);

                Console.WriteLine("Implicit:");
                int z = 0;
                float u = 3.8f;

                //z += u;
                Console.WriteLine(z);
                
            }
            else if (option == "2")
            {

            }
            else if (option == "3")
            {
                Console.Clear();
                ClientImpl client = new ClientImpl(GetLocalIPAddress().ToString(),12345);
                while (true)
                {
                    Console.WriteLine("Do you want to add a new value? [yes/no]:");
                    string userdecide = Console.ReadLine();
                    if (userdecide.ToLower().Equals("yes"))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a value:");
                        string useroption = Console.ReadLine();
                        client.SendMeasurement(useroption);
                        Console.Clear();
                        continue;
                    }
                    else if (userdecide.ToLower().Equals("no"))
                    {
                        Console.Clear();
                        client.CloseClient();
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wrong option! Please try again...");
                        Console.WriteLine($"{Environment.NewLine}Press enter to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                }
            }
            else if (option == "4")
            {

            }
            else if (option == "5")
            {
                Console.Clear();
                Console.WriteLine($"IP: {GetLocalIPAddress()}{Environment.NewLine}Port: 12345");
                Console.WriteLine($"{Environment.NewLine}Press enter to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            else if (option == "0")
            {
                Console.Clear();
                TimeSpan ts = new TimeSpan(0, 0, 2);
                Console.WriteLine("Shutting down...");
                Thread.Sleep(ts);
                Environment.Exit(0);
            }
        }

    }
}
