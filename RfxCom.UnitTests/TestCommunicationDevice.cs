using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom.UnitTests
{
    public class TestCommunicationDevice : ICommunicationDevice
    {
        public TestCommunicationDevice()
        {
            Sent = new List<Packet>();
            Received = new List<Packet>();
        }

        public List<Packet> Sent { get; }
        public List<Packet> Received { get; }


        public Task OpenAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SendAsync(Packet packet, CancellationToken cancellationToken)
        {
            Sent.Add(packet);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Packet>> ReceiveAsync(CancellationToken cancellationToken)
        {
            var copied = Received.ToList().AsEnumerable();
            Received.Clear();
            return Task.FromResult(copied);
        }

        public Task FlushAsync(CancellationToken cancellationToken)
        {
            Sent.Clear();
            Received.Clear();
            return Task.CompletedTask;
        }


        public void Dispose()
        {
        }
    }
}