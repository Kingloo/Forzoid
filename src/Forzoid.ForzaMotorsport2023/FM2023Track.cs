using System;

namespace Forzoid.ForzaMotorsport2023
{
	public class FM2023Track
	{
		public int Ordinal { get; init; }
		public string Location { get; init; }
		public string Variant { get; init; }

		public FM2023Track(int ordinal, string location, string variant)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(ordinal, -1);
			ArgumentNullException.ThrowIfNull(location);
			ArgumentNullException.ThrowIfNull(variant);

			Ordinal = ordinal;
			Location = location;
			Variant = variant;
		}

		public override string ToString()
		{
			return String.Equals(Location, "unknown", StringComparison.OrdinalIgnoreCase)
				? $"unknown ({Ordinal})"
				: $"{Location} - {Variant}";
		}
	}
}