using System;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{
    public interface ICommunicationInterface
    {
        Task SendAsync(Byte[] bytes, CancellationToken cancellationToken);

        IObservable<byte[]> Receive(CancellationToken cancellationToken);
    }
}