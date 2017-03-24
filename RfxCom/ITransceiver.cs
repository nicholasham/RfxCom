using System;
using System.Threading.Tasks;
using RfxCom.Messages;

namespace RfxCom
{
    public interface ITransceiver
    {
        IObservable<IMessage> Receive();
        Task Send(IMessage message);
    }
}