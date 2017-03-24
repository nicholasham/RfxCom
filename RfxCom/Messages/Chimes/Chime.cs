namespace RfxCom.Messages.Chimes
{
    public class Chime : IMessage
    {
        public Chime(PacketType packetType)
        {
            PacketType = packetType;
        }

        public PacketType PacketType { get; }
    }
}