using System.Linq;
using System.Threading.Tasks;

namespace RfxCom.Messages
{
    public static class InterfaceControlExtensions
    {
        public async static Task Reset(this ITransceiver transceiver)
        {
            var message = new InterfaceControlMessage(InterfaceControlCommand.Reset);
            await transceiver.Send(message);
            await Task.Delay(50);
        }

        public async static Task GetStatus(this ITransceiver transceiver)
        {
            await transceiver.Send(new InterfaceControlMessage(InterfaceControlCommand.GetStatus));
        }

        public async static Task SetMode(this ITransceiver transceiver, params Protocol[] protocols)
        {
            await transceiver.SetMode(TransceiverType.RfxTrx43392, protocols);
        }

        public async static Task SetMode(this ITransceiver transceiver, TransceiverType transceiverType, params Protocol[] protocols)
        {
            var messages = Buffer.Empty(9);

            messages[0] = transceiverType;
            messages[2] = (byte) protocols.Where(protocol => protocol.MessageNumber == 3).Sum(protocol => protocol.Value);
            messages[3] = (byte) protocols.Where(protocol => protocol.MessageNumber == 4).Sum(protocol => protocol.Value);
            messages[4] = (byte) protocols.Where(protocol => protocol.MessageNumber == 5).Sum(protocol => protocol.Value);

            await transceiver.Send(new InterfaceControlMessage(InterfaceControlCommand.SetMode, messages));

        }
    }
}