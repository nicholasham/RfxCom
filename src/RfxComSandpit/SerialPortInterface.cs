using System;
using System.IO.Ports;
using System.Threading.Tasks;
using RfxCom;

namespace RfxComSandpit
{
    public class SerialPortInterface : ICommunicationInterface
    {
        private SerialPort _serialPort;

        public SerialPortInterface(SerialPort serialPort)
        {
            _serialPort = serialPort;
        }

        public async Task WriteAsync(byte[] buffer)
        {
            var stream = _serialPort.BaseStream;
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }

        protected async Task<byte[]> ReadAsync(int blockLimit)
        {
            var stream = _serialPort.BaseStream;
            var buffer = new byte[blockLimit];
            var actualLength = await stream.ReadAsync(buffer, 0, buffer.Length);
            var received = new byte[actualLength];

            Buffer.BlockCopy(buffer, 0, received, 0, actualLength);

            return received;
        }
        
        public async Task<byte[]> ReadAsync()
        {
            return  await ReadAsync(400);
        }

        public async Task FlushAsync()
        {
            var stream = _serialPort.BaseStream;
            await stream.FlushAsync();
        }
    }
}