namespace RfxCom.Messages.InterfaceResponse
{
    public class WrongCommandReceivedMessage : InterfaceResponseMessage
    {
        public WrongCommandReceivedMessage(byte sequenceNumber) : base(sequenceNumber)
        {
        }
    }
}