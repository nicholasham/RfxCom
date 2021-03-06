using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Events;
using RfxCom.Messages;
using RfxCom.Messages.Handlers;

namespace RfxCom
{
   
    public class Transceiver : ITransceiver
    {
        private Transceiver()
        {
            ByteCounter = new ByteCounter(0);
        }

        public Transceiver(ICommunicationInterface communicationInterface, ILogger logger) : this(communicationInterface, logger, new ReceiveHandlerFactory())
        {
            CommunicationInterface = communicationInterface;
            Logger = logger;
        }

        public Transceiver(ICommunicationInterface communicationInterface, ILogger logger, IReceiveHandlerFactory handlerFactory) : this()
        {
            CommunicationInterface = communicationInterface;
            Logger = logger;
            HandlerFactory = handlerFactory;
        }

        protected ByteCounter ByteCounter { get; set; } 
        protected IReceiveHandlerFactory HandlerFactory { get; set; }
        protected ILogger Logger { get; private set; }
        protected ICommunicationInterface CommunicationInterface { get; private set; }


        public IConnectableObservable<Event> Receive()
        {
            return Observable.Create<Event>(async (subject, cancellationToken) =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var bytes = await CommunicationInterface.ReadAsync(cancellationToken);
                        var handler = HandlerFactory.Create();
                        var context = new ReceiveContext(subject, bytes);
                        handler.Handle(context);
                    }
                    catch (Exception exception)
                    {
                        subject.OnNext(new ErrorEvent(exception));
                    }
                }
            })
            .Publish();
        }
        
        public async Task Send(Message message)
        {
            const int sequenceNumberIndex = 3;

            var buffer = message.ToBytes().ToArray();
            buffer[sequenceNumberIndex] = ByteCounter.Next();       
            
            await CommunicationInterface.WriteAsync(buffer);

            Logger.Info("Message Sent ({0}) - {1}", message.GetType().Name, message);
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