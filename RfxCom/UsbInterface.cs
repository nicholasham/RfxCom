using System;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{
    public class UsbInterface : ICommunicationInterface
    {


        
        public Task Send(Packet packet, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IObservable<Packet> Receive(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}