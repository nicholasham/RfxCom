using RfxCom.Messages.InterfaceControl;

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

    public class SetModeResponseMessage : InterfaceResponseMessage
    {
        public SetModeResponseMessage(byte sequenceNumber, TransceiverType transceiverType, Protocol[] protocols) : base(sequenceNumber)
        {
            TransceiverType = transceiverType;
            Protocols = protocols;
        }

        public TransceiverType TransceiverType { get; }

        public Protocol[] Protocols { get; }
    }
}