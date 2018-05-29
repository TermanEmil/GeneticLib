﻿using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace GeneticLib.Utils.Graph
{
    public class NeuralNetDrawer
    {
		private static readonly string pyFilePath =
            "../GeneticLib/GeneticLib/Utils/Graph/Python/PyNeuralNetDrawer.py";

		private readonly SocketProxy socketProxy;

		public NeuralNetDrawer(bool verbose = false)
		{
			socketProxy = new SocketProxy(verbose);

			socketProxy.SetupServer();
			StartPyProg();
			socketProxy.StartListening();
		}

		public void QueueNeuralNetJson(string jsonData)
		{
			socketProxy.SendStrMsg(jsonData);
		}

		private void StartPyProg()
        {
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
        }

		private string CreatePyProgJSONSettings()
        {
            var jsonArgv = new
            {
				connection_link = socketProxy.socketEndpoint.Address.ToString(),
				connection_port = socketProxy.socketEndpoint.Port.ToString(),
				verbose = socketProxy.Verbose
            };

            var result = JsonConvert.SerializeObject(jsonArgv);
            result = result.Replace("\"", "\\\"");         
            return result;
        }
    }
}
