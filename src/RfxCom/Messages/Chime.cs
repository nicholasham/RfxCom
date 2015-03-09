using System.Diagnostics.Contracts;

namespace RfxCom.Messages
{
    
    public class Chime : Message
    {
        public Chime(byte packetLength, PacketType packetType, ChimeSubType subType, byte sequenceNumber, byte id1, byte id2, ChimeSound sound, byte rssi)
        {
            PacketLength = packetLength;
            PacketType = packetType;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            Id1 = id1;
            Id2 = id2;
            Sound = sound;
            Rssi = rssi;
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

        public static bool TryParse(byte[] bytes, out Chime chime)
        {
            chime = default(Chime);

            if (bytes.Length != 8)
            {
                return false;
            }

            var packetLength = bytes[0];
            PacketType packetType;
            
            if (PacketType.TryParse(bytes[1], out packetType) && packetType != PacketType.Chime)
            {
                return false;
            }

            var subType = ChimeSubType.Parse(bytes[2], ChimeSubType.Unsupported); ;
            var sequenceNumber = bytes[3];
            var id1 = bytes[4];
            var id2 = bytes[5];
            var sound = ChimeSound.Parse(bytes[6], ChimeSound.Unknown1);
            var rssi = bytes[7];

            chime = new Chime(packetLength, packetType, subType, sequenceNumber, id1, id2, sound, rssi);

            return true;

        }

        public override string ToString()
        {
            return string.Format("Chime {0} with sound {1} ", SubType.Description, Sound.Description);
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