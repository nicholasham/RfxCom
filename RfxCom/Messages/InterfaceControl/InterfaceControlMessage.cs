namespace RfxCom.Messages.InterfaceControl
{
    public class InterfaceControlMessage : IMessage
    {
        public InterfaceControlMessage(byte sequenceNumber)
        {
            SequenceNumber = sequenceNumber;
        }

        public byte SequenceNumber { get; } 

        public PacketType PacketType => PacketType.InterfaceControl;
    }
}