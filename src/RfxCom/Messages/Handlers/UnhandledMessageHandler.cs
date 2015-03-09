using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{
    public class UnhandledMessageHandler : ReceiveHandler
    {
        public override void Handle(ReceiveContext context)
        {
            context.Observable.OnNext(new MessageReceived(new RawMessage(context.Data)));
        }
    }
}