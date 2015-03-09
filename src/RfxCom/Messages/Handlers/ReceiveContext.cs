using System;
using RfxCom.Events;

namespace RfxCom.Messages.Handlers
{
    public class ReceiveContext
    {
        public ReceiveContext(IObserver<Event> observable, byte[] data)
        {
            Observable = observable;
            Data = data;
        }

        public IObserver<Event> Observable { get; set; }
        public byte[] Data { get; set; }
    }
}