using System;
using System.Linq;
using LanguageExt;
using LanguageExt.SomeHelp;

namespace RfxCom.Messages.TransceiverResponse
{
    public class TransceiverMessageDecoder : IMessageDecoder
    {
        public bool CanDecode(Packet packet)
        {
            return packet.Type == PacketType.TransceiverMessage;
        }

        public Option<IMessage> Decode(Packet packet)
        {
            var subType = (TransceiverResponseSubType)packet.SubType;

            switch (subType)
            {
                case TransceiverResponseSubType.Error:
                    return new TransceiverMessage(packet.SequenceNumber, subType, Option<TransmitterResponse>.None);
                case TransceiverResponseSubType.Response:
                    return new TransceiverMessage(packet.SequenceNumber, subType, packet.Data.Cast<TransmitterResponse>().First());
                default:
                    return Option<IMessage>.None;
            }

        }
    }
}