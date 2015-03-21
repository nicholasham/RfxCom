using RfxCom.Messages;
using Should;
using Xunit;

namespace RfxCom.UnitTests.Messages
{
    public class InterfaceResponseMessageTests
    {
        [Fact]
        public void TryParse_ShouldReturnFalseWhenIncorrectPacketLength()
        {
            InterfaceResponseMessage message;

            var buffer = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var result = InterfaceResponseMessage.TryParse(buffer, out message);

            result.ShouldBeFalse();
        }

        [Fact]
        public void TryParse_ShouldReturnFalseWhenIncorrectPacketDataLength()
        {
            InterfaceResponseMessage message;

            var buffer = new byte[] { PacketLengths.InterfaceResponse, 0x00};
            var result = InterfaceResponseMessage.TryParse(buffer, out message);

            result.ShouldBeFalse();
        }

        [Fact]
        public void TryParse_ShouldReturnFalseWhenIncorrectPacketType()
        {
            InterfaceResponseMessage message;

            var buffer = new byte[] { PacketLengths.InterfaceResponse, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var result = InterfaceResponseMessage.TryParse(buffer, out message);

            result.ShouldBeFalse();
        }

        [Fact]
        public void TryParse_ShouldReturnFalseWhenIncorrectSubType()
        {
            InterfaceResponseMessage message;

            var buffer = new byte[] { PacketLengths.InterfaceResponse, PacketType.InterfaceMessage, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var result = InterfaceResponseMessage.TryParse(buffer, out message);

            result.ShouldBeFalse();
        }


        [Fact]
        public void TryParse_ShouldReturnFalseWhenIncorrectInterfaceCommand()
        {
            InterfaceResponseMessage message;

            var buffer = new byte[] { PacketLengths.InterfaceResponse, PacketType.InterfaceMessage, InterfaceControlSubType.ModeCommand, 0x00,0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            var result = InterfaceResponseMessage.TryParse(buffer, out message);

            result.ShouldBeFalse();
        }
    }
}