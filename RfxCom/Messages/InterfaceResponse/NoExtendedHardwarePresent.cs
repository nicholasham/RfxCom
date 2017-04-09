namespace RfxCom.Messages.InterfaceResponse
{
    public class NoExtendedHardwarePresent : InterfaceResponseMessage
    {
        public NoExtendedHardwarePresent(byte sequenceNumber) : base(sequenceNumber)
        {
        }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response: No extended hardware present";
        }
    }
}