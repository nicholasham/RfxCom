using System.Linq;
using LanguageExt;
using RfxCom.Messages.InterfaceControl;

namespace RfxCom.Messages.InterfaceResponse
{
    public enum InterfaceResponseSubType : byte
    {
        ModeCommandResponse = 0x00,
        UnknownRtsRemote = 0x01,
        NoExtendedHardware = 0x02,
        ListRfyRemotes = 0x03,
        WrongCommandReceived = 0xFF
    }

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

            if (subType == InterfaceResponseSubType.ModeCommandResponse)
                return DecodeModeCommandResponse(packet);

            return Option<IMessage>.None;
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