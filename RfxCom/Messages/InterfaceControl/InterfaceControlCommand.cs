namespace RfxCom.Messages.InterfaceControl
{
    public enum InterfaceControlCommand : byte
    {
        Reset = 0x00,
        GetStatus = 0x02,
        SetMode = 0x03,
        SaveMode = 0x06
    }
}