﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using RJCP.IO.Ports;

namespace RfxCom
{
    public class UsbDevice : ICommunicationDevice
    {
        private readonly SerialPortStream _stream;

        public UsbDevice(string portName)
        {
            _stream = new SerialPortStream(portName, 38400, 8, Parity.None, StopBits.One);
        }

        public Task OpenAsync(CancellationToken cancellationToken)
        {
            _stream.Open();
            return Task.CompletedTask;
        }

        public async Task CloseAsync(CancellationToken cancellationToken)
        {
            await _stream.FlushAsync(cancellationToken);
            _stream.Close();
        }

        public Task SendAsync(Packet packet, CancellationToken cancellationToken)
        {
            var bytes = packet.ToBytes();
            _stream.Write(bytes, 0, bytes.Length);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Packet>> ReceiveAsync(CancellationToken cancellationToken)
        {
            return await ReadAsync(cancellationToken);
        }

        public Task FlushAsync(CancellationToken cancellationToken)
        {
            return _stream.FlushAsync(cancellationToken);
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
            var buffer = new byte[length];
            var totalBytesRemaining = length;
            var totalBytesRead = 0;

            while (!cancellationToken.IsCancellationRequested && totalBytesRemaining != 0)
            {
                var bytesRead = await _stream.ReadAsync(buffer, totalBytesRead, totalBytesRemaining, cancellationToken);
                totalBytesRead += bytesRead;
                totalBytesRemaining -= bytesRead;
            }

            return buffer;
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }
    }
}