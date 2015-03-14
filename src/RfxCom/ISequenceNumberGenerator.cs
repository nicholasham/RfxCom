namespace RfxCom
{
    public interface ISequenceNumberGenerator
    {
        byte Reset();
        byte Next();
    }
}