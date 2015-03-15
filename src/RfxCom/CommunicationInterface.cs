using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RfxCom
{
    public abstract class CommunicationInterface : Disposable, ICommunicationInterface
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

        private async Task<byte[]> ReadBuffer(int length, CancellationToken cancellationToken)
        {
            var packetBuffer = new byte[length];
            var totalBytesRemaining = length;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stream.Dispose();
            }
        }
    }
}