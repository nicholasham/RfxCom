namespace RfxCom.Messages
{
    public class ChimeSubType : Field<ChimeSubType>
    {
        public ChimeSubType(byte value, string description)
            : base(value, description)
        {
        }

        public static ChimeSubType ByronSx = new ChimeSubType(0x00, "Byron Sx");
        public static ChimeSubType ByronMp001 = new ChimeSubType(0x01, "Byron Mp001");
        public static ChimeSubType Unsupported = new ChimeSubType(0xFF, "Unsupported");
    }
}