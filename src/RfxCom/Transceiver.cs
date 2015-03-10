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
    public class Transceiver : ITransceiver
    {
        public Transceiver(ICommunicationInterface communicationInterface, ILogger logger,
            IReceiveHandlerFactory handlerFactory)
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
            const int sequenceNumberIndex = 3;
            
            var buffer = command.ToBytes().ToArray();
            buffer[sequenceNumberIndex] = NextSequenceNumber();
            
            await CommunicationInterface.WriteAsync(buffer);
            
            Logger.Debug("Sent: {0}", buffer.Dump());
        }

        public async Task Reset()
        {
            ResetSequenceNumber();
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

        protected byte ResetSequenceNumber()
        {
            SequenceNumber = byte.MinValue;
            return SequenceNumber;
        }

        protected byte NextSequenceNumber()
        {
            SequenceNumber = SequenceNumber == byte.MaxValue ? byte.MinValue : Convert.ToByte(SequenceNumber + 1);
            return SequenceNumber;
        }

    }
}