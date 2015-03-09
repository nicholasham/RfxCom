using System.Collections.Generic;

namespace RfxCom.Messages
{

    public class Protocol : Field<Protocol>
    {
        

        public byte MessageNumber { get; private set; }

        public static Protocol AeBlyss = new Protocol(3, 0x01, "AE Blyss");
        public static Protocol Rubiscon = new Protocol(3, 0x02, "Rubiscon");
        public static Protocol FineOffset = new Protocol(3, 0x04, "Fine offset / Viking");
        public static Protocol Lighting4 = new Protocol(3, 0x08, "Lighting 4");
        public static Protocol Rsl = new Protocol(3, 0x10, "RSL");
        public static Protocol ByronSx = new Protocol(3, 0x20, "Byron SX");
        public static Protocol Rfu = new Protocol(3, 0x40, "RFU");
        public static Protocol EnableDisplayOfUndecoded = new Protocol(3, 0x80, "Enable Display Of Undecoded");

        public static Protocol Mertik = new Protocol(4, 0x01, "Mertik");
        public static Protocol AdLightwaveRf = new Protocol(4, 0x02, "AD LightwaveRF");
        public static Protocol Hideki = new Protocol(4, 0x04, "Hideki/UPM");
        public static Protocol LaCrosse = new Protocol(4, 0x08, "La Crosse");
        public static Protocol Fs20 = new Protocol(4, 0x10, "FS20");
        public static Protocol ProGuard = new Protocol(4, 0x20, "ProGuard");
        public static Protocol BlindsT0 = new Protocol(4, 0x40, "Blinds T0");
        public static Protocol BlindsT1 = new Protocol(4, 0x80, "Blinds T1");

        public static Protocol X10 = new Protocol(5, 0x01, "X10");
        public static Protocol Arc = new Protocol(5, 0x02, "ARC");
        public static Protocol Ac = new Protocol(5, 0x04, "AC");
        public static Protocol HomeEasyEu = new Protocol(5, 0x08, "HomeEasy EU");
        public static Protocol Meiantech = new Protocol(5, 0x10, "Meiantech");
        public static Protocol OregonScientific = new Protocol(5, 0x20, "Oregon Scientific");
        public static Protocol Ati = new Protocol(5, 0x40, "ATI");
        public static Protocol Visonic = new Protocol(5, 0x80, "Visonic");
        
        public Protocol(byte messageNumber, byte value, string description) : base(value, description)
        {
            MessageNumber = messageNumber;
        }
        
        

        
    }

  

}