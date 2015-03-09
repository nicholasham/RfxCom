namespace RfxCom.Messages
{
    public abstract class Message
    {
        public abstract byte[] ToBytes();

        public override string ToString()
        {
            return ToBytes().Dump();
        }
    }
}