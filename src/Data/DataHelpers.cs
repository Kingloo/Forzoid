using System;

namespace Forzoid.Data
{
    public static class DataHelpers
    {
        public static Game DetermineGame(byte[] packet)
            => DetermineGame(packet.AsSpan());

        public static Game DetermineGame(ReadOnlySpan<byte> packet)
        {
            switch (packet.Length)
            {
                case 324:
                    return Game.ForzaHorizon4;
                // case xxx:
                //     return Game.ForzaMotorsport7;
                case 0:
                    return Game.None;
                default:
                    return Game.Unknown;
            }
        }
    }
}