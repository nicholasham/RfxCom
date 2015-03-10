namespace RfxCom.Messages
{
    public class InterfaceMessageSubType : Field<InterfaceMessageSubType>
    {
        public static readonly InterfaceMessageSubType InterfaceMessageOnModeCommand = new InterfaceMessageSubType(0x00, "Response on a mode command");
        public static readonly InterfaceMessageSubType UnknownRtsRemote = new InterfaceMessageSubType(0x01, "Unknown RTS remote");
        public static readonly InterfaceMessageSubType NoExtendedHardware = new InterfaceMessageSubType(0x02, "No extended hardware present");
        public static readonly InterfaceMessageSubType ListRfyRemotes = new InterfaceMessageSubType(0x03, "List RFY remotes");
        public static readonly InterfaceMessageSubType WrongCommandReceived = new InterfaceMessageSubType(0xFF, "wrong command received from the application"); 

        private InterfaceMessageSubType(byte value, string description) : base(value, description)
        {
        }
    }
}