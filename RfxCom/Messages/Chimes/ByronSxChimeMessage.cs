namespace RfxCom.Messages.Chimes
{
    public class ByronSxChimeMessage : IMessage
    {
        public ByronSxChimeMessage(byte sequenceNumber, ushort id, ChimeSound sound, byte signalStrength)
        {
            PacketType = PacketType.Chime;
            Sound = sound;
            SignalStrength = signalStrength;
            SequenceNumber = sequenceNumber;
            Id = id;
        }

        public ByronSxChimeMessage(byte sequenceNumber, ushort id, ChimeSound sound)
        {
            PacketType = PacketType.Chime;
            Sound = sound;
            SignalStrength = 0x00;
            SequenceNumber = sequenceNumber;
            Id = id;
        }

        public byte SequenceNumber { get; }
        public ushort Id { get; }
        public PacketType PacketType { get; }

        public ChimeSound Sound { get; }

        public byte SignalStrength { get; }

        public override string ToString()
        {
            return $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Chime Type: Byron SX, Id: {Id}, Sound: {Sound}, Signal Strength: {SignalStrength}";
        }
    }
}