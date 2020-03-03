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
            ClientImpl client = new ClientImpl("192.168.150.1", 12345);
            client.SendMeasurement("xxx");
        }
    }
}
