using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using RfxCom.Events;
using RfxCom.Messages;
using RfxCom.Messages.Handlers;

namespace RfxCom
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
        
        public async Task SendGetStatus()
        {
            await SendMessage(PacketType.InterfaceCommand, SubType.ModeCommand, Command.GetStatus,
                Enumerable.Repeat<Byte>(0x00, 9).ToArray());
        }

        public async Task Reset()
        {
            await Send(new Reset());
        }

        public Task Flush()
        {
            return CommunicationInterface.FlushAsync();
        }

        public async Task Enable(params Protocol[] protocols)
        {
            var messages = new byte[] { 0x53, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            for (var number = 1; number <= 5; number++)
            {

                if (protocols.Any(x=> x.MessageNumber == number))
                {
                    var value = protocols
                        .Where(protocol => protocol.MessageNumber == number)
                        .Sum(protocol => protocol.Value);

                    messages[number - 1] = Convert.ToByte(value);    
                }
                
            }
            
            await SendMessage(PacketType.InterfaceCommand, SubType.ModeCommand, Command.SetMode , messages);

        }

        public async Task Initialize()
        {
            await Reset();
            await Task.Delay(5000);
            await Flush();
            await SendGetStatus();

        }
        
        protected async Task SendMessage(PacketType type, SubType subType, Command command, params byte[] extra)
        {
            var sequenceNumber = NextSequenceNumber();
            var byteCount = Convert.ToByte(extra.Length + 4);
            var buffer = new[] {byteCount, type, subType, sequenceNumber, command};
            buffer = buffer.Concat(extra).ToArray();
            await CommunicationInterface.WriteAsync(buffer);

            Logger.Info("Sent: {0}", buffer.Dump());
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