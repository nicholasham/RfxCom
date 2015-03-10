namespace RfxCom.Messages.Handlers
{
    public class ReceiveHandlerFactory : IReceiveHandlerFactory
    {
        public IReceiveHandler Create()
        {
            var transceiverMessageHandler = new TransceiverMessageHandler();
            var interfaceMessageHandler = new InterfaceMessageHandler();
            var chimeMessageHandler = new ChimeMessageHandler();
            var unhandledMessageHandler = new UnhandledMessageHandler();

            transceiverMessageHandler.NextHandler = interfaceMessageHandler;
            interfaceMessageHandler.NextHandler = chimeMessageHandler;
            chimeMessageHandler.NextHandler = unhandledMessageHandler;

            return interfaceMessageHandler;
        }
    }
}