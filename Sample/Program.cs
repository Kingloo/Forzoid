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
        private static int listenPort = DataListener.DefaultPort;

        public static async Task<int> Main(string[] args)
        {
            /*
                dotnet run 1234
                    or
                dotnet Sample.dll 1234
                    or
                Sample.exe 1234
            */

            if (TrySetPortNumber(args, out int port))
            {
                listenPort = port;
            }

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, listenPort);

            using (DataListener listener = new DataListener(endPoint))
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                bool keepListening = true;

                Console.CancelKeyPress += (s, e) =>
                {
                    Console.WriteLine("exiting...");

                    keepListening = false;

                    tokenSource.Cancel();
                };

                Console.WriteLine($"begin listening asyncly for data on port {listenPort}");

                while (keepListening)
                {
                    ReadOnlyMemory<byte> rawData = await listener.ListenAsync(tokenSource.Token);

                    if (Packet.TryCreate(rawData, endPoint, out Packet packet))
                    {
                        Console.WriteLine($"{packet.EndPoint.Address}:{packet.EndPoint.Port} - {packet.ToString()}");
                    }
                }
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