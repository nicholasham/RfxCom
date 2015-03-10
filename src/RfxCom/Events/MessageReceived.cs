using Newtonsoft.Json;
using RfxCom.Messages;

namespace RfxCom.Events
{
   
    public class MessageReceived : Event
    {
        public MessageReceived(Message message)
        {
            Message = message;
        }

        public Message Message { get;  private set; }

        public override string ToString()
        {
            return string.Format("Message Received - {0}", Message.ToString());
        }
    }
}