namespace RfxCom.Messages.InterfaceResponse
{
    public class UnknownRtsRemoteMessage : InterfaceResponseMessage
    {
        public UnknownRtsRemoteMessage(byte sequenceNumber) : base(sequenceNumber)
        {
        }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response: Unknown Rts Remote";
        }
    }
}