using System.Linq;

namespace RfxCom.Messages
{
    internal class Buffer
    {
        public static byte[] Empty(byte length)
        {
            var bytes = Enumerable.Repeat<byte>(0x00, length).ToArray();
            return bytes;
        }
    }
}