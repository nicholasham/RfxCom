namespace RfxCom.Messages
{
    public enum PacketType : byte
    {
        InterfaceControl = 0x00,
        InterfaceResponse = 0x01,
        TransceiverMessage = 0x02,
        Chime = 0x16,
        Unknown = 0xff
    }
    


}