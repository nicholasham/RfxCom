namespace RfxCom.Messages
{
    public class InterfaceControlSubType : Field<InterfaceControlSubType>
    {
        public static InterfaceControlSubType ModeCommand = new InterfaceControlSubType(0x00, "Mode Command");
        
        internal InterfaceControlSubType(byte value, string description) : base(value, description)
        {
        }
    }

   
    
}