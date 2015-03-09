namespace RfxCom.Messages.Handlers
{
    public class ReceiveHandlerFactory : IReceiveHandlerFactory
    {
        public IReceiveHandler Create()
        {
            var statusMessageHandler = new StatusMessageHandler();
            var chimeHandler = new ChimeReceiveHandler();
            var unhandled = new UnhandledMessageHandler();

            statusMessageHandler.NextHandler = chimeHandler;
            chimeHandler.NextHandler = unhandled;
            return statusMessageHandler;
        }
    }
}