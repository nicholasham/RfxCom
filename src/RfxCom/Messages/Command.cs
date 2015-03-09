namespace RfxCom.Messages
{
    public class Command : Field<Command>
    {
        public static readonly Command Reset = new Command(0x0, "Reset the receive / transceiver");
        public static readonly Command GetStatus = new Command(0x02, "Get status, return firmware versions and configuration of the interface");
        public static readonly Command SetMode = new Command(0x03, "Set mode msg1-msg5, return also the firmware version and configuration of the interface");
        public static readonly Command Unknown = new Command(0xff, "Unknown command");

        private Command(byte value, string description) : base(value, description)
        {
        }
    }
}