using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{
    public class TransceiverMessageHandler : ReceiveHandler
    {
        public override void Handle(ReceiveContext context)
        {

            TransceiverMessage message;

            if (TransceiverMessage.TryParse(context.Data, out message))
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