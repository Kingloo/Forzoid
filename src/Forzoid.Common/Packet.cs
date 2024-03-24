using System;
using System.Net;

namespace Forzoid.Common
{
	public class Packet
	{
		public IPEndPoint Source { get; }
		public IPEndPoint Destination { get; }
		public ReadOnlyMemory<byte> Data { get; }
		public DateTimeOffset ArrivalTime { get; }

		public Packet(IPEndPoint source, IPEndPoint destination, ReadOnlyMemory<byte> data)
		{
			if (source is null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (destination is null)
			{
				throw new ArgumentNullException(nameof(destination));
			}

			Source = source;
			Destination = destination;
			Data = data;

			ArrivalTime = DateTimeOffset.UtcNow;
		}

		public override string ToString()
		{
			return $"{ArrivalTime} - from '{Source}' to '{Destination}' length {Data.Length} bytes";
		}
	}
}
