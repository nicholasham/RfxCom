namespace RfxCom.Messages
{
    public class RawMessage : Message
    {
        public RawMessage(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; private set; }

        public override byte[] ToBytes()
        {
            return Data;
        }
        
    }


}