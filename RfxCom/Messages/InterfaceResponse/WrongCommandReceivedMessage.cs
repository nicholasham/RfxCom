namespace RfxCom.Messages.InterfaceResponse
{
    public class WrongCommandReceivedMessage : InterfaceResponseMessage
    {
        public WrongCommandReceivedMessage(byte sequenceNumber) : base(sequenceNumber)
        {
        }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response: Wrong command received";
        }
    }
}