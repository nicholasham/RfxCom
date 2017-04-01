using LanguageExt;

namespace RfxCom.Messages.InterfaceControl
{
    public class InterfaceControlMessageEncoder : IMessageEncoder
    {
        protected const byte NotUsed = 0x00;
      
        public bool CanEncode(IMessage message)
        {
            return message.PacketType == PacketType.InterfaceControl;
        }

        Option<Packet> IMessageEncoder.Encode(IMessage message)
        {
            switch (message)
            {
                case ResetMessage controlMessage:
                    return EncodeResetCommand(controlMessage);
                case SetModeMessage controlMessage:
                    return EncodeSetModeCommand(controlMessage);
                case GetStatusMessage controlMessage:
                    return EncodeGetStatusCommand(controlMessage);
                case SaveModeMessage controlMessage:
                    return EncodeSaveModeCommand(controlMessage);
                default:
                    return Option<Packet>.None;
            }
        }

        private static Packet EncodeGetStatusCommand(IMessage message)
        {
            var data = new[]
            {
                (byte) InterfaceControlCommand.GetStatus,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
            };

            return new Packet(PacketLengths.InterfaceControl, message.PacketType, 0x00, message.SequenceNumber, data);

        }
        private static Packet EncodeSetModeCommand(SetModeMessage message)
        {
            var data = new[]
            {
                (byte) InterfaceControlCommand.SetMode,
                (byte)message.TransceiverType,
                NotUsed,
                message.Protocols.EncodeMessage3(),
                message.Protocols.EncodeMessage4(),
                message.Protocols.EncodeMessage5(),
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed
            };

            return new Packet(PacketLengths.InterfaceControl, message.PacketType, 0x00, message.SequenceNumber, data);
        }


        private static Packet EncodeResetCommand(IMessage message)
        {
            var data = new[]
            {
                (byte) InterfaceControlCommand.Reset,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed
            };
            return new Packet(PacketLengths.InterfaceControl,message.PacketType, 0x00, message.SequenceNumber, data);
        }

        private static Packet EncodeSaveModeCommand(IMessage message)
        {
            var data = new[]
            {
                (byte) InterfaceControlCommand.SaveMode,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed,
                NotUsed
            };
            return new Packet(PacketLengths.InterfaceControl, message.PacketType, 0x00, message.SequenceNumber, data);
        }
    }
}