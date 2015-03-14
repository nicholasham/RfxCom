namespace RfxCom.Messages
{
    public class PacketType : Field<PacketType>
    {
        public static PacketType InterfaceCommand = new PacketType(0x00, "Interface Command");
        public static PacketType InterfaceMessage = new PacketType(0x01, "Interface Message");
        public static PacketType TransceiverMessage = new PacketType(0x02, "Receiver/Transmitter message");
        public static PacketType Chime = new PacketType(0x16, "Byron");

        private PacketType(byte value, string description) : base(value, description)
        {
        }
        
    }
}