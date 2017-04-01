using LanguageExt;

namespace RfxCom.Messages
{
    public interface IMessageCodec
    {
        Option<Packet> Encode(IMessage message);
        Option<IMessage> Decode(Packet packet);

    }
}