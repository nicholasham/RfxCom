using System;
using System.Collections.Generic;
using System.Linq;
using RfxCom.Messages;

namespace RfxCom.Commands
{
    public class GetStatusCommand : InterfaceCommand
    {
        public override IEnumerable<byte> ToBytes()
        {
            return ToBytes(InterfaceCommandType.GetStatus)
                .Union(Enumerable.Repeat<Byte>(0x00, 9));
        }
    }
}