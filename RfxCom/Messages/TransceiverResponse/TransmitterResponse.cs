namespace RfxCom.Messages.TransceiverResponse
{
    public enum TransmitterResponse : byte
    {
        Ok = 0x00,
        OkButDelay = 0x01,
        DidNotLock = 0x02,
        AcAddressZero = 0x03
    }
}