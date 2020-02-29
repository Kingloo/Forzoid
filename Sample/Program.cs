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
                or
            .\Sample {1234}

            depending on how you built/published
        */

        public static async Task<int> Main(string[] args)
        {
            int port = TrySetPortNumber(args, out int p) ? p : DataListener.DefaultPort;

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

            await foreach (Packet packet in listener.ListenAsync(tokenSource.Token))
            {
                string message = $"{packet.EndPoint.Address}:{packet.EndPoint.Port} - pos.: {packet.Dash.RacePosition} - lap: {packet.Dash.LapNumber} - cur. race time: {packet.Dash.CurrentRaceTime}";

                Console.WriteLine(message);
            }

            Console.WriteLine(" - exited!");

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
                Console.Error.WriteLine($"!! port number ({newPort}) must be >=1024 and <=65535");

                port = -1;
                return false;
            }

            port = newPort;
            return true;
        }
    }
}