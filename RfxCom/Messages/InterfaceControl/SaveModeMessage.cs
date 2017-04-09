namespace RfxCom.Messages.InterfaceControl
{
    public class SaveModeMessage : InterfaceControlMessage
    {
        public SaveModeMessage(byte sequenceNumber) : base(sequenceNumber)
        {
        }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Command: Save Mode";
        }
    }
}