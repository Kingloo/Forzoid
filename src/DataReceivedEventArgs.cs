using System;
using Forzoid.Data;

namespace Forzoid
{
    public class DataReceivedEventArgs : EventArgs
    {
        public Packet Packet { get; }

        public DataReceivedEventArgs(Packet packet)
        {
            Packet = packet ?? throw new ArgumentNullException(nameof(packet));
        }
    }
}