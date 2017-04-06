using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RfxCom.Messages;
using RfxCom.Messages.Chimes;
using RfxCom.Messages.InterfaceControl;
using RfxCom.Messages.InterfaceResponse;
using Xunit;
using System;
using Microsoft.Reactive.Testing;

namespace RfxCom.UnitTests
{
    public class TransceiverTests
    {
        private readonly TestCommunicationDevice _testCommunicationDevice;
        private readonly Transceiver _transceiver;

        public TransceiverTests()
        {
            _testCommunicationDevice = new TestCommunicationDevice();
            _transceiver = new Transceiver(new MessageCodec(), _testCommunicationDevice);
        }

        public static IEnumerable<object[]> SentMessages()
        {
            return new[]
            {
               Array(new ByronSxChimeMessage(1, 770, ChimeSound.Clarinet, 5), "07 16 00 01 02 03 0E 05"),
               Array(new ResetMessage(), "0D 00 00 00 00 00 00 00 00 00 00 00 00 00"),
               Array(new GetStatusMessage(1), "0D 00 00 01 02 00 00 00 00 00 00 00 00 00"),
               Array(new SetModeMessage(0, TransceiverType.RfxTrx43392, Protocol.EnableDisplayOfUndecoded,Protocol.LaCrosse, Protocol.OregonScientific, Protocol.Ac, Protocol.Arc, Protocol.X10),"0D 00 00 00 03 53 00 80 08 27 00 00 00 00"),
               Array(new SaveModeMessage(1), "0D 00 00 01 06 00 00 00 00 00 00 00 00 00")
            };
        }


        private static object[] Array(IMessage message, string packetString)
        {
            return new object[] {message, packetString};
        }

        private static object[] Array(string packetString, IMessage message)
        {
            return new object[] {packetString, message};
        }

        public static IEnumerable<object[]> ReceivedMessages()
        {
            return new[]
            {
                Array("07 16 00 01 02 03 0E 05", new ByronSxChimeMessage(1, 770, ChimeSound.Clarinet, 5)),
                Array("07 16 00 01 02 03 03 05", new ByronSxChimeMessage(1, 146, ChimeSound.BigBen, 8)),
                Array("0D 01 00 00 03 53 00 80 08 27 00 00 00 00", new SetModeResponseMessage(0, TransceiverType.RfxTrx43392,Protocol.EnableDisplayOfUndecoded,Protocol.LaCrosse, Protocol.OregonScientific, Protocol.Ac, Protocol.Arc, Protocol.X10 ))
            };
        }

        [Theory]
        [MemberData(nameof(SentMessages))]
        public async Task ShouldSendMessagesCorrectly(IMessage sentMessage, string packetString)
        {
            var expectedPacket = Packet.Parse(packetString).First();
            await _transceiver.SendAsync(sentMessage, CancellationToken.None);
            var actualPacket = _testCommunicationDevice.Buffer.Last();
            actualPacket.ShouldBeEquivalentTo(expectedPacket);
        }

        [Theory]
        [MemberData(nameof(ReceivedMessages))]
        public async Task ShouldReceiveMessagesCorrectly(string byteString, IMessage expectedMessage)
        {

            var packet = Packet.Parse(byteString);
            _testCommunicationDevice.Buffer.Add(packet.First());

            var messages = await _transceiver.ReceiveAsync(CancellationToken.None);
            var actualMessage = messages.First();

            actualMessage.ShouldBeEquivalentTo(expectedMessage);
        }
    }
}