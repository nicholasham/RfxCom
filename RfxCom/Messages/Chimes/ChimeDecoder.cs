using LanguageExt;

namespace RfxCom.Messages.Chimes
{
    internal class ChimeDecoder : IMessageDecoder
    {
        public Option<IMessage> Decode(byte[] data)
        {
            return Option<IMessage>.None;
        }
    }
}