using System.Linq;
using LanguageExt;

namespace RfxCom.Messages.TransceiverResponse
{
    public class TransceiverMessage : IMessage
    {
        public TransceiverMessage(byte sequenceNumber, TransceiverResponseSubType subType, Option<TransmitterResponse> transmitterResponse)
        {
            PacketType = PacketType.TransceiverMessage;
            SequenceNumber = sequenceNumber;
            SubType = subType;
            TransmitterResponse = transmitterResponse;
        }

        public PacketType PacketType { get; }
        public byte SequenceNumber { get; }

        public TransceiverResponseSubType SubType { get; }

        public Option<TransmitterResponse> TransmitterResponse { get; }

        public override string ToString()
        {
            if (SubType == TransceiverResponseSubType.Error)
            {
                return $"Receiver Error - PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Message: receiver did not lock Id";
            }
            else
            {
                return $"Transmitter Response - PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response: {TransmitterResponse.First()}";
            }
            
        }
    }
}