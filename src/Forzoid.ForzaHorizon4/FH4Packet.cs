using System;
using Forzoid.Common;

namespace Forzoid.ForzaHorizon4
{
	public class FH4Packet
	{
		public Packet RawPacket { get; }
		public IGame Game { get; }
		// public FH4Dash Dash { get; init; }
		// public FH4Sled Sled { get; init; }

		public FH4Packet(Packet packet)
		{
			if (packet is null)
			{
				throw new ArgumentNullException(nameof(packet));
			}

			RawPacket = packet;

			Game = new Game(
				fullName: "Forza Horizon 4",
				shortName: "FH4",
				releaseYear: 2018
			);

			// Dash = FH4Dash.Create(packet.Data.Span);
			// Sled = FH4Sled.Create(packet.Data.Span);
		}
	}
}
