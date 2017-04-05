using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using RJCP.IO.Ports;

namespace RfxCom
{
   

    public class UsbInterface : ICommunicationInterface
    {
        private readonly SerialPortStream _stream;

        public UsbInterface(string portName)
        {
            _stream = new SerialPortStream(portName, 38400, 8, Parity.None, StopBits.One);
            _stream.Open();
        }

        public Task Send(Packet packet, CancellationToken cancellationToken)
        {
            return _stream.WriteAsync(packet, 0, packet.Length + 1, cancellationToken);
        }

        public IObservable<Packet> Receive(CancellationToken cancellationToken)
        {
            return Observable.FromAsync(token => ReadAsync(cancellationToken))
                .SelectMany(packet => packet)
                .Publish()
                .RefCount();
        }

        private async Task<Option<Packet>> ReadAsync(CancellationToken cancellationToken)
        {
            var packetLengthBuffer = await ReadBuffer(1, cancellationToken);
            var packetLength = packetLengthBuffer[0];

            if (packetLength <= 0) return Option<Packet>.None;
            var packetBuffer = await ReadBuffer(packetLength, cancellationToken);
            var data = packetLengthBuffer.Concat(packetBuffer).ToArray();

            return Packet.Parse(data);
        }

        private async Task<byte[]> ReadBuffer(int length, CancellationToken cancellationToken)
        {
            var packetBuffer = new byte[length];
            var totalBytesRemaining = length;
            var totalBytesRead = 0;

            while (totalBytesRemaining != 0)
            {
                var bytesRead = await _stream.ReadAsync(packetBuffer, totalBytesRead, totalBytesRemaining, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                totalBytesRead += bytesRead;
                totalBytesRemaining -= bytesRead;
            }

            return packetBuffer;
        }

        
    }
}