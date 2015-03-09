namespace RfxCom.Messages
{
    public class TransceiverType : Field<TransceiverType>
    {
        public static readonly TransceiverType RfxTrx31000 = new TransceiverType(0x50, "310.00 MHZ");
        public static readonly TransceiverType RfxTrx31500 = new TransceiverType(0x51, "315.00 MHZ");
        public static readonly TransceiverType RfxRec43392 = new TransceiverType(0x52, "RFX 433.92 MHZ Receiver Only");
        public static readonly TransceiverType RfxTrx43392 = new TransceiverType(0x53, "RFX 433.92 MHZ Transceiver");
        public static readonly TransceiverType RfxTrx43342 = new TransceiverType(0x54, "433.42 MHZ");
        public static readonly TransceiverType RfxTrx86800 = new TransceiverType(0x55, "868.00 MHZ");
        public static readonly TransceiverType RfxTrx86800Fsk = new TransceiverType(0x56, "868.00 MHZ FSK");
        public static readonly TransceiverType RfxTrx86830 = new TransceiverType(0x57, "868.30 MHZ");
        public static readonly TransceiverType RfxTrx86830Fsk = new TransceiverType(0x58, "868.30 MHZ FSK");
        public static readonly TransceiverType RfxTrx86835 = new TransceiverType(0x59, "868.35 MHZ");
        public static readonly TransceiverType RfxTrx86835Fsk = new TransceiverType(0x5A, "868.35 MHZ FSK");
        public static readonly TransceiverType RfxTrx86895 = new TransceiverType(0x5B, "868.95 MHZ");
        public static readonly TransceiverType Default = RfxRec43392;

        private TransceiverType(byte value, string description) : base(value, description)
        {
        }
    }
}