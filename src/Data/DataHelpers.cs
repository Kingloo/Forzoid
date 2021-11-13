using System;

namespace Forzoid.Data
{
	internal static class DataHelpers
	{
		internal static Game DetermineGame(byte[] packet)
			=> DetermineGame(packet.AsSpan());

		internal static Game DetermineGame(ReadOnlySpan<byte> packet)
			=> packet.Length switch
			{
				0 => Game.None,
				311 => Game.ForzaMotorsport7,
				324 => Game.ForzaHorizon4,
				_ => Game.Unknown
			};

		internal static CarClass DetermineCarClass(int value)
			=> value switch
			{
				0 => CarClass.E,
				1 => CarClass.D,
				2 => CarClass.C,
				3 => CarClass.B,
				4 => CarClass.A,
				5 => CarClass.S,
				6 => CarClass.R,
				7 => CarClass.P,
				8 => CarClass.X,
				_ => CarClass.Unknown
			};

		internal static DrivetrainType DetermineDrivetrainType(int value)
			=> value switch
			{
				0 => DrivetrainType.FWD,
				1 => DrivetrainType.RWD,
				2 => DrivetrainType.AWD,
				_ => DrivetrainType.Unknown
			};

#if NETSTANDARD2_0
        internal static int ToInt32(ReadOnlySpan<byte> value) => System.BitConverter.ToInt32(value.ToArray(), 0);
        internal static ushort ToUInt16(ReadOnlySpan<byte> value) => System.BitConverter.ToUInt16(value.ToArray(), 0);
        internal static uint ToUInt32(ReadOnlySpan<byte> value) => System.BitConverter.ToUInt32(value.ToArray(), 0);
        internal static float ToSingle(ReadOnlySpan<byte> value) => System.BitConverter.ToSingle(value.ToArray(), 0);
#endif
	}
}
