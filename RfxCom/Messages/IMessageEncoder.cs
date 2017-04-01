using LanguageExt;

namespace RfxCom.Messages
{
    public interface IMessageEncoder
    {
        bool CanEncode(IMessage message);
        Option<Packet> Encode(IMessage message);
    }
}