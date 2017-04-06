using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;

namespace RfxCom
{
    public class BufferedTransceiver : IBufferedTransceiver
    {
        public const int MaximumBufferSize = 1024 * 1024;

        private readonly IDisposable _readSubscription;
        private readonly BlockingCollection<IMessage> _receiveBuffer;
        private readonly Subject<Unit> _receiverTermination = new Subject<Unit>();
        private readonly ITransceiver _transceiver;

        public BufferedTransceiver(ITransceiver transceiver)
        {
            _transceiver = transceiver;
            _receiveBuffer = new BlockingCollection<IMessage>(MaximumBufferSize);

            _readSubscription = Observable
                .FromAsync(_transceiver.ReceiveAsync)
                .Repeat()
                .TakeWhile(x => x.Any())
                .SelectMany(x => x)
                .SubscribeOn(new EventLoopScheduler())
                .Subscribe(message => { _receiveBuffer.Add(message, CancellationToken.None); });

            Received = _receiveBuffer.GetConsumingEnumerable()
                .ToObservable(TaskPoolScheduler.Default)
                .TakeUntil(_receiverTermination);
        }

        public IObservable<IMessage> Received { get; }

        public Task SendAsync(IMessage message, CancellationToken cancellationToken)
        {
            return _transceiver.SendAsync(message, cancellationToken);
        }

        public void Dispose()
        {
            _receiveBuffer?.Dispose();
            _readSubscription?.Dispose();
            _receiverTermination?.Dispose();
            _transceiver?.Dispose();
        }
    }
}