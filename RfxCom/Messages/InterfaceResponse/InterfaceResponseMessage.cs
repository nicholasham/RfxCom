namespace RfxCom.Messages.InterfaceResponse
{
    public class InterfaceResponseMessage : IMessage
    {
        public InterfaceResponseMessage(byte sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
        }

        public byte SequenceNumber { get; }

        public PacketType PacketType => PacketType.InterfaceResponse;
    }
}