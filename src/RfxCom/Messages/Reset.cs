using System;
using System.Linq;

namespace RfxCom.Messages
{
    public class Reset : Message
    {
        public override byte[] ToBytes()
        {
            return new byte[] { 0x0D, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        }
    }
}