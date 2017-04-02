using System.Collections.Generic;
using System.Linq;
using LanguageExt;

namespace RfxCom.Messages.InterfaceControl
{
    public static class Protocols
    {
        private static IDictionary<Protocol, byte> Message3Protocols => new Dictionary<Protocol, byte>
        {
            {Protocol.AeBlyss, 0x01},
            {Protocol.Rubiscon, 0x02},
            {Protocol.FineOffset, 0x04},
            {Protocol.Lighting4, 0x08},
            {Protocol.Rsl, 0x10},
            {Protocol.ByronSx, 0x20},
            {Protocol.Rfu, 0x40},
            {Protocol.EnableDisplayOfUndecoded, 0x80}
        };

        private static IDictionary<Protocol, byte> Message4Protocols => new Dictionary<Protocol, byte>
        {
            {Protocol.Mertik, 0x01},
            {Protocol.AdLightwaveRf, 0x02},
            {Protocol.Hideki, 0x04},
            {Protocol.LaCrosse, 0x08},
            {Protocol.Fs20, 0x10},
            {Protocol.ProGuard, 0x20},
            {Protocol.BlindsT0, 0x40},
            {Protocol.BlindsT1, 0x80}
        };

        private static IDictionary<Protocol, byte> Message5Protocols => new Dictionary<Protocol, byte>
        {
            {Protocol.X10, 0x01},
            {Protocol.Arc, 0x02},
            {Protocol.Ac, 0x04},
            {Protocol.HomeEasyEu, 0x08},
            {Protocol.Meiantech, 0x10},
            {Protocol.OregonScientific, 0x20},
            {Protocol.Ati, 0x40},
            {Protocol.Visonic, 0x80}
        };

        public static byte EncodeMessage3(this Protocol[] protocols)
        {
            return (byte) protocols.Select(Message3Protocols.TryGetValue).Somes().Sum(x => x);
        }

        public static byte EncodeMessage4(this Protocol[] protocols)
        {
            return (byte)protocols.Select(Message4Protocols.TryGetValue).Somes().Sum(x => x);
        }

        public static byte EncodeMessage5(this Protocol[] protocols)
        {
            return (byte)protocols.Select(Message5Protocols.TryGetValue).Somes().Sum(x => x);
        }

        public static Protocol[] DecodeMessage3(byte value)
        {
            return Message3Protocols.Where(pair => (pair.Value & value) != 0).Select(pair => pair.Key).ToArray();
        }

        public static Protocol[] DecodeMessage4(byte value)
        {
            return Message4Protocols.Where(pair => (pair.Value & value) != 0).Select(pair => pair.Key).ToArray();
        }

        public static Protocol[] DecodeMessage5(byte value)
        {
            return Message5Protocols.Where(pair => (pair.Value & value) != 0).Select(pair => pair.Key).ToArray();
        }
    }
}