// A C# program for Client
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using Helper;

namespace Client
{
	public class Client
	{
		public Int32 port = 11111;
		public string ServerName;
		public string IP;

		public Client()
        {
			port = 11111;
			ServerName = Dns.GetHostName();
			IP = IPAddress.Loopback.ToString();
		}

		public Client(string IP)
        {
			this.IP = IP;
        }

		public Client(string IP, Int32 port)
        {
			this.IP = IP;
			this.port = port;
        }

		public void ExecuteClient()
		{
            try
				{
				Socket sender = null;

				// Establish the remote endpoint 
				// for the socket. This example 
				// uses port 11111 on the local 
				// computer. 
				IPAddress ipAddr = IPAddress.Parse(this.IP);

				IPEndPoint endPoint = new IPEndPoint(ipAddr, port);
				sender = new Socket(endPoint.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

				// Connect Socket to the remote 
				// endpoint using method Connect() 
				sender.Connect(endPoint);

				//Socket sender = this.getSocket();

				//sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//sender.Connect(ipAddr, this.port);

				// We print EndPoint information 
				// that we are connected 
				Console.WriteLine("Socket connected to: {0} ",
							sender.RemoteEndPoint.ToString());

				// Creation of messagge that 
				// we will send to Server 
				byte[] messageSent = Encoding.ASCII.GetBytes("Hi, I'm Client\0");
				int byteSent = sender.Send(messageSent);

				// Data buffer 
				byte[] messageReceived = new byte[1024];
				// We receive the messagge using 
				// the method Receive(). This 
				// method returns number of bytes 
				// received, that we'll use to 
				// convert them to string 
				int byteRecv = sender.Receive(messageReceived);
				Console.WriteLine("Message from Server: {0}",
					Encoding.ASCII.GetString(messageReceived,
												0, byteRecv));

				//sender.Send(Encoding.ASCII.GetBytes("/C ipconfig\0"));
				//byteRecv = sender.Receive(messageReceived);
				//Console.WriteLine("Message from Server -> {0}",
				//	Encoding.ASCII.GetString(messageReceived,
				//								0, byteRecv));

				sender.Shutdown(SocketShutdown.Both);
				sender.Close();
			}

				// Manage of Socket's Exceptions 
				catch (ArgumentNullException ane)
				{

					Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				}

				catch (SocketException se)
				{

					Console.WriteLine("SocketException : {0}", se.ToString());
				}

				catch (Exception e)
				{
					Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}
		}

		public string SendRequest(string request)
        {
            try
            {
				Socket sender = null;

				// Establish the remote endpoint 
				// for the socket. This example 
				// uses port 11111 on the local 
				// computer. 
				IPAddress ipAddr = IPAddress.Parse(this.IP);

				IPEndPoint endPoint = new IPEndPoint(ipAddr, port);
				sender = new Socket(endPoint.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

				// Connect Socket to the remote 
				// endpoint using method Connect() 
				sender.Connect(endPoint);

				//Socket sender = this.getSocket();

				//sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//sender.Connect(ipAddr, this.port);

				// Creation of messagge that 
				// we will send to Server 
				byte[] messageSent = Encoding.ASCII.GetBytes(request);
				int byteSent = sender.Send(messageSent);

				// Data buffer 
				byte[] messageReceived = new byte[1024];
                // We receive the messagge using 
                // the method Receive(). This 
                // method returns number of bytes 
                // received, that we'll use to 
                // convert them to string 

                int byteRecv = sender.Receive(messageReceived);
                string recv = Encoding.ASCII.GetString(messageReceived, 0, byteRecv);

                //sender.Send(Encoding.ASCII.GetBytes("/C ipconfig\0"));
                //byteRecv = sender.Receive(messageReceived);
                //Console.WriteLine("Message from Server -> {0}",
                //	Encoding.ASCII.GetString(messageReceived,
                //								0, byteRecv));

                sender.Shutdown(SocketShutdown.Both);
				sender.Close();

				return recv;
			}
            catch (Exception e)
            {
				Console.WriteLine(e.ToString());
                throw;
            }
        }
	}
}
