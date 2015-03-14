using System;
using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{

    public class ChimeMessageHandler : ReceiveHandler
    {


        public override void Handle(ReceiveContext context)
        {

            ChimeMessage message;

            if (ChimeMessage.TryParse(context.Data, out message))
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