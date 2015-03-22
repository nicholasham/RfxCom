using System;

namespace RfxCom.Messages
{
    public class TransceiverMessageSubType : Field<TransceiverMessageSubType>
    {
        public static readonly TransceiverMessageSubType Error = new TransceiverMessageSubType(0x00, "error, receiver did not lock messsage not used");
        public static readonly TransceiverMessageSubType Response = new TransceiverMessageSubType(0x01, "Response received, message used");

        private TransceiverMessageSubType(byte value, string description) : base(value, description)
        {
        }
    }

    public class TransceiverResponse : Field<TransceiverResponse>
    {
        public static readonly TransceiverResponse Ok = new TransceiverResponse(0x00, "ACK, transmit OK");
        public static readonly TransceiverResponse OkButDelay = new TransceiverResponse(0x01, "ACK, but transmit started after 3 seconds delay anyway with RF receive data");
        public static readonly TransceiverResponse DidNotLock = new TransceiverResponse(0x02, "NAK, transmitter did not lock on the requested transmit frequency");
        public static readonly TransceiverResponse AcAddressZero = new TransceiverResponse(0x03, "NAK, AC address zero in id1-id4 not allowed");


        private TransceiverResponse(byte value, string description) : base(value, description)
        {
        }
    }

    /// <summary>
    /// Responses or messages send by the receiver or transmitter
    /// </summary>
    public class TransceiverMessage : Message
    {
        public TransceiverMessage(byte packetLength, PacketType packetType, TransceiverMessageSubType subType, byte sequenceNumber, TransceiverResponse response)
        {
            PacketLength = packetLength;
            PacketType = packetType;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            Response = response;
        }

        public byte PacketLength { get; private set; }
        public PacketType PacketType { get; private set; }
        public TransceiverMessageSubType SubType { get; private set; }
        public Byte SequenceNumber { get; private set; }
        public TransceiverResponse Response { get; set; }

        public override byte[] ToBytes()
        {
            return new[] {PacketLength, PacketType, SubType, SequenceNumber, Response};
        }

        public static bool TryParse(byte[] bytes, out TransceiverMessage message)
        {
            message = null;

            var packetLength = bytes[0];
            PacketType packetType;

            if (!(packetLength == 4 && PacketType.TryParse(bytes[1], out packetType)))
            {
                return false;
            }

            TransceiverMessageSubType subType;
           
            if (!TransceiverMessageSubType.TryParse(bytes[2], out subType))
            {
                return false;
            }

            var sequenceNumber = bytes[3];

            TransceiverResponse response = null;

            if (subType == TransceiverMessageSubType.Response)
            {
                TransceiverResponse.TryParse(bytes[4], out response);
            }

            message = new TransceiverMessage(packetLength, packetType, subType, sequenceNumber, response);

            return true;
        }

        public override string ToString()
        {
            return String.Format("Type: {0}, Response: {1}", SubType.Description, Response.Description);
        }
    }
}