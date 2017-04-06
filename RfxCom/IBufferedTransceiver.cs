using System;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;

namespace RfxCom
{
    public interface IBufferedTransceiver : IDisposable
    {

        IObservable<IMessage> Received { get; }

        Task SendAsync(IMessage message, CancellationToken cancellationToken);

    }
}