using System;
using Forzoid.Common;

namespace Forzoid.ForzaHorizon5
{
	public class FH5Packet
	{
		public Packet RawPacket { get; }
		public IGame Game { get; }
		// public FH4Dash Dash { get; init; }
		// public FH4Sled Sled { get; init; }

		public FH5Packet(Packet packet)
		{
			if (packet is null)
			{
				throw new ArgumentNullException(nameof(packet));
			}

			RawPacket = packet;

			Game = new Game(
				fullName: "Forza Horizon 5",
				shortName: "FH5",
				releaseYear: 2021
			);

			// Dash = FH4Dash.Create(packet.Data.Span);
			// Sled = FH4Sled.Create(packet.Data.Span);
		}
	}
}