using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Forzoid.Common;

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
			int port = DataListener.DefaultPort;

			if (args.Length > 0 && int.TryParse(args[0], out int p))
			{
				port = p;
			}

			IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);

			using DataListener listener = new DataListener(endPoint);
			using CancellationTokenSource tokenSource = new CancellationTokenSource();

			Console.CancelKeyPress += (s, e) =>
			{
				e.Cancel = true;

				tokenSource.Cancel();
			};

			Console.WriteLine($"begin listening for data on port {port}");

			try
			{
				await ListenForPacketsAsync(listener, tokenSource.Token).ConfigureAwait(false);
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("cancelled");
			}

			return 0;
		}

		private static async ValueTask ListenForPacketsAsync(DataListener dataListener, CancellationToken cancellationToken)
		{
			StringBuilder sb = new StringBuilder();

			await foreach (Packet packet in dataListener.ListenAsync(cancellationToken))
			{
				if (packet.EndPoint is null
					|| packet.Dash is null)
				{
					continue;
				}

				sb.Append($"{packet.Game.ToString()} - ");
				sb.Append($"{packet.EndPoint.Address}:{packet.EndPoint.Port} - ");
				sb.Append($"pos.: {packet.Dash.RacePosition} - ");
				sb.Append($"lap: {packet.Dash.LapNumber} - ");
				sb.Append($"cur. race time: {packet.Dash.CurrentRaceTime}");

				await Console.Out.WriteLineAsync(sb.ToString().AsMemory(), cancellationToken).ConfigureAwait(false);

				sb.Clear();
			}
		}
	}
}
