using System.Collections.Generic;

namespace Forzoid.ForzaMotorsport2023
{
	internal static class FM2023Data
	{
		internal readonly static IDictionary<int, FM2023Car> Cars = new Dictionary<int, FM2023Car>
		{
			{ 247,	new FM2023Car(247,	"Toyota", "2000GT", 1969) },
			{ 316,	new FM2023Car(316,	"Lamborghini", "Countach LP5000 QV", 1988) },
			{ 323,	new FM2023Car(323,	"Lancia", "Delta HF Integrale EVO", 1992) },
			{ 445,	new FM2023Car(445,	"Nissan", "Skyline GT-R V-Spec II", 2002) },
			{ 1171,	new FM2023Car(1171,	"Ferrari", "599XX", 2010) },
			{ 2034,	new FM2023Car(2034,	"Ferrari", "LaFerrari", 2013) },
			{ 2038,	new FM2023Car(2038,	"Alfa Romeo", "4C", 2014) },
			{ 2103,	new FM2023Car(2103,	"BMW", "#6 M1 Procar", 1979) },
			{ 2184,	new FM2023Car(2184,	"Ferrari", "458 S", 0) },
			{ 2968,	new FM2023Car(2968,	"Aston Martin", "Valkyrie", 2023) },
			{ 3724,	new FM2023Car(3724,	"Ferrari", "296 GTB", 2022) }
		};

		internal readonly static IDictionary<int, FM2023Track> Tracks = new Dictionary<int, FM2023Track>
		{
			{ 0,	new FM2023Track(0,		"Laguna Seca", "Full Circuit") },
			{ 5,	new FM2023Track(5,		"Road America", "East Route") },
			{ 21,	new FM2023Track(21,		"Silverstone Racing Circuit", "Grand Prix Circuit") },
			{ 22,	new FM2023Track(22,		"Silverstone Racing Circuit", "National Circuit") },
			{ 23,	new FM2023Track(23,		"Silverstone Racing Circuit", "International Circuit") },
			{ 32,	new FM2023Track(32,		"Nürburgring", "Nordschleife") },
			{ 33,	new FM2023Track(33,		"Nürburgring", "Grand Prix Circuit") },
			{ 34,	new FM2023Track(34,		"Nürburgring", "Sprint Circuit") },
			{ 35,	new FM2023Track(35,		"Mugello Circuit", "Full Circuit") },
			{ 37,	new FM2023Track(37,		"Suzuka Circuit", "Full Circuit") },
			{ 38,	new FM2023Track(38,		"Suzuka Circuit", "East Circuit") },
			{ 67,	new FM2023Track(67,		"Maple Valley", "Full Circuit") },
			{ 68,	new FM2023Track(68,		"Maple Valley", "Short Circuit") },
			{ 100,	new FM2023Track(100,	"Le Mans - Circuit International de la Sarthe", "") },
			{ 110,	new FM2023Track(110,	"Circuit de Barcelona-Catalunya", "Grand Prix Circuit") },
			{ 232,	new FM2023Track(232,	"Indianapolis Motor Speedway", "Grand Prix Circuit") },
			{ 250,	new FM2023Track(250,	"Hockenheimring", "Full Circuit") },
			{ 251,	new FM2023Track(251,	"Hockenheimring", "National Circuit") },
			{ 252,	new FM2023Track(252,	"Hockenheimring", "Short Circuit") },
			{ 510,	new FM2023Track(510,	"Yas Marina Circuit", "Full Circuit") },
			{ 511,	new FM2023Track(511,	"Yas Marina Circuit", "North Circuit") },
			{ 512,	new FM2023Track(512,	"Yas Marina Circuit", "South") },
			{ 530,	new FM2023Track(530,	"Circuit de Spa-Francorchamps", "Full Circuit") },
			{ 840,	new FM2023Track(840,	"Daytona International Speedway", "Sports Car Circuit") },
			{ 870,	new FM2023Track(870,	"Watkins Glen International Speedway", "Full Circuit") },
			{ 873,	new FM2023Track(873,	"Watkins Glen International Speedway", "Short Circuit") },
			{ 880,	new FM2023Track(880,	"Lime Rock Park", "Full Circuit") },
			{ 882,	new FM2023Track(882,	"Lime Rock Park", "South Chicane") },
			{ 883,	new FM2023Track(883,	"Lime Rock Park", "Full Circuit (Alt)") },
			{ 990,	new FM2023Track(990,	"Virginia International Raceway", "Full") },
			{ 991,	new FM2023Track(991,	"Virginia International Raceway", "North") },
			{ 992,	new FM2023Track(992,	"Virginia International Raceway", "South") },
			{ 1110,	new FM2023Track(1110,	"Homestead-Miami Speedway", "Full Circuit") },
			{ 1111,	new FM2023Track(1111,	"Homestead-Miami Speedway", "Road Circuit") },
			{ 1452,	new FM2023Track(1452,	"Mid-Ohio Sports Car Course", "Short Circuit") },
			{ 1590,	new FM2023Track(1590,	"Kyalami Grand Prix Circuit", "Grand Prix Circuit") },
			{ 1631,	new FM2023Track(1631,	"Grank Oak Raceway", "Club Circuit") },
			{ 1640,	new FM2023Track(1640,	"Hakone", "Grand Prix Circuit") },
			{ 1643,	new FM2023Track(1643,	"Hakone", "Club Circuit Reverse") },
			{ 1660,	new FM2023Track(1660,	"Eaglerock Speedway", "Oval Circuit") }
		};
	}
}
