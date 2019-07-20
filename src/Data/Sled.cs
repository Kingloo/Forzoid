using System;
using static System.BitConverter;

namespace Forzoid.Data
{
    public class Sled
    {
        public int IsRaceOn { get; set; } = 0;
        
        public uint TimestampMS { get; set; } = 0;
        
        public float EngineMaxRpm { get; set; } = 0f;
        public float EngineIdleRpm { get; set; } = 0f;
        public float CurrentEngineRpm { get; set; } = 0f;
        
        public float AccelerationX { get; set; } = 0f;
        public float AccelerationY { get; set; } = 0f;
        public float AccelerationZ { get; set; } = 0f;
        
        public float VelocityX { get; set; } = 0f;
        public float VelocityY { get; set; } = 0f;
        public float VelocityZ { get; set; } = 0f;

        public float AngularVelocityX { get; set; } = 0f;
        public float AngularVelocityY { get; set; } = 0f;
        public float AngularVelocityZ { get; set; } = 0f;
        
        public float Yaw { get; set; } = 0f;
        public float Pitch { get; set; } = 0f;
        public float Roll { get; set; } = 0f;
        
        public float NormalizedSuspensionTravelFrontLeft { get; set; } = 0f;
        public float NormalizedSuspensionTravelFrontRight { get; set; } = 0f;
        public float NormalizedSuspensionTravelRearLeft { get; set; } = 0f;
        public float NormalizedSuspensionTravelRearRight { get; set; } = 0f;
        
        public float TireSlipRatioFrontLeft { get; set; } = 0f;
        public float TireSlipRatioFrontRight { get; set; } = 0f;
        public float TireSlipRatioRearLeft { get; set; } = 0f;
        public float TireSlipRatioRearRight { get; set; } = 0f;
        
        public float WheelRotationSpeedFrontLeft { get; set; } = 0f;
        public float WheelRotationSpeedFrontRight { get; set; } = 0f;
        public float WheelRotationSpeedRearLeft { get; set; } = 0f;
        public float WheelRotationSpeedRearRight { get; set; } = 0f;
        
        public int WheelOnRumbleStripFrontLeft { get; set; } = 0;
        public int WheelOnRumbleStripFrontRight { get; set; } = 0;
        public int WheelOnRumbleStripRearLeft { get; set; } = 0;
        public int WheelOnRumbleStripRearRight { get; set; } = 0;
        
        public float WheelInPuddleDepthFrontLeft { get; set; } = 0f;
        public float WheelInPuddleDepthFrontRight { get; set; } = 0f;
        public float WheelInPuddleDepthRearLeft { get; set; } = 0f;
        public float WheelInPuddleDepthRearRight { get; set; } = 0f;
        
        public float SurfaceRumbleFrontLeft { get; set; } = 0f;
        public float SurfaceRumbleFrontRight { get; set; } = 0f;
        public float SurfaceRumbleRearLeft { get; set; } = 0f;
        public float SurfaceRumbleRearRight { get; set; } = 0f;
        
        public float TireSlipAngleFrontLeft { get; set; } = 0f;
        public float TireSlipAngleFrontRight { get; set; } = 0f;
        public float TireSlipAngleRearLeft { get; set; } = 0f;
        public float TireSlipAngleRearRight { get; set; } = 0f;
        
        public float TireCombinedSlipFrontLeft { get; set; } = 0f;
        public float TireCombinedSlipFrontRight { get; set; } = 0f;
        public float TireCombinedSlipRearLeft { get; set; } = 0f;
        public float TireCombinedSlipRearRight { get; set; } = 0f;
        
        public float SuspensionTravelMetersFrontLeft { get; set; } = 0f;
        public float SuspensionTravelMetersFrontRight { get; set; } = 0f;
        public float SuspensionTravelMetersRearLeft { get; set; } = 0f;
        public float SuspensionTravelMetersRearRight { get; set; } = 0f;
        
        public int CarOrdinal { get; set; } = 0;
        public int CarClass { get; set; } = 0;
        public int CarPerformanceIndex { get; set; } = 0;
        public int DrivetrainType { get; set; } = 0;
        public int NumCylinders { get; set; } = 0;


        public Sled() { }

        public static Sled Create(ReadOnlySpan<byte> data)
        {
            // Future: C# 8.0 range operator
            //    span[0..sizeof(int)]
            //    span[4..sizeof(int)]
            //    span[8..sizeof(float)]
            // etc...

            if (data.Length == 0)
            {
                return null;
            }

            Sled packet = new Sled();

            packet.IsRaceOn = ToInt32(data.Slice(0, sizeof(int)));
            packet.TimestampMS = ToUInt32(data.Slice(4, sizeof(int)));

            packet.EngineMaxRpm = ToSingle(data.Slice(8, sizeof(float)));
            packet.EngineIdleRpm = ToSingle(data.Slice(12, sizeof(float)));
            packet.CurrentEngineRpm = ToSingle(data.Slice(16, sizeof(float)));

            packet.AccelerationX = ToSingle(data.Slice(20, sizeof(float)));
            packet.AccelerationY = ToSingle(data.Slice(24, sizeof(float)));
            packet.AccelerationZ = ToSingle(data.Slice(28, sizeof(float)));

            packet.VelocityX = ToSingle(data.Slice(32, sizeof(float)));
            packet.VelocityY = ToSingle(data.Slice(36, sizeof(float)));
            packet.VelocityZ = ToSingle(data.Slice(40, sizeof(float)));

            packet.AngularVelocityX = ToSingle(data.Slice(44, sizeof(float)));
            packet.AngularVelocityY = ToSingle(data.Slice(48, sizeof(float)));
            packet.AngularVelocityZ = ToSingle(data.Slice(52, sizeof(float)));

            packet.Yaw = ToSingle(data.Slice(56, sizeof(float)));
            packet.Pitch = ToSingle(data.Slice(62, sizeof(float)));
            packet.Roll = ToSingle(data.Slice(66, sizeof(float)));

            packet.NormalizedSuspensionTravelFrontLeft = ToSingle(data.Slice(70, sizeof(float)));
            packet.NormalizedSuspensionTravelFrontRight = ToSingle(data.Slice(74, sizeof(float)));
            packet.NormalizedSuspensionTravelRearLeft = ToSingle(data.Slice(78, sizeof(float)));
            packet.NormalizedSuspensionTravelRearRight = ToSingle(data.Slice(82, sizeof(float)));
            
            packet.TireSlipRatioFrontLeft = ToSingle(data.Slice(86, sizeof(float)));
            packet.TireSlipRatioFrontRight = ToSingle(data.Slice(90, sizeof(float)));
            packet.TireSlipRatioRearLeft = ToSingle(data.Slice(94, sizeof(float)));
            packet.TireSlipRatioRearRight = ToSingle(data.Slice(98, sizeof(float)));
            
            packet.WheelRotationSpeedFrontLeft = ToSingle(data.Slice(102, sizeof(float)));
            packet.WheelRotationSpeedFrontRight = ToSingle(data.Slice(106, sizeof(float)));
            packet.WheelRotationSpeedRearLeft = ToSingle(data.Slice(110, sizeof(float)));
            packet.WheelRotationSpeedRearRight = ToSingle(data.Slice(114, sizeof(float)));
            
            packet.WheelOnRumbleStripFrontLeft = ToInt32(data.Slice(118, sizeof(int)));
            packet.WheelOnRumbleStripFrontRight = ToInt32(data.Slice(122, sizeof(int)));
            packet.WheelOnRumbleStripRearLeft = ToInt32(data.Slice(126, sizeof(int)));
            packet.WheelOnRumbleStripRearRight = ToInt32(data.Slice(130, sizeof(int)));
            
            packet.WheelInPuddleDepthFrontLeft = ToSingle(data.Slice(134, sizeof(float)));
            packet.WheelInPuddleDepthFrontRight = ToSingle(data.Slice(138, sizeof(float)));
            packet.WheelInPuddleDepthRearLeft = ToSingle(data.Slice(142, sizeof(float)));
            packet.WheelInPuddleDepthRearRight = ToSingle(data.Slice(146, sizeof(float)));
            
            packet.SurfaceRumbleFrontLeft = ToSingle(data.Slice(150, sizeof(float)));
            packet.SurfaceRumbleFrontRight = ToSingle(data.Slice(154, sizeof(float)));
            packet.SurfaceRumbleRearLeft = ToSingle(data.Slice(158, sizeof(float)));
            packet.SurfaceRumbleRearRight = ToSingle(data.Slice(162, sizeof(float)));
            
            packet.TireSlipAngleFrontLeft = ToSingle(data.Slice(166, sizeof(float)));
            packet.TireSlipAngleFrontRight = ToSingle(data.Slice(170, sizeof(float)));
            packet.TireSlipAngleRearLeft = ToSingle(data.Slice(174, sizeof(float)));
            packet.TireSlipAngleRearRight = ToSingle(data.Slice(178, sizeof(float)));
            
            packet.TireCombinedSlipFrontLeft = ToSingle(data.Slice(182, sizeof(float)));
            packet.TireCombinedSlipFrontRight = ToSingle(data.Slice(186, sizeof(float)));
            packet.TireCombinedSlipRearLeft = ToSingle(data.Slice(190, sizeof(float)));
            packet.TireCombinedSlipRearRight = ToSingle(data.Slice(194, sizeof(float)));
            
            packet.SuspensionTravelMetersFrontLeft = ToSingle(data.Slice(198, sizeof(float)));
            packet.SuspensionTravelMetersFrontRight = ToSingle(data.Slice(202, sizeof(float)));
            packet.SuspensionTravelMetersRearLeft = ToSingle(data.Slice(204, sizeof(float)));
            packet.SuspensionTravelMetersRearRight = ToSingle(data.Slice(208, sizeof(float)));

            packet.CarOrdinal = ToInt32(data.Slice(212, sizeof(int)));
            packet.CarClass = ToInt32(data.Slice(216, sizeof(int)));
            packet.CarPerformanceIndex = ToInt32(data.Slice(220, sizeof(int)));
            packet.DrivetrainType = ToInt32(data.Slice(224, sizeof(int)));
            packet.NumCylinders = ToInt32(data.Slice(228, sizeof(int)));

            return packet;
        }
    }
}
