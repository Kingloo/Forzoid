using System;

namespace Forzoid.Common
{
	public class Game : IGame
	{
		public string FullName { get; }
		public string ShortName { get; }
		public int ReleaseYear { get; }

		public Game(string fullName, string shortName, int releaseYear)
		{
			if (fullName is null)
			{
				throw new ArgumentNullException(nameof(fullName));
			}
			
			if (shortName is null)
			{
				throw new ArgumentNullException(nameof(shortName));
			}

			if (releaseYear < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(releaseYear));
			}

			FullName = fullName;
			ShortName = shortName;
			ReleaseYear = releaseYear;
		}
	}
}
