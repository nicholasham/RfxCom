using System;

namespace RfxCom.Messages
{
    public static class ByteExtensions
    {
     
        public static string Dump(this byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }

        public static byte Next(this byte value)
        {
            if (value == byte.MaxValue)
            {
                return 0;
            }

            return Convert.ToByte(value + 1);
        }
    }
}