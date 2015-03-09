using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using RfxCom.Commands;
using RfxCom.Events;
using RfxCom.Messages;
using RfxCom.Messages.Handlers;

namespace RfxCom
{

  
    public class Transmitter : ITransmitter
    {
        public Transmitter(ICommunicationInterface communicationInterface, ILogger logger, IReceiveHandlerFactory handlerFactory)
        {
            CommunicationInterface = communicationInterface;
            Logger = logger;
            HandlerFactory = handlerFactory;
        }

        protected byte SequenceNumber { get; set; }
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
                            var buffer = new List<byte>();

                            var data = await CommunicationInterface.ReadAsync();

                            buffer.AddRange(data);

                            var requiredBytes = buffer[0] + 1;

                            while (buffer.Count < requiredBytes)
                            {
                                data = await CommunicationInterface.ReadAsync();
                                buffer.AddRange(data);
                            }
                            
                            while (buffer.Count > 0)
                            {
                                var count = buffer[0] + 1;
                                var bytes = buffer.Take(count).ToArray();
                                var context = new ReceiveContext(observer, bytes);
                                var handler = HandlerFactory.Create();
                                handler.Handle(context);
                                buffer = buffer.Skip(count).ToList();
                            }
                            
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

        public async Task Send(Command command)
        {
           await CommunicationInterface.WriteAsync(command.ToBytes().ToArray());
           Logger.Info("Sent: {0}", command.ToBytes().Dump());

        }

        public async Task Reset()
        {
            await Send(new ResetCommand());
            await Task.Delay(50);
        }

        public Task Flush()
        {
            return CommunicationInterface.FlushAsync();
        }
       
        public async Task Initialize()
        {
            await Reset();
            await Flush();
            await Send(new GetStatusCommand());
        }
       
        protected byte NextSequenceNumber()
        {
            return SequenceNumber = SequenceNumber.Next();
        }

        protected async Task Send(Message message)
        {
            var buffer = message.ToBytes();
            await CommunicationInterface.WriteAsync(buffer);
            Logger.Info("Sent: {0}", message.ToBytes().Dump());
        }
    }
}