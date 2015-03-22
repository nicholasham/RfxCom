using RfxCom.Messages;

namespace RfxCom.Events
{
   
    public class MessageReceived<T> : Event where T: Message
    {
        public MessageReceived(T message)
        {
            Message = message;
        }

        public T Message { get;  private set; }

        public override string ToString()
        {
            return string.Format("Message Received ({0}) - {1}", Message.GetType().Name, Message);
        }
    }
}