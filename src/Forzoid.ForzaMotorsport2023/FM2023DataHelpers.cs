using Forzoid.Common;

namespace Forzoid.ForzaMotorsport2023
{
	internal static class FM2023DataHelpers
	{
		internal static FM2023CarClass DetermineCarClass(int value)
		{
			return value switch
			{
				0 => FM2023CarClass.E,
				1 => FM2023CarClass.D,
				2 => FM2023CarClass.C,
				3 => FM2023CarClass.B,
				4 => FM2023CarClass.A,
				5 => FM2023CarClass.S,
				6 => FM2023CarClass.R,
				7 => FM2023CarClass.P,
				8 => FM2023CarClass.X,
				_ => FM2023CarClass.Unknown
			};
		}

		internal static Drivetrain DetermineDrivetrain(int value)
		{
			return value switch
			{
				0 => Drivetrain.FWD,
				1 => Drivetrain.RWD,
				2 => Drivetrain.AWD,
				_ => Drivetrain.Unknown
			};
		}
	}
}