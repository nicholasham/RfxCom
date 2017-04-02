namespace RfxCom.Messages.Transceiver
{
    public class TransceiverMessage : IMessage
    {
        public TransceiverMessage(PacketType packetType, byte sequenceNumber, TransceiverMessageSubType subType, byte message)
        {
            PacketType = packetType;
            SequenceNumber = sequenceNumber;
            SubType = subType;
            Message = message;
        }

        public PacketType PacketType { get; }
        public byte SequenceNumber { get; }

        public TransceiverMessageSubType SubType { get; }

        public byte Message { get; }
    }
}