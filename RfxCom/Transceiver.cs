using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;

namespace RfxCom
{
    public class Transceiver : ITransceiver
    {
        private readonly IMessageCodec _messageCodec;
        private readonly ICommunicationInterface _interface;

        public Transceiver(IMessageCodec messageCodec, ICommunicationInterface @interface)
        {
            _messageCodec = messageCodec;
            _interface = @interface;
        }

        public IObservable<IMessage> Receive(CancellationToken cancellationToken)
        {
            return _interface.Receive(cancellationToken).Select(Decode);
        }

        private IMessage Decode(Packet packet)
        {
            return _messageCodec.Decode(packet).Match(message =>  message, () => new RawMessage(packet.Type, packet.SubType, packet.SequenceNumber, packet.Data));
        }

        public Task Send(IMessage message, CancellationToken cancellationToken)
        {
            var result = _messageCodec.Encode(message);
            return result.Match(packet => Send(packet, cancellationToken), () => Task.CompletedTask);
        }

        private Task Send(Packet packet, CancellationToken cancellationToken)
        {
            return _interface.Send(packet, cancellationToken);
        }
    }
}