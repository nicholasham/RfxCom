using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{

    

    public interface ICommunicationInterface
    {
        /// <summary>
        /// Writes a bock of bytes to the interface
        /// </summary>
        /// <param name="buffer">a block of data to be written</param>
        /// <returns>Awaitable task</returns>
        Task WriteAsync(Byte[] buffer);

        /// <summary>
        /// Reads a full data from the interface 
        /// </summary>
        /// <returns>Awaitable task that returns blocks of bytes</returns>
        Task<Byte[]> ReadAsync(CancellationToken cancellationToken);
        
        Task FlushAsync();
    }

    public abstract class CommunicationInterface : ICommunicationInterface
    {
        protected CommunicationInterface(Stream stream)
        {
            Stream = stream;
        }

        public async Task WriteAsync(byte[] buffer)
        {
            await Stream.WriteAsync(buffer, 0, buffer.Length);
        }

        protected Stream Stream { get; set; }
        
        public async Task<byte[]> ReadAsync(CancellationToken cancellationToken)
        {
            var packetLengthBuffer = await ReadBuffer(1, cancellationToken);
            var packetLength = packetLengthBuffer[0];
            var packetBuffer = await ReadBuffer(packetLength, cancellationToken);
            var data = packetLengthBuffer.Concat(packetBuffer).ToArray();
            
            return data;
        }

        private async Task<Byte[]> ReadBuffer(int length, CancellationToken cancellationToken)
        {
            var packetBuffer = new byte[length];
            int totalBytesRemaining = length;
            var totalBytesRead = 0;
            
            while (totalBytesRemaining != 0)
            {
                var bytesRead = await Stream.ReadAsync(packetBuffer, totalBytesRead, totalBytesRemaining, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                totalBytesRead += bytesRead;
                totalBytesRemaining -= bytesRead;
            }

            return packetBuffer;
        }

        public async Task FlushAsync()
        {
            await Stream.FlushAsync();
        }

        private async Task<Byte[]> ReadBytesAsync(int bufferSize)
        {
            var buffer = new byte[bufferSize];
            var actualLength = await Stream.ReadAsync(buffer, 0, buffer.Length);
            var received = new byte[actualLength];

            Buffer.BlockCopy(buffer, 0, received, 0, actualLength);

            return received;
        }

    }


}