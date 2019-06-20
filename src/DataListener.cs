using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Forzoid.Data;

namespace Forzoid
{
    public class DataListener : IDisposable
    {
        public event EventHandler<DataReceivedEventArgs> DataReceived;
        private void OnDataReceived(Packet packet)
        {
            DataReceived?.Invoke(this, new DataReceivedEventArgs(packet));
        }

        private const int defaultPort = 50120;
        private readonly UdpClient udpClient = null;
        private bool keepListening = false;
        
        public DataListener()
            : this(IPAddress.Any, defaultPort)
        { }

        public DataListener(int port)
            : this(IPAddress.Any, port)
        { }

        public DataListener(IPAddress ipAddress, int port)
        {
            if (port < 1024 || port > 65535)
            {
                throw new ArgumentOutOfRangeException(nameof(port), $"port ({port}) must be between 1024 and 65535");
            }

            udpClient = new UdpClient(new IPEndPoint(ipAddress, port));
        }

        public void Listen()
        {
            keepListening = true;

            udpClient.Client.ReceiveTimeout = 1000 * 60; // 1000ms * 60 => 1 minute

            IPEndPoint sender = null;

            try
            {
                while (keepListening)
                {
                    if (udpClient.Available > 0)
                    {
                        ReadOnlySpan<byte> span = udpClient.Receive(ref sender);

                        if (Packet.TryCreate(span, sender, out Packet packet))
                        {
                            OnDataReceived(packet);
                        }
                    }

                    // the game only sends packets every 1/60th of a second (aka once every 16ms)
                    // so there is no point in looping any faster than this
                    Thread.Sleep(16);
                }
            }
            catch (SocketException)
            {
                Console.Error.WriteLine("client timed out");
            }
        }

        public void Stop()
        {
            keepListening = false;
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    udpClient.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}