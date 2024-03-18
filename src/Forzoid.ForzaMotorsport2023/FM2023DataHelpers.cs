using System.Globalization;
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

		internal static string DetermineCarName(int value)
		{
			// 

			return value switch
			{
				247 => "Toyota 2000GT (1969)",
				316 => "Lamborghini Countach LP5000 QV (1988)",
				2038 => "Alfa Romeo 4C (2014)",
				_ => value.ToString(CultureInfo.InvariantCulture)
			};
		}

		internal static string DetermineTrackName(int value)
		{
			// 
			
			return value switch
			{
				0 => "Laguna Seca",
				67 => "Maple Valley",
				68 => "Maple Valley (Short)",
				250 => "Hockenheimring",
				252 => "Hockenheimring (Short)",
				530 => "Circuit de Spa-Francorchamps",
				870 => "Watkins Glen International Speedway",
				883 => "Lime Rock Park (Alt)",
				991 => "Virginia International Raceway (North)",
				1110 => "Homestead-Miami Speedway",
				1111 => "Homestead-Miami Speedway (Road)",
				1590 => "Kyalami Grand Prix Circuit",
				1631 => "Grank Oak Raceway (Club)",
				_ => value.ToString(CultureInfo.InvariantCulture)
			};
		}
	}
}