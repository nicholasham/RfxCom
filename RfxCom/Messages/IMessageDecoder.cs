using LanguageExt;

namespace RfxCom.Messages
{
    public interface IMessageDecoder
    {
        bool CanDecode(Packet packet);
        Option<IMessage> Decode(Packet packet);
    }
}