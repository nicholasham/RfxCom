using System.Linq;
using LanguageExt;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Messages.InterfaceResponse
{
    public class InterfaceResponseMessageEncoder : IMessageDecoder
    {
        protected const byte NotUsed = 0x00;

        public bool CanDecode(Packet packet)
        {
            return packet.Type == PacketType.InterfaceResponse;
        }

        public Option<IMessage> Decode(Packet packet)
        {
            var subType = (InterfaceResponseSubType) packet.SubType;

            switch (subType)
            {
                case InterfaceResponseSubType.ModeCommandResponse:
                    return DecodeModeCommandResponse(packet);
                case InterfaceResponseSubType.UnknownRtsRemote:
                    return new UnknownRtsRemoteMessage(packet.SequenceNumber);
                case InterfaceResponseSubType.NoExtendedHardware:
                    return new NoExtendedHardwarePresent(packet.SequenceNumber);
                case InterfaceResponseSubType.ListRfyRemotes:
                    return DecodeListRfyRemotesMessage(packet);
                case InterfaceResponseSubType.WrongCommandReceived:
                    return new WrongCommandReceivedMessage(packet.SequenceNumber);
            }

            return Option<IMessage>.None;
        }

        private static ListRfyRemotesMessage DecodeListRfyRemotesMessage(Packet packet)
        {
            return new ListRfyRemotesMessage(packet.SequenceNumber, packet.Data[0], packet.Data[1], packet.Data[2], packet.Data[3], packet.Data[4]);
        }

        private static Option<IMessage> DecodeModeCommandResponse(Packet packet)
        {
            var message1 = (TransceiverType) packet.Data[1];
            var message3Protocols = Protocols.DecodeMessage3(packet.Data[3]);
            var message4Protocols = Protocols.DecodeMessage4(packet.Data[4]);
            var message5Protocols = Protocols.DecodeMessage5(packet.Data[5]);
            var protocols = message3Protocols.Concat(message4Protocols).Concat(message5Protocols).ToArray();

            return new SetModeResponseMessage(packet.SequenceNumber, message1, protocols);
        }
    }
}