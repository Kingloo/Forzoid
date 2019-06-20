using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Forzoid.Data;

namespace Forzoid
{
    public static class Program
    {
        private static int listenPort = 50120;
        private static bool keepListening = true;

        public static int Main(string[] args)
        {
            Console.CancelKeyPress += (s, e) => 
            {
                Console.WriteLine("exiting...");

                keepListening = false;
            };

            using (UdpClient listener = new UdpClient(new IPEndPoint(IPAddress.Any, listenPort)))
            {
                listener.Client.ReceiveTimeout = 1000 * 60; // 1000ms * 60 => 1 minute

                IPEndPoint sender = null;

                try
                {
                    while (keepListening)
                    {
                        if (listener.Available > 0)
                        {
                            ReadOnlySpan<byte> span = listener.Receive(ref sender);

                            if (Packet.TryCreate(span, out Packet packet))
                            {
                                Console.WriteLine(packet.ToString());
                            }
                            else
                            {
                                Console.WriteLine("packet creation failed");
                            }
                        }

                        // the game only sends packets every 1/60th of a second (aka once every 16ms)
                        // so there is no point in looping any faster than this
                        Thread.Sleep(16);
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("client timed out");
                }
            }
            
            return 0;
        }
    }
}
