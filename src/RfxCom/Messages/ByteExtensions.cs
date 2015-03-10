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

    }
}