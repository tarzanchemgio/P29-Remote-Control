using System;
using System.Collections.Generic;
using System.Text;

namespace Helper
{
    public class Menu
    {
        public string MainMenu()
        {
            string[] mainMenu = new string[]
            {
                "Open browser",
                //"Run cmd",
                "Quit"
            };

            string command = "";

            do
            {
                command = "";
                //Console.Clear();

                for (int i = 0; i < mainMenu.Length; i++)
                {
                    Console.WriteLine($"{i}> {mainMenu[i]}");
                }

                Console.Write($"\t> ");

                int option = chooseCommand(mainMenu.Length - 1);

                if (mainMenu[option] == "Open browser")
                {
                    command = this.BrowserMenu();
                }
                else if (mainMenu[option] == "Run cmd")
                {
                    command = this.ManuallyRunCmd();
                }
                else if (mainMenu[option] == "Quit")
                {
                    command = "/C exit";
                }
                else
                {
                    Console.WriteLine($"Invalid option");
                }
            } while (command == "Back to Menu");

            return command;
        }

        //Choose an option in menu from [0-max]
        static public Int32 chooseCommand(int max)
        {
            string input = Console.ReadLine();
            Int32 option = -1;
            while(!Int32.TryParse(input, out option) || option < 0 || option > max)
            {
                Console.Write($"Invalid input. Try again\n\t>");
                input = Console.ReadLine();
            }

            return option;
        }

        public string BrowserMenu()
        {
            string command = "/C explorer ";
            string[] browserMenu = new string[]
                {
                    "google.com",
                    "facebook.com",
                    "youtube.com",
                    "vnexpress.net",
                    "tiki.vn",
                    "linkedin.com",
                    "nettruyen.com",
                    "ln.hako.re",
                    "Manually enter website",
                    "Back to Menu",
                    "Quit"
                };

            for (int i = 0; i < browserMenu.Length; i++)
            {
                Console.WriteLine($"{i}> {browserMenu[i]}");
            }
            Console.Write($"\t> ");

            int option = chooseCommand(browserMenu.Length - 1);

            switch (browserMenu[option])
            {
                case "youtube.com":
                case "google.com":
                case "vnexpress.net":
                case "tiki.vn":
                case "facebook.com":
                case "linkedin.com":
                case "nettruyen.com":
                case "ln.hako.re":
                    {
                        command += "http://" + "\"" + browserMenu[option] + "\"";
                        break;
                    }
                case "Manually enter website":
                    {
                        Console.Write($"\t> ");
                        string web = Console.ReadLine();
                        command += "http://" + "\"" + web + "\"";
                        break;
                    }
                case "Back to Menu":
                    {
                        command = browserMenu[option];
                        break;
                    }
                case "Quit":
                    {
                        command = "/C exit";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return command;
        }

        public string ManuallyRunCmd()
        {
            Console.Write($"\t> ");
            string command = "/C " + Console.ReadLine();
            return command;
        }

        public static bool YesNoAns(string question)
        {
            while (true)
            {
                Console.Write("\t> ");
                string ans = Console.ReadLine();
                switch (ans)
                {
                    case "Yes":
                    case "yes":
                    case "YES":
                    case "Y":
                    case "1":
                    case "ok":
                    case "Ok":
                    case "OK":
                        {
                            return true;
                        }
                    case "No":
                    case "NO":
                    case "no":
                    case "0":
                        {
                            return false;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid answer! Try again, please...");
                            break;
                        }
                }
            }
        }
    }
}
