using System;

namespace Forzoid.ForzaMotorsport2023
{
	public class FM2023Track
	{
		public int Ordinal { get; }
		public string Location { get; }
		public string Variant { get; }

		public FM2023Track(int ordinal, string location, string variant)
		{
			if (ordinal < -1)
			{
				throw new ArgumentOutOfRangeException(nameof(ordinal));
			}

			if (location is null)
			{
				throw new ArgumentNullException(nameof(location));
			}

			if (variant is null)
			{
				throw new ArgumentNullException(nameof(variant));
			}

			Ordinal = ordinal;
			Location = location;
			Variant = variant;
		}

		public override string ToString()
		{
			return $"{Location} - {Variant} ({Ordinal})";
		}
	}
}
