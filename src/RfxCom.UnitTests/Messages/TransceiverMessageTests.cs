using NSubstitute.Core;
using RfxCom.Messages;
using Should;
using Xunit;

namespace RfxCom.UnitTests.Messages
{
  
    public class TransceiverMessageTests
    {
        [Fact]
        public void TryParse_ShouldReturnFalseWhenThePacketLengthIsNotCorrect()
        {
            TransceiverMessage message;

            var buffer = new byte[] { 0x05, 0x02, 0x00, 0x01, 0x01, 0x01 };
            var result = TransceiverMessage.TryParse(buffer, out message);
           
            result.ShouldBeFalse();
        }

        [Fact]
        public void TryParse_ShouldReturnFalseWhenThePacketTypeIsNotCorrect()
        {
            TransceiverMessage message;

            var buffer = new byte[] { 0x04, 0x03, 0x00, 0x01, 0x01 };
            var result = TransceiverMessage.TryParse(buffer, out message);

            result.ShouldBeFalse();
        }

        [Fact]
        public void TryParse_ShouldConstructCorrectMessageForOk()
        {
            TransceiverMessage message;

            var buffer = new byte[] {0x04, 0x02, 0x01, 0x01, 0x00};
            var result = TransceiverMessage.TryParse(buffer, out message);

            result.ShouldBeTrue();
            message.PacketLength.ShouldEqual((byte)0x04);
            message.PacketType.ShouldEqual(PacketType.TransceiverMessage);
            message.SequenceNumber.ShouldEqual((byte)0x01);
            message.SubType.ShouldEqual(TransceiverMessageSubType.Response);
            message.Response.ShouldEqual(TransceiverResponse.Ok);

        }

        [Fact]
        public void TryParse_ShouldConstructCorrectMessageForOkButDelay()
        {
            TransceiverMessage message;

            var buffer = new byte[] { 0x04, 0x02, 0x01, 0x01, 0x01 };
            var result = TransceiverMessage.TryParse(buffer, out message);

            result.ShouldBeTrue();
            message.PacketLength.ShouldEqual((byte)0x04);
            message.PacketType.ShouldEqual(PacketType.TransceiverMessage);
            message.SequenceNumber.ShouldEqual((byte)0x01);
            message.SubType.ShouldEqual(TransceiverMessageSubType.Response);
            message.Response.ShouldEqual(TransceiverResponse.OkButDelay);

        }

        [Fact]
        public void TryParse_ShouldConstructCorrectMessageForDidNotLock()
        {
            TransceiverMessage message;

            var buffer = new byte[] { 0x04, 0x02, 0x01, 0x01, 0x02};
            var result = TransceiverMessage.TryParse(buffer, out message);

            result.ShouldBeTrue();
            message.PacketLength.ShouldEqual((byte)0x04);
            message.PacketType.ShouldEqual(PacketType.TransceiverMessage);
            message.SequenceNumber.ShouldEqual((byte)0x01);
            message.SubType.ShouldEqual(TransceiverMessageSubType.Response);
            message.Response.ShouldEqual(TransceiverResponse.DidNotLock);

        }

        [Fact]
        public void TryParse_ShouldConstructCorrectMessageForAcAddressZero()
        {
            TransceiverMessage message;

            var buffer = new byte[] { 0x04, 0x02, 0x01, 0x01, 0x03 };
            var result = TransceiverMessage.TryParse(buffer, out message);

            result.ShouldBeTrue();
            message.PacketLength.ShouldEqual((byte)0x04);
            message.PacketType.ShouldEqual(PacketType.TransceiverMessage);
            message.SequenceNumber.ShouldEqual((byte)0x01);
            message.SubType.ShouldEqual(TransceiverMessageSubType.Response);
            message.Response.ShouldEqual(TransceiverResponse.AcAddressZero);

        }

        [Fact]
        public void TryParse_ShouldConstructCorrectMessageForErrorResponse()
        {
            TransceiverMessage message;

            var buffer = new byte[] { 0x04, 0x02, 0x00, 0x01, 0x00 };
            var result = TransceiverMessage.TryParse(buffer, out message);

            result.ShouldBeTrue();
            message.PacketLength.ShouldEqual((byte)0x04);
            message.PacketType.ShouldEqual(PacketType.TransceiverMessage);
            message.SequenceNumber.ShouldEqual((byte)0x01);
            message.SubType.ShouldEqual(TransceiverMessageSubType.Error);
            message.Response.ShouldBeNull();

        }
    }
}