namespace RfxCom.Messages.Handlers
{
    public interface IReceiveHandlerFactory
    {
        IReceiveHandler Create();
    }
}