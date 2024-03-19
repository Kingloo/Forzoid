using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Forzoid.Common;

namespace Forzoid.ForzaMotorsport2023
{
	public class FM2023DataListener : IDisposable
	{
		private readonly ForzoidUdpClient udpClient;

		public FM2023DataListener(IPEndPoint localEndPoint)
		{
			ArgumentNullException.ThrowIfNull(localEndPoint);

			udpClient = new ForzoidUdpClient(localEndPoint);
		}

		public async IAsyncEnumerable<FM2023Packet> ListenAsync([EnumeratorCancellation] CancellationToken cancellationToken)
		{
			await foreach (Packet packet in udpClient.ListenAsync(cancellationToken).ConfigureAwait(false))
			{
				yield return new FM2023Packet(packet);
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
