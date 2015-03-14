using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Events;
using RfxCom.Messages;
using RfxCom.Messages.Handlers;

namespace RfxCom
{
   
   

    public class Transceiver : ITransceiver
    {
        public Transceiver(ICommunicationInterface communicationInterface, ILogger logger,
            IReceiveHandlerFactory handlerFactory)
        {
            CommunicationInterface = communicationInterface;
            Logger = logger;
            HandlerFactory = handlerFactory;
            ByteCounter = new ByteCounter(0);
        }

        protected ByteCounter ByteCounter { get; set; } 
        protected IReceiveHandlerFactory HandlerFactory { get; set; }
        protected ILogger Logger { get; private set; }
        protected ICommunicationInterface CommunicationInterface { get; private set; }

        public IObservable<Event> Receive(TimeSpan interval, IScheduler scheduler)
        {
            return Observable.Create<Event>(observer =>
            {
                return scheduler.ScheduleAsync(async (ctrl, ct) =>
                {
                    while (!ct.IsCancellationRequested)
                    {
                        try
                        {
                            var bytes = await CommunicationInterface.ReadAsync(ct);
                            var handler = HandlerFactory.Create();
                            var context = new ReceiveContext(observer, bytes);
                            handler.Handle(context);
                        }
                        catch (Exception ex)
                        {
                            observer.OnNext(new ErrorEvent(ex));
                        }

                        await ctrl.Sleep(interval, ct);
                    }
                });
            });
        }

        public async Task Send(Message message)
        {
            const int sequenceNumberIndex = 3;

            var buffer = message.ToBytes().ToArray();

            var interfaceControlMessage = message as InterfaceControlMessage;

            if (interfaceControlMessage != null && interfaceControlMessage.ControlCommand == InterfaceControlCommand.Reset)
            {
                buffer[sequenceNumberIndex] = 0;        
            }
            else
            {
                buffer[sequenceNumberIndex] = ByteCounter.Next();        
            }
            
            await CommunicationInterface.WriteAsync(buffer);

            Logger.Debug("Sent: {0}", buffer.Dump());
        }

        public async Task Reset()
        {
            ByteCounter.Reset();
            var message = new InterfaceControlMessage(InterfaceControlCommand.Reset);
            await Send(message);
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        public Task Flush()
        {
            return CommunicationInterface.FlushAsync();
        }

        public async Task Initialize()
        {
            await Reset();
            await Flush();
            await this.GetStatus();
        }

       

    }
}