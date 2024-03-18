using System;

namespace Forzoid.Common
{
	public class Game : IGame
	{
		public string FullName { get; init; }
		public string ShortName { get; init; }
		public int ReleaseYear { get; init; }

		public Game(string fullName, string shortName, int releaseYear)
		{
			ArgumentNullException.ThrowIfNull(fullName);
			ArgumentNullException.ThrowIfNull(shortName);
			ArgumentOutOfRangeException.ThrowIfNegativeOrZero(releaseYear);

			FullName = fullName;
			ShortName = shortName;
			ReleaseYear = releaseYear;
		}
	}
}