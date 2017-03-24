namespace RfxCom.Messages
{
    public class RawMessage
    {
        public RawMessage(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; }
    }
}