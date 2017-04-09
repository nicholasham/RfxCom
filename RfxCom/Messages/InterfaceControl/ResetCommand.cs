namespace RfxCom.Messages.InterfaceControl
{
    public class ResetMessage : InterfaceControlMessage
    {
        public ResetMessage() : base(0)
        {
        }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Command: Reset";
        }
    }
}