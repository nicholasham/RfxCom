namespace RfxCom.Messages.InterfaceResponse
{
    public class UnknownRtsRemoteMessage : InterfaceResponseMessage
    {
        public UnknownRtsRemoteMessage(byte sequenceNumber) : base(sequenceNumber)
        {
        }
    }
}