using System;
using System.Net;

namespace Forzoid.Common
{
	public class Packet
	{
		public IPEndPoint Source { get; init; }
		public IPEndPoint Destination { get; init; }
		public ReadOnlyMemory<byte> Data { get; init; }
		public DateTimeOffset ArrivalTime { get; init; }

		public Packet(IPEndPoint source, IPEndPoint destination, ReadOnlyMemory<byte> data)
		{
			ArgumentNullException.ThrowIfNull(source);
			ArgumentNullException.ThrowIfNull(destination);
			ArgumentNullException.ThrowIfNull(data);

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