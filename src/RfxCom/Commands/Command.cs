using System.Collections.Generic;

namespace RfxCom.Commands
{
    public abstract class Command
    {
        public byte SequenceNumber { get; protected set; }
        public abstract IEnumerable<byte> ToBytes();
    }
}