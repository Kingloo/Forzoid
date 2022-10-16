using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Forzoid;
using Forzoid.Data;

namespace Sample
{
	public static class Program
	{
		/*
            dotnet run {1234}
                or
            dotnet .\Sample.dll {1234}
                or
            .\Sample.exe {1234}
                or
            .\Sample {1234}

            depending on how you built/published
        */

		public static async Task<int> Main(string[] args)
		{
			int port = int.TryParse(args[0], out int p) ? p : DataListener.DefaultPort;

			IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);

			using DataListener listener = new DataListener(endPoint);
			using CancellationTokenSource tokenSource = new CancellationTokenSource();

			Console.CancelKeyPress += (s, e) =>
			{
				e.Cancel = true;

				Console.Write("exiting...");

				tokenSource.Cancel();
			};

			Console.WriteLine($"begin listening for data on port {port}");

			StringBuilder sb = new StringBuilder();

			await foreach (Packet packet in listener.ListenAsync(tokenSource.Token))
			{
				if (packet.EndPoint is null
					|| packet.Dash is null)
				{
					continue;
				}

				sb.Append($"{packet.EndPoint.Address}:{packet.EndPoint.Port} - ");
				sb.Append($"pos.: {packet.Dash.RacePosition} - ");
				sb.Append($"lap: {packet.Dash.LapNumber} - ");
				sb.Append($"cur. race time: {packet.Dash.CurrentRaceTime}");

				Console.WriteLine(sb.ToString());

				sb.Clear();
			}

			Console.WriteLine(" - exited!");

			return 0;
		}
	}
}
