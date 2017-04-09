namespace RfxCom.Messages.InterfaceControl
{
    public abstract class InterfaceControlMessage : IMessage
    {
        protected InterfaceControlMessage(byte sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
        }

        public byte SequenceNumber { get; } 

        public PacketType PacketType => PacketType.InterfaceControl;
    }
}