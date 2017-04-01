using System;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{
    public interface ICommunicationInterface
    {
        Task Send(Packet packet, CancellationToken cancellationToken);

        IObservable<Packet> Receive(CancellationToken cancellationToken);
    }
}