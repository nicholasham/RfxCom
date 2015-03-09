namespace RfxCom.Messages
{
    public class ChimeSound : Field<ChimeSound>
    {
        public static ChimeSound Tubular3Notes = new ChimeSound(0x01, "Tubular 3 notes");
        public static ChimeSound BigBen = new ChimeSound(0x03, "Big Ben");
        public static ChimeSound Tubular2Notes = new ChimeSound(0x05, " Tubular 2 notes");
        public static ChimeSound Solo = new ChimeSound(0x09, "Solo");
        public static ChimeSound Unknown1 = new ChimeSound(0x0D, "Unknown 1");
        public static ChimeSound Unknown2 = new ChimeSound(0x06, "Unknown 2");
        public static ChimeSound Unknown3 = new ChimeSound(0x02, "Unknown 3");

        private ChimeSound(byte value, string description)
            : base(value, description)
        {
        }
    }
}