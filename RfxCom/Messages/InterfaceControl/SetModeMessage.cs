namespace RfxCom.Messages.InterfaceControl
{
    public class SetModeMessage : InterfaceControlMessage
    {
        public SetModeMessage(byte sequenceNumber, TransceiverType transceiverType, params Protocol[] protocols) : base(sequenceNumber)
        {
            TransceiverType = transceiverType;
            Protocols = protocols;
        }

        public TransceiverType TransceiverType { get; }

        public Protocol[] Protocols { get; }

    }
}