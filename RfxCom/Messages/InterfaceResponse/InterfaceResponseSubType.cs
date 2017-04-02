namespace RfxCom.Messages.InterfaceResponse
{
    public enum InterfaceResponseSubType : byte
    {
        ModeCommandResponse = 0x00,
        UnknownRtsRemote = 0x01,
        NoExtendedHardware = 0x02,
        ListRfyRemotes = 0x03,
        WrongCommandReceived = 0xFF
    }
}