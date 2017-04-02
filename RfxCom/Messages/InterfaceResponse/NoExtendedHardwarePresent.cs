namespace RfxCom.Messages.InterfaceResponse
{
    public class NoExtendedHardwarePresent : InterfaceResponseMessage
    {
        public NoExtendedHardwarePresent(byte sequenceNumber) : base(sequenceNumber)
        {
        }
    }
}