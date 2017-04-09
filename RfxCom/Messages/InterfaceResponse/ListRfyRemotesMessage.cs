namespace RfxCom.Messages.InterfaceResponse
{
    public class ListRfyRemotesMessage : InterfaceResponseMessage
    {
        public ListRfyRemotesMessage(byte sequenceNumber, byte location, byte id1, byte id2, byte id3, byte unitNumber) : base(sequenceNumber)
        {
            Location = location;
            Id1 = id1;
            Id2 = id2;
            Id3 = id3;
            UnitNumber = unitNumber;
        }

        public byte Location { get; }

        public byte Id1 { get; }

        public byte Id2 { get; }

        public byte Id3 { get; }

        public byte UnitNumber { get; }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response: List Rfy Remotes";
        }
    }
}