using System.Diagnostics.Contracts;

namespace RfxCom.Messages.Handlers
{
    public interface IReceiveHandler
    {
        void Handle(ReceiveContext context);
    }

    public interface IChainedReceiveHandler : IReceiveHandler
    {
        IChainedReceiveHandler NextHandler { get; }
    }
}