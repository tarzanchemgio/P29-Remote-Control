using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Helper;

namespace Client
{
    class ClientMain
    {
        static void Main(string[] args)
        {
            //State 0: Connect to server
            bool ans = true;
            while (ans)
            {
                try
                {
                    string clientConfig = "Connect to server, IP: ";
                    Console.Write(clientConfig);
                    string ip = Console.ReadLine();

                    Client client = new Client(ip);
                    client.ExecuteClient();

                    Menu menu = new Menu();

                    while (true)
                    {
                        string cmd = menu.MainMenu() + "\0";

                        string result = client.SendRequest(cmd);
                        Console.WriteLine($"From server: {result}");

                        if (cmd == "/C exit\0")
                        {
                            Environment.Exit(0);
                            return;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    ans = Menu.YesNoAns("Would you like to try again?");
                }
            }

        }

    }
}
