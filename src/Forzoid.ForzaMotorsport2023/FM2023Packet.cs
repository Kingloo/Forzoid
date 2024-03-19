using System;
using Forzoid.Common;

namespace Forzoid.ForzaMotorsport2023
{
	public class FM2023Packet
	{
		public Packet RawPacket { get; }
		public IGame Game { get; }
		public FM2023Dash Dash { get; private set; }
		public FM2023Sled Sled { get; private set; }

		public FM2023Packet(Packet packet)
		{
			ArgumentNullException.ThrowIfNull(packet);

			RawPacket = packet;

			Game = new Game(
				fullName: "Forza Motorsport (2023)",
				shortName: "FM2023",
				releaseYear: 2023
			);

			Dash = FM2023Dash.Create(packet.Data.Span);
			Sled = FM2023Sled.Create(packet.Data.Span);
		}
	}
}
