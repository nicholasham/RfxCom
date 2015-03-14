namespace RfxCom.Messages
{
    public class InterfaceResponseSubType : Field<InterfaceResponseSubType>
    {
        public static readonly InterfaceResponseSubType InterfaceMessageOnModeCommand = new InterfaceResponseSubType(0x00, "Response on a mode command");
        public static readonly InterfaceResponseSubType UnknownRtsRemote = new InterfaceResponseSubType(0x01, "Unknown RTS remote");
        public static readonly InterfaceResponseSubType NoExtendedHardware = new InterfaceResponseSubType(0x02, "No extended hardware present");
        public static readonly InterfaceResponseSubType ListRfyRemotes = new InterfaceResponseSubType(0x03, "List RFY remotes");
        public static readonly InterfaceResponseSubType WrongCommandReceived = new InterfaceResponseSubType(0xFF, "wrong command received from the application"); 

        private InterfaceResponseSubType(byte value, string description) : base(value, description)
        {
        }
    }
}