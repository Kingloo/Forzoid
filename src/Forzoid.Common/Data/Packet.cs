using System;
using System.Net;

namespace Forzoid.Common.Fred
{
	public class Packet
	{
		public IPEndPoint? EndPoint { get; } = null;

		public Packet() { }

		public Packet(IPEndPoint endPoint)
		{
			ArgumentNullException.ThrowIfNull(endPoint);

			EndPoint = endPoint;
		}

		public static Packet Empty => new Packet();

		private static ReadOnlySpan<byte> PrepareForForzaHorizon4(ReadOnlySpan<byte> data)
		{
			/*
                courtesy RUBITS from the ForzaMotorsport.net forums

                https://forums.forzamotorsport.net/turn10_postsm1086008_Data-Output.aspx#post_1086008

					update 2023.10.02: above forum thread no longer exists

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

			byte[] fm7Sled = data[..fm7SledLength].ToArray();
			byte[] fm7Dash = data.Slice(244, fm7DashLength).ToArray();

			Array.Copy(fm7Sled, 0, contiguous, 0, fm7SledLength);
			Array.Copy(fm7Dash, 0, contiguous, fm7SledLength, fm7DashLength);

			// third argument is fm7SledLength because we put 232 bytes into contiguous starting at index 0
			// so the 232nd byte is in position 231, so the copy should start copying the next data as of index 232

			return contiguous.AsSpan();
		}
	}
}
