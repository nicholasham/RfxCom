using System;
using System.Collections.Generic;
using System.Linq;

namespace RfxCom.Messages
{

    public class ResponseSubType : Field<ResponseSubType>
    {
        public static readonly ResponseSubType ResponseOnModeCommand = new ResponseSubType(0x00, "Response on a mode command");
        public static readonly ResponseSubType UnknownRtsRemote = new ResponseSubType(0x01, "Unknown RTS remote");
        public static readonly ResponseSubType NoExtendedHardware = new ResponseSubType(0x02, "No extended hardware present");
        public static readonly ResponseSubType ListRfyRemotes = new ResponseSubType(0x03, "List RFY remotes");
        public static readonly ResponseSubType WrongCommandReceived = new ResponseSubType(0xFF, "wrong command received from the application"); 

        private ResponseSubType(byte value, string description) : base(value, description)
        {
        }
    }


    public class ResponseMessage : Message
    {
        public ResponseMessage(byte packetLength, PacketType packetType, ResponseSubType subType, byte sequenceNumber, InterfaceCommandType commandType, TransceiverType message1, Protocol[] protocols)
        {
            PacketLength = packetLength;
            PacketType = packetType;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            CommandType = commandType;
            Message1 = message1;
            Protocols = protocols;
        }

        public byte PacketLength { get; private set; }
        public PacketType PacketType { get; set; }
        public ResponseSubType SubType { get; private set; }
        public byte SequenceNumber { get; private set; }
        public InterfaceCommandType CommandType { get; private set; }
        public TransceiverType Message1 { get; private set; }
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
                Message1, 
            };
        }

        public static bool TryParse(byte[] bytes, out ResponseMessage message)
        {
            message = default (ResponseMessage);

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

            var subType = ResponseSubType.Parse(bytes[2], ResponseSubType.WrongCommandReceived); ;
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
            
            message = new ResponseMessage(packetLength, packetType, subType, sequenceNumber, command, message1, enabledProtocols);

            return true;
        }

        public override string ToString()
        {
            var protocols = string.Join(",", Protocols.Select(x => x.Description));
            return String.Format("Frequency: {0}, Enabled Protocols: {1}", Message1.Description, protocols);
        }
    }
}