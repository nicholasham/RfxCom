using System;
using System.Linq;
using LanguageExt;

namespace RfxCom.Messages
{
    public interface IMessage
    {
        PacketType PacketType { get; }
    }
}