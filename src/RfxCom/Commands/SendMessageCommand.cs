using System.Collections.Generic;
using RfxCom.Messages;

namespace RfxCom.Commands
{
    public class SendMessageCommand : Command
    {
        public SendMessageCommand(Message message)
        {
            Message = message;
        }

        public Message Message { get; private set; }

        public override IEnumerable<byte> ToBytes()
        {
            return Message.ToBytes();
        }
    }
}