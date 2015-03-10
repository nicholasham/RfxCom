using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{
    public class InterfaceMessageHandler : ReceiveHandler
    {
        public override void Handle(ReceiveContext context)
        {
            InterfaceMessage message;

            if (InterfaceMessage.TryParse(context.Data, out message))
            {
                context.Observable.OnNext(new MessageReceived(message));
            }
            else
            {
                InvokeNextHandler(context);
            }
        }
    }
}