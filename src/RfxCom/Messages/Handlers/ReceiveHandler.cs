namespace RfxCom.Messages.Handlers
{
    public abstract class ReceiveHandler : IReceiveHandler
    {
        public IReceiveHandler NextHandler { get; set; }

        public abstract void Handle(ReceiveContext context);


        protected void InvokeNextHandler(ReceiveContext context)
        {
            if (NextHandler != null)
            {
                NextHandler.Handle(context);
            }
        }
        
    }
}