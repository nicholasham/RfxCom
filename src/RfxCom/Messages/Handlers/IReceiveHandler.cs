namespace RfxCom.Messages.Handlers
{
    public interface IReceiveHandler
    {
        void Handle(ReceiveContext context);
    }
}