using System;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;

namespace RfxCom
{
    public interface ITransceiver
    {

        IObservable<IMessage> Received { get; }

        IObservable<IMessage> Sent { get; }

        Task SendAsync(IMessage message, CancellationToken cancellationToken);

        Task StartAsync(CancellationToken cancellationToken);

        Task StopAsync(CancellationToken cancellationToken);

    }
}