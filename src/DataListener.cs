using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Forzoid.Data;

namespace Forzoid
{
	public class DataListener : IDisposable
	{
		public const int DefaultPort = 50120;

		private readonly IPEndPoint ipEndPoint;
		private readonly UdpClient udpClient;

		public DataListener()
			: this(new IPEndPoint(IPAddress.Any, DefaultPort))
		{ }

		public DataListener(int port)
			: this(new IPEndPoint(IPAddress.Any, port))
		{ }

		public DataListener(IPEndPoint endPoint)
		{
			ArgumentNullException.ThrowIfNull(endPoint);

			ipEndPoint = endPoint;
			
			udpClient = new UdpClient(endPoint);
		}

		public async IAsyncEnumerable<Packet> ListenAsync([EnumeratorCancellation] CancellationToken cancellationToken)
		{
			cancellationToken.Register(udpClient.Close);

			while (true)
			{
				cancellationToken.ThrowIfCancellationRequested();

				UdpReceiveResult result;

				try
				{
					result = await udpClient.ReceiveAsync(cancellationToken).ConfigureAwait(false);
				}
				catch (SocketException)
				{
					yield break;
				}

				if (Packet.TryCreate(result.Buffer, ipEndPoint, out Packet? packet))
				{
					yield return packet;
				}
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
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
