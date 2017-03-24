using LanguageExt;

namespace RfxCom.Messages.Chimes
{
    internal class ChimeEncoder : IMessageEncoder
    {
        public Option<byte[]> Encode(IMessage message)
        {
            return Option<byte[]>.None;
        }
    }
}