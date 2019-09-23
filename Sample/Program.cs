using System;
using System.Net;
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

            depending on how you built/published
        */

        public static async Task<int> Main(string[] args)
        {
            int port = TrySetPortNumber(args, out int p) ? p : DataListener.DefaultPort;

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);

            using (DataListener listener = new DataListener(endPoint))
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                Console.CancelKeyPress += (s, e) =>
                {
                    Console.WriteLine("exiting...");

                    tokenSource.Cancel();
                };

                Console.WriteLine($"begin listening for data on port {port}");

                do
                {
                    ReadOnlyMemory<byte> rawData = await listener.ListenAsync(tokenSource.Token);

                    if (Packet.TryCreate(rawData, endPoint, out Packet packet))
                    {
                        string message = $"{packet.EndPoint.Address}:{packet.EndPoint.Port} - pos.: {packet.Dash.RacePosition} - lap: {packet.Dash.LapNumber} - tire temp: {packet.Dash.TireTempFrontRight} - cur. race time: {packet.Dash.CurrentRaceTime}";

                        Console.WriteLine(message);
                    }
                }
                while (!tokenSource.IsCancellationRequested);
            }

            return 0;
        }

        private static bool TrySetPortNumber(string[] args, out int port)
        {
            if (args.Length < 1)
            {
                port = -1;
                return false;
            }

            if (!int.TryParse(args[0], out int newPort))
            {
                port = -1;
                return false;
            }

            if (newPort < 1024 || newPort > 65535)
            {
                Console.Error.WriteLine($"Fatal error: port number ({newPort}) must be between 1024 and 65535");

                port = -1;
                return false;
            }

            port = newPort;
            return true;
        }
    }
}