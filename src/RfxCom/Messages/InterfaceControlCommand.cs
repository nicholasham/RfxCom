namespace RfxCom.Messages
{
    public class InterfaceControlCommand : Field<InterfaceControlCommand>
    {
        public static readonly InterfaceControlCommand Reset = new InterfaceControlCommand(0x0, "Reset the receive / transceiver");
        public static readonly InterfaceControlCommand GetStatus = new InterfaceControlCommand(0x02, "Get status, return firmware versions and configuration of the interface");
        public static readonly InterfaceControlCommand SetMode = new InterfaceControlCommand(0x03, "Set mode msg1-msg5, return also the firmware version and configuration of the interface");
        public static readonly InterfaceControlCommand Unknown = new InterfaceControlCommand(0xff, "Unknown command");

        private InterfaceControlCommand(byte value, string description) : base(value, description)
        {
        }
    }
}