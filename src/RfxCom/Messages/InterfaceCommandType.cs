namespace RfxCom.Messages
{
    public class InterfaceCommandType : Field<InterfaceCommandType>
    {
        public static readonly InterfaceCommandType Reset = new InterfaceCommandType(0x0, "Reset the receive / transceiver");
        public static readonly InterfaceCommandType GetStatus = new InterfaceCommandType(0x02, "Get status, return firmware versions and configuration of the interface");
        public static readonly InterfaceCommandType SetMode = new InterfaceCommandType(0x03, "Set mode msg1-msg5, return also the firmware version and configuration of the interface");
        public static readonly InterfaceCommandType Unknown = new InterfaceCommandType(0xff, "Unknown command");

        private InterfaceCommandType(byte value, string description) : base(value, description)
        {
        }
    }
}