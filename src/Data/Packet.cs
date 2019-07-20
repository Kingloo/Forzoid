using System;
using System.Net;

namespace Forzoid.Data
{
    public class Packet
    {
        public IPEndPoint EndPoint { get; }
        public Game Game { get; set; } = Game.None;
        public Sled Sled { get; set; }
        public Dash Dash { get; set; }

        public Packet(IPEndPoint endPoint)
            : this(Game.None, endPoint) { }
        
        public Packet(Game game, IPEndPoint endPoint)
        {
            Game = game;
            EndPoint = endPoint ?? throw new ArgumentNullException(nameof(endPoint));
        }

        public static bool TryCreate(ReadOnlyMemory<byte> data, IPEndPoint endPoint, out Packet packet)
        {
            if (data.Length == 0)
            {
                packet = null;
                return false;
            }

            Game game = DataHelpers.DetermineGame(data);

            ReadOnlyMemory<byte> adjusted = Prepare(data, game);

            if (Sled.Create(adjusted) is Sled sled
                && Dash.Create(adjusted) is Dash dash)
            {
                packet = new Packet(game, endPoint);
                return true;    
            }

            packet = null;
            return false;
        }

        private static ReadOnlyMemory<byte> Prepare(ReadOnlyMemory<byte> data, Game game)
        {
            switch (game)
            {
                case Game.ForzaHorizon4:
                    return PrepareForForzaHorizon4(data);
                case Game.ForzaMotorsport7:
                    return PrepareForForzaMotorsport7(data);
                default:
                    return ReadOnlyMemory<byte>.Empty;
            }
        }

        private static ReadOnlyMemory<byte> PrepareForForzaMotorsport7(ReadOnlyMemory<byte> data)
        {
            // a Forza Motorsport 7 packet is contiguous, no unknown data and no gaps

            return data;
        }

        private static ReadOnlyMemory<byte> PrepareForForzaHorizon4(ReadOnlyMemory<byte> data)
        {
            /*
            
            courtesy RUBITS from the ForzaMotorsport.net forums

            https://forums.forzamotorsport.net/turn10_postsm1086008_Data-Output.aspx#post_1086008
            
            a Forza Horizon 4 packet has some unknown data

            [0]-[231] FM7 Sled data (as mentioned before)
            [232]-[243] FH4 new unknown data
            [244]-[322] FM7 Car Dash data
            [323] FH4 new unknown data
            
            !!! We are throwing away the unknown data as it is, well, unknown.
            This can be revisited when/if a full packet format for FH4 is published.

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

            return contiguous.AsMemory();
        }
    }
}