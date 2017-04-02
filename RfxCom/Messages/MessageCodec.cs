using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LanguageExt;

namespace RfxCom.Messages
{
    public class MessageCodec : IMessageCodec
    {
        private readonly IEnumerable<IMessageDecoder> _decoders;
        private readonly IEnumerable<IMessageEncoder> _encoders;

        public MessageCodec()
        {
            _decoders = Get<IMessageDecoder>();
            _encoders = Get<IMessageEncoder>();
        }


        public Option<IMessage> Decode(Packet packet)
        {
            return _decoders
                .Where(decoder => decoder.CanDecode(packet))
                .Select(decoder => decoder.Decode(packet))
                .DefaultIfEmpty(Option<IMessage>.None)
                .First();
        }

        public Option<Packet> Encode(IMessage message)
        {
            return _encoders
                .Where(encoder => encoder.CanEncode(message))
                .Select(encoder => encoder.Encode(message))
                .DefaultIfEmpty(Option<Packet>.None)
                .First();
        }

        private IEnumerable<T> Get<T>()
        {
            Func<TypeInfo, bool> IsConcrete<T>()
            {
                return type => typeof(T).GetTypeInfo().IsAssignableFrom(type) && !type.IsAbstract &&
                               type.IsClass;
            }

            return GetType()
                .GetTypeInfo()
                .Assembly.DefinedTypes
                .Where(IsConcrete<T>())
                .Select(type => Activator.CreateInstance(type.AsType()))
                .Cast<T>()
                .ToArray();
        }
    }
}