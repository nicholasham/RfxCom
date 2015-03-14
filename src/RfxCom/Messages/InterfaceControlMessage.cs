using System;
using System.Linq;

namespace RfxCom.Messages
{
    public class InterfaceControlMessage : Message
    {

        public InterfaceControlMessage(InterfaceControlCommand controlCommand)
            : this(0, controlCommand, Buffer.Empty(9))
        {
        }

        public InterfaceControlMessage(InterfaceControlCommand controlCommand, byte[] messages) : this(0, controlCommand, messages)
        {
        }

        public InterfaceControlMessage(byte sequenceNumber, InterfaceControlCommand controlCommand, byte[] messages)
        {
            if (messages.Length != 9)
            {
                throw new ArgumentException("Must be an array of 9 bytes", "messages");
            }

            PacketLength = PacketLengths.InterfaceControl;
            PacketType = PacketType.InterfaceCommand;
            SubType = InterfaceControlSubType.ModeCommand;
            SequenceNumber = sequenceNumber;
            ControlCommand = controlCommand;
            Messages = messages;
        }

        public byte PacketLength { get; private set; }
        public PacketType PacketType { get; set; }
        public InterfaceControlSubType SubType { get; private set; }
        public byte SequenceNumber { get; private set; }
        public InterfaceControlCommand ControlCommand { get; private set; }
        public byte[] Messages { get; private set; }

        public override byte[] ToBytes()
        {
            return new[]
            {
                PacketLength,
                PacketType,
                SubType,
                SequenceNumber,
                ControlCommand,
            }
            .Concat(Messages)
            .ToArray();
        }
    }
}