using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{
    public class ChimeReceiveHandler : ReceiveHandler
    {
        public override void Handle(ReceiveContext context)
        {

            Chime chime;

            if (Chime.TryParse(context.Data, out chime))
            {
                context.Observable.OnNext(new MessageReceived(chime));
            }
            else
            {
                InvokeNextHandler(context);
            }

        }
    }
}