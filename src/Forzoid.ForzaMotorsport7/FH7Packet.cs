using System;
using Forzoid.Common;

namespace Forzoid.ForzaMotorsport7
{
	public class FM7Packet
	{
		public Packet RawPacket { get; }
		public IGame Game { get; }
		// public FH4Dash Dash { get; init; }
		// public FH4Sled Sled { get; init; }

		public FM7Packet(Packet packet)
		{
			ArgumentNullException.ThrowIfNull(packet);

			RawPacket = packet;

			Game = new Game(
				fullName: "Forza Motorsport 7",
				shortName: "FM7",
				releaseYear: 2017
			);

			// Dash = FH4Dash.Create(packet.Data.Span);
			// Sled = FH4Sled.Create(packet.Data.Span);
		}
	}
}
