using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom
{
    public class ReactiveTransceiver : IBufferedTransceiver
    {
        public const int MaximumBufferSize = 1024 * 1024;

        private readonly IMessageCodec _codec;
        private readonly ICommunicationDevice _device;
        private readonly BlockingCollection<IMessage> _receivedMessages;
        private readonly IDisposable _receiveSubscription;
        private readonly BlockingCollection<IMessage> _sentMessages;

        public ReactiveTransceiver(IMessageCodec codec, ICommunicationDevice device) : this(codec, device, TaskPoolScheduler.Default)
        {
        }
        public ReactiveTransceiver(IMessageCodec codec, ICommunicationDevice device, IScheduler scheduler)
        {
            _codec = codec;
            _device = device;

            _receivedMessages = new BlockingCollection<IMessage>();
            _sentMessages = new BlockingCollection<IMessage>();

            

            Received = _receivedMessages.GetConsumingEnumerable()
                .ToObservable(scheduler);

            Sent = _sentMessages.GetConsumingEnumerable()
                .ToObservable(scheduler);

            Task.WaitAll(InitialiseAsync(CancellationToken.None));

            _receiveSubscription = Observable
                .FromAsync(ReceiveAsync)
                .Repeat()
                .TakeWhile(x => x.Any())
                .SelectMany(x => x)
                .SubscribeOn(new EventLoopScheduler())
                .Subscribe(message => { _receivedMessages.Add(message); });
        }

        private async Task InitialiseAsync(CancellationToken cancellationToken)
        {
            await SendAsync(new ResetMessage(), CancellationToken.None);
            await Task.Delay(TimeSpan.FromSeconds(9), cancellationToken);
            await _device.FlushAsync(cancellationToken);
            await SendAsync(new GetStatusMessage(1), CancellationToken.None);
        }

        public IObservable<IMessage> Received { get; }
        public IObservable<IMessage> Sent { get; }
      
        private async Task<IEnumerable<IMessage>> ReceiveAsync(CancellationToken cancellationToken)
        {
            return (await _device.ReceiveAsync(cancellationToken)).Select(Decode);
        }

        public async Task SendAsync(IMessage message, CancellationToken cancellationToken)
        {
            var result = _codec.Encode(message);

            if (result.Any())
            {
                await _device.SendAsync(result.First(), cancellationToken);
                _sentMessages.Add(message, cancellationToken);
            }
        }

        public void Dispose()
        {
            _receivedMessages?.Dispose();
            _receiveSubscription?.Dispose();
            _sentMessages?.Dispose();
            _device?.Dispose();
        }

        private IMessage Decode(Packet packet)
        {
            return _codec
                .Decode(packet)
                .Match(message => message, () => new RawMessage(packet));
        }
    }
}