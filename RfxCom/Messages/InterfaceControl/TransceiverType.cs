namespace RfxCom.Messages.InterfaceControl
{
    public enum TransceiverType : byte
    {
        RfxTrx31000 = 0x50,
        RfxTrx31500 = 0x51,
        RfxRec43392 = 0x52,
        RfxTrx43392 = 0x53,
        RfxTrx43342 = 0x54,
        RfxTrx86800 = 0x55,
        RfxTrx86800Fsk = 0x56,
        RfxTrx86830 = 0x57,
        RfxTrx86830Fsk = 0x58,
        RfxTrx86835 = 0x59,
        RfxTrx86835Fsk = 0x5A,
        RfxTrx86895 = 0x5B
    }
}