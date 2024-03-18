namespace Forzoid.Common
{
	public interface ICar
	{
		string Name { get; set; }
		string Manufacturer { get; set; }
		ICarClass CarClass { get; set; }
		Drivetrain Drivetrain { get; set; }
	}
}