namespace Forzoid.Common
{
	public interface IGame
	{
		public string FullName { get; set; }
		public string ShortName { get; set; }
		public int ReleaseDate { get; set; }
	}
}
