using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Messages.InterfaceResponse
{
    public class SetModeResponseMessage : InterfaceResponseMessage
    {
        public SetModeResponseMessage(byte sequenceNumber, TransceiverType transceiverType, params Protocol[] protocols) : base(sequenceNumber)
        {
            TransceiverType = transceiverType;
            Protocols = protocols;
        }

        public TransceiverType TransceiverType { get; }

        public Protocol[] Protocols { get; }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response: Set Mode, TransceiverType: {TransceiverType}, Protocols: {string.Join(",", Protocols)}";
        }
    }
}