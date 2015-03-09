using System;
using System.Collections.Generic;
using System.Linq;
using RfxCom.Messages;

namespace RfxCom.Commands
{
    public class SetModeCommand : InterfaceCommand
    {
        public SetModeCommand(params Protocol[] protocols)
        {
            TransceiverType = TransceiverType.Default;
            Protocols = protocols;
        }

        public TransceiverType TransceiverType { get; private set; }
        public Protocol[] Protocols { get; private set; }

        public override IEnumerable<byte> ToBytes()
        {
            var messages = new byte[] {TransceiverType, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            for (var number = 1; number <= 5; number++)
            {
                if (Protocols.All(x => x.MessageNumber != number)) continue;

                var value = Protocols
                    .Where(protocol => protocol.MessageNumber == number)
                    .Sum(protocol => protocol.Value);

                messages[number - 1] = Convert.ToByte(value);
            }

            return ToBytes(InterfaceCommandType.SetMode).Union(messages);
        }
    }
}