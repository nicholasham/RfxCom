using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{

    public interface ICommunicationDevice
    {

        Task StartAsync(CancellationToken cancellationToken);

        Task StopAsync(CancellationToken cancellationToken);

        Task SendAsync(Packet packet, CancellationToken cancellationToken);

        Task<IEnumerable<Packet>> ReceiveAsync(CancellationToken cancellationToken);

        Task FlushAsync(CancellationToken cancellationToken);

    }
}