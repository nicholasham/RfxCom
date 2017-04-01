using System;
using System.Linq;
using LanguageExt;
using RfxCom.Messages;

namespace RfxCom
{
    public class Packet
    {
        public Packet(byte length, PacketType type, byte subType, byte sequenceNumber, params byte[] data)
        {
            Length = length;
            Type = type;
            SubType = subType;
            SequenceNumber = sequenceNumber;
            Data = data;
        }

        public byte Length { get; }
        public PacketType Type { get; }

        public byte SubType { get; }

        public byte SequenceNumber { get; }

        public byte[] Data { get; }

        public static Option<Packet> Parse(byte[] bytes)
        {
            return bytes.Length < 4
                ? Option<Packet>.None
                : new Packet(bytes[0], (PacketType) bytes[1], bytes[2], bytes[3], bytes.Skip(4).ToArray());
        }

        public static Option<Packet> Parse(string value)
        {
            Option<Packet> ParseString(string byteString)
            {
                return Parse(byteString.Split(' ')
                    .Select(ConvertToByte).ToArray());
            }

            byte ConvertToByte(string byteValue)
            {
                return Convert.ToByte($"0x{byteValue}", 16);
            }

            return string.IsNullOrEmpty(value) ? Option<Packet>.None : ParseString(value);
        }

        public static implicit operator byte[](Packet packet)
        {
            return packet.ToBytes();
        }

        private  byte[] ToBytes()
        {
            return new[] {Length, (byte) Type, SubType, SequenceNumber}.Concat(Data).ToArray();
        }

        public override string ToString()
        {
            var bytes = ToBytes();
            var result = string.Join(" ", bytes.Select(x => $"{x:X2}"));
            return result;
        }
    }
}