using LanguageExt;

namespace RfxCom.Messages
{
    public interface IMessageEncoder
    {
        Option<byte[]> Encode(IMessage message);
    }
}