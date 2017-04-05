namespace RfxCom.Messages
{
    public class RawMessage : IMessage
    {
        public RawMessage(PacketType packetType, byte subType, byte sequenceNumber, byte[] data)
        {
            PacketType = packetType;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            Data = data;
        }

        public PacketType PacketType { get; }

        public byte SubType { get; }

        public byte SequenceNumber { get; }

        public byte[] Data { get; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}