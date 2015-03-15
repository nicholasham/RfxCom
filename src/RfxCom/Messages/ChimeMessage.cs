using System.Diagnostics.Contracts;

namespace RfxCom.Messages
{
    
    public class ChimeMessage : Message
    {
        public ChimeMessage(ChimeSubType subType, byte sequenceNumber, byte id1, byte id2, ChimeSound sound, byte rssi)
        {
            PacketLength = PacketLengths.Chime;
            PacketType = PacketType.Chime;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            Id1 = id1;
            Id2 = id2;
            Sound = sound;
            Rssi = rssi;
            Filler = 0x00;
        }

        public byte PacketLength { get;  private set; }
        public PacketType PacketType { get;  private set; }
        public ChimeSubType SubType { get;  private set; }
        public byte  SequenceNumber { get; private set; }
        public byte Id1 { get;  private set; }
        public byte Id2 { get;  private set; }
        public ChimeSound Sound { get;  private set; }
        public byte Filler { get;  private set; }
        public byte Rssi { get;  private set; }

        public static bool TryParse(byte[] bytes, out ChimeMessage chimeMessage)
        {
            chimeMessage = default(ChimeMessage);

            if (bytes.Length != 8)
            {
                return false;
            }

            var packetLength = bytes[0];

            if (packetLength != PacketLengths.Chime)
            {
                return false;
            }

            PacketType packetType;
            
            if (PacketType.TryParse(bytes[1], out packetType) && packetType != PacketType.Chime)
            {
                return false;
            }

            var subType = ChimeSubType.Parse(bytes[2], ChimeSubType.Unsupported); ;
            var sequenceNumber = bytes[3];
            var id1 = bytes[4];
            var id2 = bytes[5];
            var sound = ChimeSound.Parse(bytes[6], ChimeSound.TubularMix);
            var rssi = bytes[7];

            chimeMessage = new ChimeMessage(subType, sequenceNumber, id1, id2, sound, rssi);

            return true;

        }

        public override string ToString()
        {
            return string.Format("Type: {0}, Sound: {1} ", SubType.Description, Sound.Description);
        }

        public override byte[] ToBytes()
        {
            return new[]
            {
                PacketLength, 
                PacketType, 
                SubType, 
                SequenceNumber, 
                Id1, 
                Id2, 
                Sound, 
                Filler, 
                Rssi
            };
        }
    }
}