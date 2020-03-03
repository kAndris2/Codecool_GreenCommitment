using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Common;
using Client;
using System.Threading;

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
                    "Close Client",
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
            Console.WriteLine("\nPlease enter a number: ");
            string option = Console.ReadLine();

            if (option == "1")
            {

            }
            else if (option == "2")
            {

            }
            else if (option == "3")
            {

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
