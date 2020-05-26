// A C# Program for Server 
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{ 
	class Server
	{
		public Int32 port = 11111;
		public string IP;
		public Socket listener;

		Socket clientSocket;

		public Server()
        {
			port = 11111;
			IP = this.getPublicIPAddress();
		}

		public Server(string IP)
        {
			this.IP = IP;
        }

		public void ExecuteServer()
		{
			try
			{
				IPAddress ipAddr = IPAddress.Parse(this.IP);

				IPEndPoint endPoint = new IPEndPoint(ipAddr, port);

				// Creation TCP/IP Socket using 
				// Socket Class Costructor 
				listener = new Socket(endPoint.AddressFamily,
							SocketType.Stream, ProtocolType.Tcp);

				// Using Bind() method we associate a 
				// network address to the Server Socket 
				// All client that will connect to this 
				// Server Socket must know this network 
				// Address 
				listener.Bind(endPoint);

				// Using Listen() method we create 
				// the Client list that will want 
				// to connect to Server 
				listener.Listen(10);
			}

			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public void WaitForRequest()
        {
            try
            {
				while (true)
				{
					Console.WriteLine("Waiting connection ... ");

					// Suspend while waiting for 
					// incoming connection Using 
					// Accept() method the server 
					// will accept connection of client 
					clientSocket = listener.Accept();

					// Data buffer 
					byte[] bytes = new Byte[1024];
					string data = null;

					while (true)
					{

						int numByte = clientSocket.Receive(bytes);

						data += Encoding.ASCII.GetString(bytes,
												0, numByte);

						if (data.IndexOf("\0") > -1)
							break;
					}

					Console.WriteLine("Message received: {0} ", data);
					string result = this.HandleRequest(data);
					// Send a message to Client 
					// using Send() method 
					byte[] message = Encoding.ASCII.GetBytes(result);
					clientSocket.Send(message);

					if (result == "exit\0")
					{
						Environment.Exit(0);
					}
				}
			}
            catch (Exception e)
            {
				Console.WriteLine(e.ToString());
            }
        }

		string getPublicIPAddress()
        {
			string externalip = new WebClient().DownloadString("http://icanhazip.com");
			return externalip.Substring(0, externalip.Length-1);
		}

		public string getIPv4()
        {
			return IPAddress.Parse(this.IP).MapToIPv4().ToString();
        }

		public string getIPv6()
        {
			return IPAddress.Parse(this.IP).MapToIPv6().ToString();
        }

		string HandleRequest(string rq)
        {
			string returnMessage = "";
			switch (rq)
			{
				case "Hi, I'm Client\0":
                    {
						returnMessage = "Hi, I'm server";
						break;
                    }
				case "/C exit\0":
                    {
						returnMessage = "exit";
						break;
                    }
				default:
					{
						ProcessStartInfo startInfo = new ProcessStartInfo("CMD.exe", rq)
						{
							RedirectStandardOutput = true
						};
						Process process = new Process();
						process.StartInfo = startInfo;

						if (process.Start())
						{
							returnMessage = "Operation successs!";
						}
                        else
                        {
							returnMessage = "Invalid command";
                        }
						break;
					}
			}

			return returnMessage + "\0";
        }

		static public IPAddress[] AddressList()
        {
			return Dns.GetHostEntry(Dns.GetHostName()).AddressList;
        }

		~Server()
        {
			if (!clientSocket.IsBound || clientSocket == null)
				return;
            else
            {
				// Close client Socket using the 
				// Close() method. After closing, 
				// we can use the closed Socket 
				// for a new Client Connection 
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();
			}

			if(!listener.IsBound || listener == null)
            {
				return;
            }
            else
            {
				listener.Shutdown(SocketShutdown.Both);
				listener.Close();
			}
			
		}
	}
}
