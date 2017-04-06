using System;
using LanguageExt;
using RfxCom.Messages;
using RfxCom.Messages.Chimes;
using Enum = System.Enum;

namespace RfxCom.Messages.Chimes
{ 
    internal class ChimeDecoder : IMessageDecoder
    {
        public bool CanDecode(Packet packet)
        {
            return packet.Type == PacketType.Chime;
        }

        public Option<IMessage> Decode(Packet packet)
        {
            if (packet.Length != PacketLengths.Chime)
                return Option<IMessage>.None;

            var sequenceNumber = packet.SequenceNumber;
            var id = BitConverter.ToUInt16(packet.Data, 0);
            var signalStrengh = packet.Data[2];

            var sound = (ChimeSound) packet.Data[3];
            return new ByronSxChimeMessage(sequenceNumber, id, sound, signalStrengh);
        }

    }
}