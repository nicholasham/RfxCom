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
            return SubType == TransceiverResponseSubType.Error 
                ? $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response Type: Error, Message: receiver did not lock Id" 
                : $"PacketType: {PacketType}, Sequence Number: {SequenceNumber}, Response Type: Transmitter, Response: {TransmitterResponse.First()}";
        }
    }
}