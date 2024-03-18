using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Forzoid.Common;
using Forzoid.ForzaMotorsport2023;

namespace Sample
{
	public static class Program
	{
		public static async Task<int> Main(string[] args)
		{
			int port = ForzoidUdpClient.DefaultPort;

			IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);

			using FM2023DataListener listener = new FM2023DataListener(endPoint);
			using CancellationTokenSource tokenSource = new CancellationTokenSource();

			Console.CancelKeyPress += (object? s, ConsoleCancelEventArgs e) =>
			{
				e.Cancel = true;

				Console.WriteLine("CTRL-C");

				tokenSource.Cancel();
			};

			Console.WriteLine($"begin listening for data on port {port}");

			try
			{
				await ListenForPacketsAsync(listener, tokenSource.Token).ConfigureAwait(false);
			}
			catch (OperationCanceledException)
			{
				Console.WriteLine("stopped");
			}

			return 0;
		}

		private static async ValueTask ListenForPacketsAsync(FM2023DataListener dataListener, CancellationToken cancellationToken)
		{
			float currentBestLap = 0.0f;
			
			StringBuilder sb = new StringBuilder();

			await foreach (FM2023Packet packet in dataListener.ListenAsync(cancellationToken).ConfigureAwait(false))
			{
				string message = CreateConsoleMessage(sb, packet);

				if (packet.Dash.BestLap > currentBestLap)
				{
					await Console.Out.WriteLineAsync("new best lap!").ConfigureAwait(false);

					currentBestLap = packet.Dash.BestLap;
				}

				await Console.Out.WriteLineAsync(message.AsMemory(), cancellationToken).ConfigureAwait(false);

				sb.Clear();
			}
		}

		private static string CreateConsoleMessage(StringBuilder sb, FM2023Packet packet)
		{
			const string SpaceHyphenSpace = " - ";

			sb.Append(packet.Game.FullName);
			sb.Append(SpaceHyphenSpace);
			sb.Append(packet.RawPacket.Source);
			sb.Append(SpaceHyphenSpace);
			sb.Append(packet.Sled.IsRaceOn ? "ON" : "PAUSED");
			
			if (packet.Sled.IsRaceOn)
			{
				sb.Append(SpaceHyphenSpace);
				sb.Append(packet.Dash.RacePosition);
				sb.Append(SpaceHyphenSpace);
				sb.Append(packet.Dash.CurrentLapTime);
				sb.Append(SpaceHyphenSpace);
				sb.Append(packet.Dash.BestLap);
			}

			return sb.ToString();
		}
	}
}
