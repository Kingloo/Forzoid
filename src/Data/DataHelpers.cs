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
                case 0:
                    return Game.None;
                case 311:
                    return Game.ForzaMotorsport7;
                case 324:
                    return Game.ForzaHorizon4;
                default:
                    return Game.Unknown;
            }
        }

        public static CarClass DetermineCarClass(int value)
        {
            switch (value)
            {
                case 0:
                    return CarClass.E;
                case 1:
                    return CarClass.D;
                case 2:
                    return CarClass.C;
                case 3:
                    return CarClass.B;
                case 4:
                    return CarClass.A;
                case 5:
                    return CarClass.S;
                case 6:
                    return CarClass.R;
                case 7:
                    return CarClass.P;
                case 8:
                    return CarClass.X;
                default:
                    return CarClass.Unknown;
            }
        }

        public static DrivetrainType DetermineDrivetrainType(int value)
        {
            switch (value)
            {
                case 0:
                    return DrivetrainType.FWD;
                case 1:
                    return DrivetrainType.RWD;
                case 2:
                    return DrivetrainType.AWD;
                default:
                    return DrivetrainType.Unknown;
            }
        }
    }
}