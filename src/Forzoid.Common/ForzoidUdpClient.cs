using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Forzoid.Common
{
	public class ForzoidUdpClient : IDisposable
	{
		public const int DefaultPort = 50120;

		private readonly IPEndPoint localEndPoint;
		private readonly UdpClient udpClient;

		public ForzoidUdpClient(IPEndPoint localEndPoint)
		{
			if (localEndPoint is null)
			{
				throw new ArgumentNullException(nameof(localEndPoint));
			}

			this.localEndPoint = localEndPoint;
			udpClient = new UdpClient(localEndPoint);
		}

		public async IAsyncEnumerable<Packet> ListenAsync([EnumeratorCancellation] CancellationToken cancellationToken)
		{
			cancellationToken.Register(udpClient.Close);

			while (disposedValue == false)
			{
				cancellationToken.ThrowIfCancellationRequested();

				UdpReceiveResult result;

				try
				{
					result = await udpClient.ReceiveAsync().ConfigureAwait(false);
				}
				catch (SocketException socketException)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						throw new OperationCanceledException("cancellation token called UdpClient.Close", socketException, cancellationToken);
					}
					else
					{
						throw;
					}
				}

				yield return new Packet(result.RemoteEndPoint, localEndPoint, result.Buffer);
			}
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
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
