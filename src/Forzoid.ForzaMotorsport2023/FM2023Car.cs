using System;

namespace Forzoid.ForzaMotorsport2023
{
	public class FM2023Car
	{
		public int Ordinal { get; }
		public string Make { get; }
		public string Model { get; }
		public int ReleaseYear { get; }

		public FM2023Car(int ordinal, string make, string model, int releaseYear)
		{
			if (ordinal < -1)
			{
				throw new ArgumentOutOfRangeException(nameof(ordinal));
			}

			if (make is null)
			{
				throw new ArgumentNullException(nameof(make));
			}

			if (model is null)
			{
				throw new ArgumentNullException(nameof(model));
			}

			if (releaseYear < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(releaseYear));
			}

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
