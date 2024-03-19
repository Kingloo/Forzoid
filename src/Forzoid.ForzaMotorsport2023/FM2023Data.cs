using System.Collections.Generic;

namespace Forzoid.ForzaMotorsport2023
{
	internal static class FM2023Data
	{
		internal readonly static IDictionary<int, FM2023Car> Cars = new Dictionary<int, FM2023Car>
		{
			{ 247,	new FM2023Car(247,	"Toyota", "2000GT", 1969) },
			{ 316,	new FM2023Car(316,	"Lamborghini", "Countach LP5000 QV", 1988) },
			{ 1171,	new FM2023Car(1171,	"Ferrari", "599XX", 2010) },
			{ 2038,	new FM2023Car(2038,	"Alfa Romeo", "4C", 2014) },
			{ 2184,	new FM2023Car(2184,	"Ferrari", "458 S", 0) },
			{ 2968,	new FM2023Car(2968,	"Aston Martin", "Valkyrie", 2023) }
		};

		internal readonly static IDictionary<int, FM2023Track> Tracks = new Dictionary<int, FM2023Track>
		{
			{ 0,	new FM2023Track(0,		"Laguna Seca", "Full Circuit") },
			{ 5,	new FM2023Track(5,		"Road America", "East Route") },
			{ 32,	new FM2023Track(32,		"Nürburgring", "Nordschleife") },
			{ 38,	new FM2023Track(38,		"Suzuka Circuit", "East Circuit") },
			{ 67,	new FM2023Track(67,		"Maple Valley", "Full Circuit") },
			{ 68,	new FM2023Track(68,		"Maple Valley", "Short Circuit") },
			{ 100,	new FM2023Track(100,	"Le Mans - Circuit International de la Sarthe", "") },
			{ 110,	new FM2023Track(110,	"Circuit de Barcelona-Catalunya", "") },
			{ 250,	new FM2023Track(250,	"Hockenheimring", "Full Circuit") },
			{ 252,	new FM2023Track(252,	"Hockenheimring", "Short Circuit") },
			{ 512,	new FM2023Track(512,	"Yas Marina Circuit", "South") },
			{ 530,	new FM2023Track(530,	"Circuit de Spa-Francorchamps", "Full Circuit") },
			{ 840,	new FM2023Track(840,	"Daytona International Speedway", "Sports Car") },
			{ 870,	new FM2023Track(870,	"Watkins Glen International Speedway", "Full Circuit") },
			{ 883,	new FM2023Track(883,	"Lime Rock Park", "Full Circuit (Alt)") },
			{ 991,	new FM2023Track(991,	"Virginia International Raceway", "North") },
			{ 992,	new FM2023Track(992,	"Virginia International Raceway", "South") },
			// { 992,	new FM2023Track(992,	"Silverstone Racing Circuit", "International Circuit") },
			{ 1110,	new FM2023Track(1110,	"Homestead-Miami Speedway", "Full Circuit") },
			{ 1111,	new FM2023Track(1111,	"Homestead-Miami Speedway", "Road Circuit") },
			{ 1590,	new FM2023Track(1590,	"Kyalami Grand Prix Circuit", "Grand Prix Circuit") },
			{ 1631,	new FM2023Track(1631,	"Grank Oak Raceway", "Club Circuit") }
		};
	}
}
