namespace RfxCom.Messages.Handlers
{
    public abstract class ReceiveHandler : IChainedReceiveHandler
    {
        public IChainedReceiveHandler NextHandler { get; set; }

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