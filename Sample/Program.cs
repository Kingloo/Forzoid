using System;
using Forzoid;

namespace Sample
{
    public static class Program
    {
        private static int listenPort = 50120;
        
        public static int Main(string[] args)
        {
            if (TrySetPortNumber(args, out int port))
            {
                listenPort = port;
            }

            using (DataListener listener = new DataListener())
            {
                Console.CancelKeyPress += (s, e) =>
                {
                    Console.WriteLine("exiting...");

                    listener.Stop();
                };

                listener.DataReceived += (s, e) =>
                {
                    Console.WriteLine($"{e.Packet.EndPoint.Address}:{e.Packet.EndPoint.Port} - {e.Packet.ToString()}");
                };

                Console.WriteLine($"begin listening for data on port {listenPort}");

                listener.Listen();
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