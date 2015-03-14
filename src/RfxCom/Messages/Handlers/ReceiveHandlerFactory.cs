namespace RfxCom.Messages.Handlers
{
    public class ReceiveHandlerFactory : IReceiveHandlerFactory
    {
        public IReceiveHandler Create()
        {
            var transceiverMessageHandler = new TransceiverMessageHandler();
            var interfaceResponseMessageHandler = new InterfaceResponseMessageHandler();
            var chimeMessageHandler = new ChimeMessageHandler();
            var unhandledMessageHandler = new UnhandledMessageHandler();

            transceiverMessageHandler.NextHandler = interfaceResponseMessageHandler;
            interfaceResponseMessageHandler.NextHandler = chimeMessageHandler;
            chimeMessageHandler.NextHandler = unhandledMessageHandler;
            
            return transceiverMessageHandler;
        }
    }
}