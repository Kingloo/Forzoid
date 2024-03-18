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
				// 311 => Game.ForzaMotorsport7, // can't have same switch value twice, default to FM2023
				311 => Game.ForzaMotorsport2023,
				324 => Game.ForzaHorizon4,
				_ => Game.Unknown
			};

#if NETSTANDARD2_0
        internal static int ToInt32(ReadOnlySpan<byte> value) => System.BitConverter.ToInt32(value.ToArray(), 0);
        internal static ushort ToUInt16(ReadOnlySpan<byte> value) => System.BitConverter.ToUInt16(value.ToArray(), 0);
        internal static uint ToUInt32(ReadOnlySpan<byte> value) => System.BitConverter.ToUInt32(value.ToArray(), 0);
        internal static float ToSingle(ReadOnlySpan<byte> value) => System.BitConverter.ToSingle(value.ToArray(), 0);
#endif
	}
}
