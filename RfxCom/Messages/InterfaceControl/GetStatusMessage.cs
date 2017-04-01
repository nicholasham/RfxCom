namespace RfxCom.Messages.InterfaceControl
{
    public class GetStatusMessage : InterfaceControlMessage
    {
        public GetStatusMessage(byte sequenceNumber) : base(sequenceNumber)
        {
        }
    }
}