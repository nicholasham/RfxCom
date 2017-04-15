using System.Collections.Generic;
using System.Linq;

namespace RfxCom.Messages
{
    public interface IMessage
    {
        PacketType PacketType { get; }
        byte SequenceNumber { get; }

    }
    
}