using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Forzoid.Data
{
	public class Packet
	{
		public IPEndPoint? EndPoint { get; } = null;
		public Game Game { get; set; } = Game.None;
		public Sled? Sled { get; set; } = null;
		public Dash? Dash { get; set; } = null;

		public Packet() { }

		public Packet(IPEndPoint endPoint)
		{
			EndPoint = endPoint;
		}

		public static Packet Empty => new Packet();

		public static bool TryCreate(ReadOnlyMemory<byte> data, IPEndPoint endPoint, [NotNullWhen(true)] out Packet? packet)
		{
			if (data.Length == 0)
			{
				packet = null;
				return false;
			}

			Game game = DataHelpers.DetermineGame(data.Span);

			ReadOnlySpan<byte> adjusted = PreparePacket(game, data.Span);

			if (Sled.Create(adjusted) is Sled sled
				&& Dash.Create(adjusted) is Dash dash)
			{
				packet = new Packet(endPoint)
				{
					Game = game,
					Sled = sled,
					Dash = dash
				};

				return true;
			}

			packet = null;
			return false;
		}

		private static ReadOnlySpan<byte> PreparePacket(Game game, ReadOnlySpan<byte> data)
			=> game switch
			{
				Game.ForzaHorizon4 => PrepareForForzaHorizon4(data),
				Game.ForzaHorizon5 => throw new NotImplementedException("Forza Horizon 5 is not supported (yet)"),
				Game.ForzaMotorsport7 => PrepareForForzaMotorsport7(data),
				_ => ReadOnlySpan<byte>.Empty
			};

		private static ReadOnlySpan<byte> PrepareForForzaMotorsport7(ReadOnlySpan<byte> data)
		{
			// a Forza Motorsport 7 packet is contiguous, no unknown data and no gaps

			return data;
		}

		private static ReadOnlySpan<byte> PrepareForForzaHorizon4(ReadOnlySpan<byte> data)
		{
			/*
                courtesy RUBITS from the ForzaMotorsport.net forums

                https://forums.forzamotorsport.net/turn10_postsm1086008_Data-Output.aspx#post_1086008

                a Forza Horizon 4 packet has some unknown data

                [0]-[231] FM7 Sled data (as mentioned before)
                [232]-[243] FH4 new unknown data
					[232]->[235]
						changes when you change car, not individual car ID or manufacturer
						some manufacturers' cars have same ID, some cars from different manufacturers have same number
					[236]->[239] and [240]->[243]
						environmental damage (e.g. trees, walls) but NOT other cars
						when parsed as floats gives values between 0.0 and 1.0
                [244]-[322] FM7 Car Dash data
                [323] FH4 new unknown data
					has always been zero
            */

			int fm7SledLength = 232;
			int fm7DashLength = 79;

			byte[] contiguous = new byte[fm7SledLength + fm7DashLength];

			byte[] fm7Sled = data.Slice(0, fm7SledLength).ToArray();
			byte[] fm7Dash = data.Slice(244, fm7DashLength).ToArray();

			Array.Copy(fm7Sled, 0, contiguous, 0, fm7SledLength);
			Array.Copy(fm7Dash, 0, contiguous, fm7SledLength, fm7DashLength);

			// third argument is fm7SledLength because we put 232 bytes into contiguous starting at index 0
			// so the 232nd byte is in position 231, so the copy should start copying the next data as of index 232

			return contiguous.AsSpan();
		}
	}
}
