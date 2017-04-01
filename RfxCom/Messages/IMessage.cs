namespace RfxCom.Messages
{
    public interface IMessage
    {
        PacketType PacketType { get; }
        byte SequenceNumber { get; }

    }
}