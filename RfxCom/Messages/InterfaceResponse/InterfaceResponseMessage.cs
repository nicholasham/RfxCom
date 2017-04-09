namespace RfxCom.Messages.InterfaceResponse
{
    public abstract class InterfaceResponseMessage : IMessage
    {
        protected InterfaceResponseMessage(byte sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
        }

        public byte SequenceNumber { get; }

        public PacketType PacketType => PacketType.InterfaceResponse;
    }
}