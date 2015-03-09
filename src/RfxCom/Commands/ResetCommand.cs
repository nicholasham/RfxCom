using System;
using System.Collections.Generic;
using System.Linq;
using RfxCom.Messages;

namespace RfxCom.Commands
{
    /// <summary>
    /// </summary>
    public class ResetCommand : InterfaceCommand
    {
        public override IEnumerable<Byte> ToBytes()
        {
            return ToBytes(InterfaceCommandType.Reset)
                .Union(Enumerable.Repeat<Byte>(0x00, 9));
        }
    }
}