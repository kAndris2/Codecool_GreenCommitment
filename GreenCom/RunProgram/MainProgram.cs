using System;
using System.Collections.Generic;
using System.Text;
using Client;
using Common;
using Server;

namespace RunProgram
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Ui ui = new Ui();
            while (true)
            {
                ui.HandleMenu();
                try
                {
                    ui.Choose();
                }
                catch (Exception)
                {

                    throw new InvalidOperationException("Wrong option, please try again.");
                }
            }
        }
    }
}
