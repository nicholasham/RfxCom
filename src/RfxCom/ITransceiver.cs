using System;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using RfxCom.Events;
using RfxCom.Messages;

namespace RfxCom
{
    public interface ITransceiver
    {
        IObservable<Event> Receive(TimeSpan interval, IScheduler scheduler);
        Task Send(Message message);
        Task Reset();
        Task Flush();
        Task Initialize();
    }
}
