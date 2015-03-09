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


    public class TransceiverType : Field<TransceiverType>
    {
        public static readonly TransceiverType Type1 = new TransceiverType(0x50, "310.00 MHZ");
        public static readonly TransceiverType Type2 = new TransceiverType(0x51, "315.00 MHZ");
        public static readonly TransceiverType Type3 = new TransceiverType(0x52, "433.92 MHZ Receiver Only");
        public static readonly TransceiverType Type4 = new TransceiverType(0x53, "433.92 MHZ Transceiver");
        public static readonly TransceiverType Type5 = new TransceiverType(0x54, "433.42 MHZ");
        public static readonly TransceiverType Type6 = new TransceiverType(0x55, "868.00 MHZ");
        public static readonly TransceiverType Type7 = new TransceiverType(0x56, "868.00 MHZ FSK");
        public static readonly TransceiverType Type8 = new TransceiverType(0x57, "868.30 MHZ");
        public static readonly TransceiverType Type9 = new TransceiverType(0x58, "868.30 MHZ FSK");
        public static readonly TransceiverType Type10 = new TransceiverType(0x59, "868.35 MHZ");
        public static readonly TransceiverType Type11 = new TransceiverType(0x5A, "868.35 MHZ FSK");
        public static readonly TransceiverType Type12 = new TransceiverType(0x5B, "868.95 MHZ");
        public static readonly TransceiverType Unknown = new TransceiverType(0xFF, "Unknown");

        private TransceiverType(byte value, string description) : base(value, description)
        {
        }
    }

    public class ResponseMessage : Message
    {
        public ResponseMessage(byte packetLength, PacketType packetType, ResponseSubType subType, byte sequenceNumber, Command command, TransceiverType message1, Protocol[] protocols)
        {
            PacketLength = packetLength;
            PacketType = packetType;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            Command = command;
            Message1 = message1;
            Protocols = protocols;
        }

        public byte PacketLength { get; private set; }
        public PacketType PacketType { get; set; }
        public ResponseSubType SubType { get; private set; }
        public byte SequenceNumber { get; private set; }
        public Command Command { get; private set; }
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
                Command, 
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
            var command = Command.Parse(bytes[4], Command.GetStatus);
            var message1 = TransceiverType.Parse(bytes[5], TransceiverType.Unknown);
            
            // Protocols
            var enabledProtocols = (from protocol in Protocol.GetAll()
                let index = protocol.MessageNumber + 4
                where protocol.IsEnabled(bytes[index])
                select protocol).ToArray();
            
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