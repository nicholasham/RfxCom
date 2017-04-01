using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RfxCom.Messages;
using RfxCom.Messages.Chimes;
using RfxCom.Messages.InterfaceControl;
using Xunit;

namespace RfxCom.UnitTests
{
    public class TransceiverTests
    {
        private readonly TestCommunicationInterface _testCommunicationInterface;
        private readonly Transceiver _transceiver;

        public TransceiverTests()
        {
            _testCommunicationInterface = new TestCommunicationInterface();
            _transceiver = new Transceiver(new MessageCodec(), _testCommunicationInterface);
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
                Array("07 16 00 01 02 03 03 05", new ByronSxChimeMessage(1, 146, ChimeSound.BigBen, 8))
                //new object[] { "07 16 01 01 02 03 0E", new RawMessage((PacketType) 0x16, 0x1, 0x1, new byte []{ 0x02, 0x03, 0x0E})},
                //new object[] { "0D 00 00 01 02 00 00 00 00 00 00 00 00 00" , new GetStatusMessage(1)},
                //new object[] { "0D 00 00 00 00 00 00 00 00 00 00 00 00 00", new ResetMessage(), }
            };
        }

        [Theory]
        [MemberData(nameof(SentMessages))]
        public async Task ShouldSendMessagesCorrectly(IMessage sentMessage, string packetString)
        {
            var expectedPacket = Packet.Parse(packetString).First();
            await _transceiver.Send(sentMessage, CancellationToken.None);
            var actualPacket = _testCommunicationInterface.Buffer.Last();
            actualPacket.ShouldBeEquivalentTo(expectedPacket);
        }

        [Theory]
        [MemberData(nameof(ReceivedMessages))]
        public async Task ShouldReceiveKnownMessagesCorrectly(string byteString, IMessage expectedMessage)
        {
            IMessage actualMessage = null;

            var packet = Packet.Parse(byteString);
            _testCommunicationInterface.Buffer.Add(packet.First());

            await _transceiver.Receive(CancellationToken.None).ForEachAsync(message => actualMessage = message);

            actualMessage.ShouldBeEquivalentTo(expectedMessage);
        }
    }
}