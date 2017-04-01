using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;

namespace RfxCom.UnitTests
{
    public class TestCommunicationInterface : ICommunicationInterface
    {
        public TestCommunicationInterface()
        {
            Buffer = new List<Packet>();
        }

        public List<Packet> Buffer { get; }
        
        public Task Send(Packet packet, CancellationToken cancellationToken)
        {
            Buffer.Add(packet);
            return Task.CompletedTask;
        }

        public IObservable<Packet> Receive(CancellationToken cancellationToken)
        {
            return Buffer.ToObservable();
        }
    }
}