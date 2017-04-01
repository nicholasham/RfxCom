using System;
using LanguageExt;

namespace RfxCom.Messages.Chimes
{
    internal class ChimeEncoder : IMessageEncoder
    {
        public bool CanEncode(IMessage message)
        {
            return message.PacketType == PacketType.Chime;
        }

        public Option<Packet> Encode(IMessage message)
        {
            switch (message)
            {
                case ByronSxChimeMessage chimeMessage:
                    return EncodeByronSxChimeMessage(chimeMessage);
                default:
                    return Option<Packet>.None;
            }
        }

        private static Packet EncodeByronSxChimeMessage(ByronSxChimeMessage message)
        {
            var idBytes = BitConverter.GetBytes(message.Id);

            var data = new[]
            {
                idBytes[0],
                idBytes[1],
                (byte) message.Sound,
                message.SignalStrength
            };

            return new Packet(PacketLengths.Chime, PacketType.Chime, 0x00, message.SequenceNumber, data);
        }

    }
}