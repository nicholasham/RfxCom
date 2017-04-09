namespace RfxCom.Messages.InterfaceControl
{
    public class GetStatusMessage : InterfaceControlMessage
    {
        public GetStatusMessage(byte sequenceNumber) : base(sequenceNumber)
        {
        }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Command: Get Status";
        }
    }
}