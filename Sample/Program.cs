using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Forzoid.Common;
using Forzoid.ForzaMotorsport2023;

namespace Sample
{
	public static class Program
	{
		private readonly static TimeSpan warningThreshold = TimeSpan.FromSeconds(3d);
		private static DateTimeOffset mosetRecentPacketArrived = DateTimeOffset.MinValue;

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
			System.Timers.Timer warningTimer = new System.Timers.Timer(warningThreshold)
			{
				AutoReset = true,
				Enabled = true
			};

			warningTimer.Elapsed += OnWarningThreshold;

			warningTimer.Start();

			TimeSpan currentBestLap = TimeSpan.Zero;

			StringBuilder sb = new StringBuilder();

			await foreach (FM2023Packet packet in dataListener.ListenAsync(cancellationToken).ConfigureAwait(false))
			{
				mosetRecentPacketArrived = DateTimeOffset.UtcNow;

				string message = CreateConsoleMessage(sb, packet);

				if (packet.Dash.BestLap > currentBestLap)
				{
					await Console.Out.WriteLineAsync("new best lap!".AsMemory(), cancellationToken).ConfigureAwait(false);

					currentBestLap = packet.Dash.BestLap;
				}

				await Console.Out.WriteLineAsync(message.AsMemory(), cancellationToken).ConfigureAwait(false);

				sb.Clear();
			}

			if (warningTimer.Enabled)
			{
				warningTimer.Stop();
			}

			warningTimer.Elapsed -= OnWarningThreshold;

			warningTimer.Dispose();
		}

		private static void OnWarningThreshold(object? sender, ElapsedEventArgs e)
		{
			if (DateTimeOffset.UtcNow - mosetRecentPacketArrived > warningThreshold)
			{
				Task.Run(async () => await OnWarningThresholdAsync(sender, e).ConfigureAwait(false))
					.ContinueWith(
						(Task task) => Console.Error.WriteLine($"FM2023DataListener.OnWarningThresholdAsync threw '{task.Exception?.GetType().Name}'"),
						TaskContinuationOptions.OnlyOnFaulted
					);
			}
		}

		private static async Task OnWarningThresholdAsync(object? sender, ElapsedEventArgs e)
		{
			await Console.Error.WriteLineAsync(
				$"no packets for {warningThreshold.TotalSeconds} seconds".AsMemory(),
				CancellationToken.None)
			.ConfigureAwait(false);
		}

		private static string CreateConsoleMessage(StringBuilder sb, FM2023Packet packet)
		{
			const string Separator = " | ";

			sb.Append(packet.Game.ShortName);
			sb.Append(Separator);
			sb.Append(packet.RawPacket.Source);
			sb.Append(Separator);
			sb.Append(packet.Sled.IsRaceOn ? "RACING" : "IN MENUS");

// FM2023 | 192.68.0.52:5200 | {RACING|IN MENUS} | Maple Valley - Full Circuit | Toyota 2000GT (1969) | B 600 | lap 1 | 1st | 00:00:12.345 | 00:00:11.999

			if (packet.Sled.IsRaceOn)
			{
				sb.Append(Separator);
				sb.Append(packet.Dash.Track);
				sb.Append(Separator);
				sb.Append(packet.Sled.Car);
				sb.Append(Separator);
				sb.Append($"{packet.Sled.CarClass} {packet.Sled.CarPerformanceIndex}");
				sb.Append(Separator);
				sb.Append($"lap {packet.Dash.LapNumber}");
				sb.Append(Separator);
				sb.Append(LapNumberHumanReadable(packet.Dash.RacePosition));
				sb.Append(Separator);
				sb.Append(packet.Dash.CurrentLapTime.ToString(@"hh\:mm\:ss\.fff", CultureInfo.CurrentCulture));
				sb.Append(Separator);
				sb.Append(packet.Dash.BestLap.ToString(@"hh\:mm\:ss\.fff", CultureInfo.CurrentCulture));
			}

			return sb.ToString();
		}

		private static string LapNumberHumanReadable(int racePosition)
		{
			ArgumentOutOfRangeException.ThrowIfNegative(racePosition);

			return racePosition switch
			{
				0 => "0",
				1 => "1st",
				2 => "2nd",
				3 => "3rd",
				_ => $"{racePosition}th"
			};
		}
	}
}
