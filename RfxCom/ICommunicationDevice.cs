using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{

    public interface ICommunicationDevice : IDisposable
    {

        Task SendAsync(Packet packet, CancellationToken cancellationToken);

        Task<IEnumerable<Packet>> ReceiveAsync(CancellationToken cancellationToken);

        Task FlushAsync(CancellationToken cancellationToken);

    }
}