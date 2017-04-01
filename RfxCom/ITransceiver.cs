using System;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;

namespace RfxCom
{
    public interface ITransceiver
    {
        IObservable<IMessage> Receive(CancellationToken cancellationToken);
        Task Send(IMessage message, CancellationToken cancellationToken);
    }
}