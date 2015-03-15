using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{
    public class InterfaceResponseMessageHandler : ReceiveHandler
    {
        public override void Handle(ReceiveContext context)
        {
            InterfaceResponseMessage message;

            if (InterfaceResponseMessage.TryParse(context.Data, out message))
            {
                context.Observable.OnNext(new MessageReceived<InterfaceResponseMessage>(message));
            }
            else
            {
                InvokeNextHandler(context);
            }
        }
    }
}