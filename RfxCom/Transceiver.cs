using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RfxCom.Messages;

namespace RfxCom
{
    public class Transceiver : ITransceiver
    {
        private readonly IMessageCodec _messageCodec;
        private readonly ICommunicationDevice _communicationDevice;
      
        public const int MaximumBufferSize = 1024 * 1024;

       
        public Transceiver(IMessageCodec messageCodec, ICommunicationDevice communicationDevice)
        {
            _messageCodec = messageCodec;
            _communicationDevice = communicationDevice;
           
        }
    
        private IEnumerable<IMessage> Decode(IEnumerable<Packet> packets)
        {
           return packets.Select(Decode);
        }


        private IMessage Decode(Packet packet)
        {
            return _messageCodec.Decode(packet).Match(message =>  message, () => new RawMessage(packet.Type, packet.SubType, packet.SequenceNumber, packet.Data));
        }

        public Task SendAsync(IMessage message, CancellationToken cancellationToken)
        {
            var result = _messageCodec.Encode(message);
            return result.Match(packet => _communicationDevice.SendAsync(packet, cancellationToken), () => Task.CompletedTask);
        }

        public Task<IEnumerable<IMessage>> ReceiveAsync(CancellationToken cancellationToken)
        {
            return _communicationDevice.ReceiveAsync(cancellationToken).Select(Decode);
        }

        public void Dispose()
        {
            _communicationDevice?.Dispose();
        }
    }
}