using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Forzoid.Data;

namespace Forzoid
{
    public class DataListener : IDisposable
    {
        private const int minPort = 1024;
        private const int maxPort = 65535;
        
        private const int _defaultPort = 50120;
        public static int DefaultPort => _defaultPort;

        private readonly IPEndPoint ipEndPoint;
        private readonly UdpClient udpClient;
        
        public DataListener()
            : this(new IPEndPoint(IPAddress.Any, _defaultPort))
        { }

        public DataListener(int port)
            : this(new IPEndPoint(IPAddress.Any, port))
        { }

        public DataListener(IPEndPoint endPoint)
        {
            if (endPoint.Port < minPort || endPoint.Port > maxPort)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "port number must be between {0} and {1} (you provided {2})", minPort, maxPort, endPoint.Port);

                throw new ArgumentOutOfRangeException(nameof(endPoint), message);
            }

            ipEndPoint = endPoint;
            udpClient = new UdpClient(endPoint);
        }

        public async Task<Packet?> ListenAsync()
        {
            UdpReceiveResult result = await udpClient.ReceiveAsync().ConfigureAwait(false);

            return Packet.TryCreate(result.Buffer, ipEndPoint, out Packet? packet) ? packet : null;
        }

        public async IAsyncEnumerable<Packet?> ListenEnumerableAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                UdpReceiveResult result = await udpClient.ReceiveAsync();

                if (Packet.TryCreate(result.Buffer, ipEndPoint, out Packet? packet))
                {
                    yield return packet;
                }
                else // unsure if this is necessary
                {
                    yield return Packet.Empty;
                }
            }

            yield break;
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