using System;
using System.Collections.Generic;
using System.Linq;

namespace RfxCom.Messages
{
    public class InterfaceMessage : Message
    {
        public InterfaceMessage(byte packetLength, PacketType packetType, InterfaceMessageSubType subType, byte sequenceNumber, InterfaceCommandType commandType, TransceiverType transceiverType, Protocol[] protocols)
        {
            PacketLength = packetLength;
            PacketType = packetType;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            CommandType = commandType;
            TransceiverType = transceiverType;
            Protocols = protocols;
        }

        public byte PacketLength { get; private set; }
        public PacketType PacketType { get; set; }
        public InterfaceMessageSubType SubType { get; private set; }
        public byte SequenceNumber { get; private set; }
        public InterfaceCommandType CommandType { get; private set; }
        public TransceiverType TransceiverType { get; private set; }
        public Protocol[] Protocols { get; private set; }
        
        public override byte[] ToBytes()
        {
            return new[]
            {
                PacketLength, 
                PacketType, 
                SubType, 
                SequenceNumber, 
                CommandType, 
                TransceiverType, 
            };
        }

        public static bool TryParse(byte[] bytes, out InterfaceMessage message)
        {
            message = default (InterfaceMessage);

            if (bytes.Length != 14)
            {
                return false;
            }

            var packetLength = bytes[0];
            PacketType packetType;

            if (PacketType.TryParse(bytes[1], out packetType) && packetType != PacketType.InterfaceMessage)
            {
                return false;
            }

            var subType = InterfaceMessageSubType.Parse(bytes[2], InterfaceMessageSubType.WrongCommandReceived); ;
            var sequenceNumber = bytes[3];
            var command = InterfaceCommandType.Parse(bytes[4], InterfaceCommandType.GetStatus);
            var message1 = TransceiverType.Parse(bytes[5], TransceiverType.Default);

            var message3 = bytes[7];
            var message4 = bytes[8];
            var message5 = bytes[9];

            var enabledProtocols1 = Protocol.ListEnabled(message3).Where(x => x.MessageNumber == 3);
            var enabledProtocols2 = Protocol.ListEnabled(message4).Where(x => x.MessageNumber == 4);
            var enabledProtocols3 = Protocol.ListEnabled(message5).Where(x => x.MessageNumber == 5);
            var enabledProtocols = enabledProtocols1.Concat(enabledProtocols2).Concat(enabledProtocols3).ToArray();
            
            message = new InterfaceMessage(packetLength, packetType, subType, sequenceNumber, command, message1, enabledProtocols);

            return true;
        }

        public override string ToString()
        {
            var protocols = string.Join(",", Protocols.Select(x => x.Description));
            return String.Format("Frequency: {0}, Enabled Protocols: {1}", TransceiverType.Description, protocols);
        }
    }
}