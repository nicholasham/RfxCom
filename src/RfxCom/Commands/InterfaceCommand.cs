using RfxCom.Messages;

namespace RfxCom.Commands
{
    public abstract class InterfaceCommand : Command
    {
        private const byte PacketLength = 0x0D;

        protected byte[] ToBytes(InterfaceCommandType interfaceCommandType)
        {
            return new[] {PacketLength, PacketType.InterfaceCommand, SubType.ModeCommand, SequenceNumber, interfaceCommandType};
        }
    }
}