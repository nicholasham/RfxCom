using System.Linq;

namespace RfxCom.Messages
{
    public class RawMessage : IMessage
    {
        public RawMessage(Packet packet)
        {
            Packet = packet;
        }
        public Packet Packet { get; }

        public override string ToString()
        {
            var data = string.Join(" ", Packet.Data.Select(x => $"{x:X2}"));
            return $"Raw(PacketType: {Packet.Type},Sub Type:{Packet.SubType}, Sequence Number: {Packet.SequenceNumber}, Data: {data})";

        }

        public PacketType PacketType => Packet.Type;
        public byte SequenceNumber => Packet.SequenceNumber;
    }
}