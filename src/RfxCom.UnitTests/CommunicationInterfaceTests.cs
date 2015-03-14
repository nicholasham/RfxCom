using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute.Core;
using RfxCom.Messages;
using Should;
using Xunit;

namespace RfxCom.UnitTests
{
    public class TestInterface : CommunicationInterface
    {
        public TestInterface(Stream stream) : base(stream)
        {
        }

        public async Task WriteChunksWithDelay(IEnumerable<byte[]> data)
        {
            
            foreach (var bytes in data)
            {
                await Stream.WriteAsync(bytes, 0, bytes.Length);
            }

            await Stream.FlushAsync();

            await Task.Delay(50);
            
        }

        public async Task WriteWithDelay(params byte[] bytes)
        {
            await Stream.WriteAsync(bytes, 0, bytes.Length);
            await Stream.FlushAsync();
            await Task.Delay(50);
        }

        
    }

    public class CommunicationInterfaceTests
    {

        [Fact]
        public async Task ReadAsync_ShouldReturnFullMessage()
        {
            var sut = new TestInterface(new MemoryStream());

            sut.WriteWithDelay(0x07, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07);

            var result = await sut.ReadAsync(CancellationToken.None);

            var actual = result.Dump();
            var expected = "07-01-02-03-04-05-06-07";

            actual.ShouldEqual(expected);

        }

        [Fact]
        public async Task ReadAsync_ShouldReturnFullMessageEvenWhenReceivedDataIsChunked()
        {
            var sut = new TestInterface(new MemoryStream());
            
            var data = new List<byte[]>()
            {
                new byte[] { 0x07 }, 
                new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 }
            };
            
            sut.WriteChunksWithDelay(data);
            
            var result = await sut.ReadAsync(CancellationToken.None);

            var actual = result.Dump();
            var expected = "07-01-02-03-04-05-06-07";

            actual.ShouldEqual(expected);
            
        }


        [Fact]
        public async Task ReadAsync_ShouldReturnFullMessageOnly()
        {
            var sut = new TestInterface(new MemoryStream());

            sut.WriteWithDelay(0x07, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x09);

            var result = await sut.ReadAsync(CancellationToken.None);

            var actual = result.Dump();
            var expected = "07-01-02-03-04-05-06-07";

            actual.ShouldEqual(expected);

        }


        
    }
}