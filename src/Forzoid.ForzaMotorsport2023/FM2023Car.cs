using System;

namespace Forzoid.ForzaMotorsport2023
{
	public class FM2023Car
	{
		public int Ordinal { get; init; }
		public string Make { get; init; }
		public string Model { get; init; }
		public int ReleaseYear { get; init; }

		public FM2023Car(int ordinal, string make, string model, int releaseYear)
		{
			ArgumentOutOfRangeException.ThrowIfLessThan(ordinal, -1);
			ArgumentNullException.ThrowIfNull(make);
			ArgumentNullException.ThrowIfNull(model);
			ArgumentOutOfRangeException.ThrowIfNegative(releaseYear);

			Ordinal = ordinal;
			Make = make;
			Model = model;
			ReleaseYear = releaseYear;
		}

		public override string ToString()
		{
			return String.Equals(Make, "unknown", StringComparison.OrdinalIgnoreCase)
				? $"unknown ({Ordinal})"
				: $"{Make} {Model}";
		}
	}
}
