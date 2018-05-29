using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeneticLib.Utils.Graph
{
	/// <summary>
    /// A server socket which has only one client.
	/// I use it to communicate with my python programs.
    /// </summary>
    public class SocketProxy
    {      
		public bool Verbose { get; set; }

		private Socket serverSocket;
		private Socket clientSocket = null;
		public IPEndPoint socketEndpoint;      

		public SocketProxy(bool verbose = true)
        {
			Verbose = verbose;       
        }

		public bool SendStrMsg(string msg)
		{
			if (clientSocket == null)
				return false;
			
			SendData(clientSocket, msg);         
			return true;
		}

		public void SetupServer()
		{
			LogMsg(() => Console.WriteLine("Setting up the server..."));         

			serverSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);
			
			TryToConnectUntilAValidPortIsFound();         
            serverSocket.Listen(5);

			LogMsg(() => Console.WriteLine("Server set up."));
		}

		public void StartListening()
		{
			LogMsg(() => Console.WriteLine("Starting listening"));
			while (clientSocket == null)
			    serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
		}

		private void AcceptCallback(IAsyncResult asyncResult)
		{
			LogMsg(() => Console.WriteLine("A client has connected"));

			Socket newClient = serverSocket.EndAccept(asyncResult);
			clientSocket = newClient;

            // Start accepting again.
			serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
		}
        
		private void SendData(Socket targetSocket, string msg)
		{
			var data = Encoding.ASCII.GetBytes(msg);
			msg = String.Format("{0, -7}", data.Length) + msg;
			data = Encoding.ASCII.GetBytes(msg);

			try
			{
				targetSocket.BeginSend(
					data,
					0,
					data.Length,
					SocketFlags.None,
					new AsyncCallback(SendDataCallback),
					targetSocket);
			}
			catch (SocketException)
			{
				clientSocket.Shutdown(SocketShutdown.Both);
				clientSocket.Close();
				clientSocket = null;
			}
		}

		private void SendDataCallback(IAsyncResult asyncResult)
        {
            LogMsg(() => Console.WriteLine("The message has been received."));
            try
            {
                var socket = asyncResult.AsyncState as Socket;
                socket.EndSend(asyncResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }         
        }

		#region Helpers    
        /// <summary>
        /// Very stupid... but working solution...
        /// </summary>
		private void TryToConnectUntilAValidPortIsFound()
		{
			for (int port = 2000; port < 3000; port++)
            {
                socketEndpoint = new IPEndPoint(IPAddress.Any, port);

                try
                {
                    serverSocket.Bind(socketEndpoint);
                    break;
                }
                catch (SocketException)
                {
                }
            }
		}

		private void LogMsg(Action msgAction)
		{
			if (Verbose)
				msgAction.Invoke();
		}
		#endregion
    }
}
