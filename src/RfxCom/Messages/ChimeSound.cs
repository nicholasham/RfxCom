namespace RfxCom.Messages
{
    public class ChimeSound : Field<ChimeSound>
    {
        public static ChimeSound Tubular3Notes = new ChimeSound(0x01, "Tubular 3 Notes");
        public static ChimeSound TubularMix = new ChimeSound(0x0D, "Tubular Mix");

        public static ChimeSound BigBen = new ChimeSound(0x03, "Big Ben");
        public static ChimeSound Clarinet = new ChimeSound(0x0E, "Clarinet Rise");

        public static ChimeSound Tubular2Notes = new ChimeSound(0x05, "Tubular 2 Notes");
        public static ChimeSound Saxophone = new ChimeSound(0x06, "Saxophone Rise");
        
        public static ChimeSound Solo = new ChimeSound(0x09, "Solo");
        public static ChimeSound MorningDew = new ChimeSound(0x02, "Morning Dew");

        private ChimeSound(byte value, string description)
            : base(value, description)
        {
        }
    }
}