namespace Forzoid.Common
{
	public interface IGame
	{
		string FullName { get; }
		string ShortName { get; }
		int ReleaseYear { get; }
	}
}
