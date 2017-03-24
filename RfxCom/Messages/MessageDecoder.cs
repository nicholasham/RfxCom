using System.Collections.Generic;
using LanguageExt;
using RfxCom.Messages.Chimes;
using static RfxCom.Messages.Enum;

namespace RfxCom.Messages
{
    public class MessageCodec : IMessageCodec
    {
        private static IDictionary<PacketType, IMessageDecoder> Decoders => new Dictionary<PacketType, IMessageDecoder>
        {
            {PacketType.Chime, new ChimeDecoder()}
        };

        private static IDictionary<PacketType, IMessageEncoder> Encoders => new Dictionary<PacketType, IMessageEncoder>
        {
            {PacketType.Chime, new ChimeEncoder()}
        };

        public Option<IMessage> Decode(byte[] data)
        {
            return DecodePacketType(data).Bind(Decoders.TryGetValue).Bind(decoder => decoder.Decode(data));
        }

        public Option<byte[]> Encode(IMessage message)
        {
            return Encoders.TryGetValue(message.PacketType).Bind(encoder => encoder.Encode(message));
        }

        private static Option<PacketType> DecodePacketType(IEnumerable<byte> bytes)
        {
            return bytes.HeadOrNone().Bind(ParseEnum<PacketType>).Head();
        }
    }
}