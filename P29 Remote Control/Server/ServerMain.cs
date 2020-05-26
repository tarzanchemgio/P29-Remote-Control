using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Helper;

namespace Server
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            while (true)
            {//State 0: Server Config
                Console.WriteLine($"Choose an IP adress for server: ");

                IPAddress[] addresses = Server.AddressList();
                for (int i = 0; i < addresses.Length; i++)
                {
                    Console.WriteLine($"{i}> {addresses[i]}");
                }

                Console.Write("\t> ");
                int option = Menu.chooseCommand(addresses.Length - 1);

                //State 1: Execute server and wait
                try
                {
                    Server tmpServer = new Server(addresses[option].ToString());
                    Console.WriteLine($"Server IP: {tmpServer.IP}\nPort: {tmpServer.port}");
                    tmpServer.ExecuteServer();
                    tmpServer.WaitForRequest();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }
}
