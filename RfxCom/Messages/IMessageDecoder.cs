using LanguageExt;

namespace RfxCom.Messages
{
    public interface IMessageDecoder
    {
        Option<IMessage> Decode(byte[] data);
    }
}