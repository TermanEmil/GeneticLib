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
    public class SocketProxy
    {      
		private static readonly string pyFilePath =
			"../GeneticLib/GeneticLib/Utils/Graph/Python/PyNeuralNetDrawer.py";

		public bool Verbose { get; set; }

		private Socket serverSocket;
		private Socket clientSocket = null;
		private IPEndPoint socketEndpoint;      

		public SocketProxy(bool verbose = true)
        {
			Verbose = verbose;
			                  
			SetupServer();
			StartPyProg();
			StartListening();         
        }

		public bool SendStrMsg(string msg)
		{
			if (clientSocket == null)
				return false;
			
			SendData(clientSocket, msg);         
			return true;
		}

		private void SetupServer()
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

		private void StartListening()
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

			targetSocket.BeginSend(
                data,
                0,
                data.Length,
                SocketFlags.None,
				new AsyncCallback(SendDataCallback),
				targetSocket);
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

		private void StartPyProg()
		{
			LogMsg(() => Console.WriteLine("Starting py progr..."));
			using (var p = new Process())
            {
                var info = new ProcessStartInfo("python3")
                {
                    Arguments = pyFilePath + " " + CreatePyProgJSONSettings(),
                    RedirectStandardInput = false,
                    RedirectStandardError = false,
                    RedirectStandardOutput = false,
                    UseShellExecute = false
                };

                p.StartInfo = info;
                p.Start();
            }
			LogMsg(() => Console.WriteLine("Py prog started."));
		}

		#region Helpers
		private string CreatePyProgJSONSettings()
		{
			var jsonArgv = new
			{
				connection_link = socketEndpoint.Address.ToString(),
				connection_port = socketEndpoint.Port.ToString(),
				verbose = Verbose
			};

			var result = JsonConvert.SerializeObject(jsonArgv);
			result = result.Replace("\"", "\\\"")
						   .Replace(" ", "")
						   .Replace("\n", "");

			return result;
		}

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
