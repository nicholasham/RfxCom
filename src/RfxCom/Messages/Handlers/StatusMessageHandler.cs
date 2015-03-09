using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{
    public class StatusMessageHandler : ReceiveHandler
    {
        public override void Handle(ReceiveContext context)
        {
            ResponseMessage message;

            if (ResponseMessage.TryParse(context.Data, out message))
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