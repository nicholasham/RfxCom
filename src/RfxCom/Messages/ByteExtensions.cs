using System;
using System.Collections.Generic;
using System.Linq;

namespace RfxCom.Messages
{
    public static class ByteExtensions
    {
     
        public static string Dump(this IEnumerable<byte> bytes)
        {
            return BitConverter.ToString(bytes.ToArray());
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