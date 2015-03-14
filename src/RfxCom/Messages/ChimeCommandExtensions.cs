using System.Threading.Tasks;

namespace RfxCom.Messages
{
    public static class ChimeCommandExtensions
    {
        public async static Task SendChime(this ITransceiver transceiver, ChimeSubType subType, ChimeSound sound)
        {
            var message = new ChimeMessage(subType, 0x00, 0x00, 0x00, sound,  0x00);
            await transceiver.Send(message);
        }
    }
}