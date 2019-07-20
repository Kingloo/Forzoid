using System;

namespace Forzoid.Data
{
    public static class DataHelpers
    {
        public static Game DetermineGame(byte[] packet)
            => DetermineGame(packet.AsMemory());

        public static Game DetermineGame(ReadOnlyMemory<byte> packet)
        {
            switch (packet.Length)
            {
                case 324:
                    return Game.ForzaHorizon4;
                // TODO: I don't know what this should be
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