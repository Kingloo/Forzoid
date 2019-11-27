using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Forzoid
{
    public class DataListener : IDisposable
    {
        private const int minPort = 1024;
        
        private const int _defaultPort = 50120;
        public static int DefaultPort => _defaultPort;
        
        private readonly UdpClient udpClient;
        
        public DataListener()
            : this(new IPEndPoint(IPAddress.Any, _defaultPort))
        { }

        public DataListener(int port)
            : this(new IPEndPoint(IPAddress.Any, port))
        { }

        public DataListener(IPEndPoint endPoint)
        {
            if (endPoint.Port < minPort)
            {
                string message = string.Format(CultureInfo.CurrentCulture, "port cannot be less than {0}, was {1}", minPort, endPoint.Port);

                throw new ArgumentOutOfRangeException(nameof(endPoint), message);
            }

            udpClient = new UdpClient(endPoint);
        }

        public Task<ReadOnlyMemory<byte>> ListenAsync()
            => ListenAsync(CancellationToken.None);

        public async Task<ReadOnlyMemory<byte>> ListenAsync(CancellationToken token)
        {
            Task<UdpReceiveResult> task = Task.Run(udpClient.ReceiveAsync, token);
            
            UdpReceiveResult result = await task.ConfigureAwait(false);

            if (task.IsFaulted)
            {
                return ReadOnlyMemory<byte>.Empty;
            }

            return new ReadOnlyMemory<byte>(result.Buffer);
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