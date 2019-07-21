using System;
using static System.BitConverter;

namespace Forzoid.Data
{
    public class Dash
    {
        public float PositionX { get; set; } = 0f;
        public float PositionY { get; set; } = 0f;
        public float PositionZ { get; set; } = 0f;

        public float Speed { get; set; } = 0f;
        public float Power { get; set; } = 0f;
        public float Torque { get; set; } = 0f;

        public float TireTempFrontLeft { get; set; } = 0f;
        public float TireTempFrontRight { get; set; } = 0f;
        public float TireTempRearLeft { get; set; } = 0f;
        public float TireTempRearRight { get; set; } = 0f;

        public float Boost { get; set; } = 0f;
        public float Fuel { get; set; } = 0f;
        public float DistanceTraveled { get; set; } = 0f;
        public float BestLap { get; set; } = 0f;
        public float LastLap { get; set; } = 0f;
        public float CurrentLap { get; set; } = 0f;
        public float CurrentRaceTime { get; set; } = 0f;

        public int LapNumber { get; set; } = 0; // in docu. this is ushort
        public byte RacePosition { get; set; } = byte.MinValue;

        public byte Accel { get; set; } = byte.MinValue;
        public byte Brake { get; set; } = byte.MinValue;
        public byte Clutch { get; set; } = byte.MinValue;
        public byte HandBrake { get; set; } = byte.MinValue;
        public byte Gear { get; set; } = byte.MinValue;
        public sbyte Steer { get; set; } = sbyte.MinValue;

        public sbyte NormalizedDrivingLine { get; set; } = sbyte.MinValue;
        public sbyte NormalizedAIBrakeDifference { get; set; } = sbyte.MinValue;

        public Dash() { }

        public static Dash Create(ReadOnlySpan<byte> data)
        {
            if (data.Length == 0)
            {
                return null;
            }

            Dash dash = new Dash();

            dash.PositionX = ToSingle(data.Slice(232, sizeof(float)));
            dash.PositionY = ToSingle(data.Slice(236, sizeof(float)));
            dash.PositionZ = ToSingle(data.Slice(240, sizeof(float)));

            dash.Speed = ToSingle(data.Slice(244, sizeof(float)));
            dash.Power = ToSingle(data.Slice(248, sizeof(float)));
            dash.Torque = ToSingle(data.Slice(252, sizeof(float)));

            dash.TireTempFrontLeft = ToSingle(data.Slice(256, sizeof(float)));
            dash.TireTempFrontRight = ToSingle(data.Slice(260, sizeof(float)));
            dash.TireTempRearLeft = ToSingle(data.Slice(264, sizeof(float)));
            dash.TireTempRearRight = ToSingle(data.Slice(268, sizeof(float)));

            dash.Boost = ToSingle(data.Slice(272, sizeof(float)));
            dash.Fuel = ToSingle(data.Slice(276, sizeof(float)));
            dash.DistanceTraveled = ToSingle(data.Slice(280, sizeof(float)));
            dash.BestLap = ToSingle(data.Slice(284, sizeof(float)));
            dash.LastLap = ToSingle(data.Slice(288, sizeof(float)));
            dash.CurrentLap = ToSingle(data.Slice(292, sizeof(float)));
            dash.CurrentRaceTime = ToSingle(data.Slice(296, sizeof(float)));

            /*
                per the ForzaMotorsport.net forums, LapNumber is a ushort that reports lap 1 as lap 0
                
                HOWEVER!

                we can't just '+ 1' as '+' only works on ints
                so we extract it as a ushort then convert it to int - this is why the LapNumber property is an int instead of a ushort
                
                HOWEVER!

                when a race starts, before you have crossed the start/finish line
                the in-game UI will say 'laps 0/3'
                the way we do it here it will already say 'laps 1/3'
            */

            ushort lapNumber = ToUInt16(data.Slice(300, sizeof(ushort)));

            dash.LapNumber = Convert.ToInt32(lapNumber) + 1;
            dash.RacePosition = data[302];

            dash.Accel = data[303];
            dash.Brake = data[304];
            dash.Clutch = data[305];
            dash.HandBrake = data[306];
            dash.Gear = data[307];
            dash.Steer = (sbyte)data[308];

            dash.NormalizedDrivingLine = (sbyte)data[309];
            dash.NormalizedAIBrakeDifference = (sbyte)data[310];

            return dash;
        }
    }
}