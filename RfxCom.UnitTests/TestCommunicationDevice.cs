using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom.UnitTests
{
    public class TestCommunicationDevice : ICommunicationDevice
    {
        public TestCommunicationDevice()
        {
            Buffer = new List<Packet>();
        }

        public List<Packet> Buffer { get; }
        
    
        public Task SendAsync(Packet packet, CancellationToken cancellationToken)
        {
            Buffer.Add(packet);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Packet>> ReceiveAsync(CancellationToken cancellationToken)
        {
            var copied = Buffer.ToList().AsEnumerable();
            Buffer.Clear();
            return Task.FromResult(copied);
        }

        public Task FlushAsync(CancellationToken cancellationToken)
        {
            Buffer.Clear();
            return Task.CompletedTask;
        }


        public void Dispose()
        {
        }
    }
}