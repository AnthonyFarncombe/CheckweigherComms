using System;
using System.Net;

namespace CheckweigherComms
{
    class ReceiveData
    {
        public const int bytesLength = 11;
        private const byte DLE = 0x10;
        private const byte STX = 0x2;
        private const byte ETX = 0x3;

        public ReceiveData(byte[] buffer)
        {
            if (buffer.Length != bytesLength)
                throw new ArgumentException($"Expected {bytesLength} bytes but received {buffer.Length} bytes");
            if (buffer[0] != DLE)
                throw new ArgumentException($"DLE ({DLE}) byte not matched, found {buffer[0]}");
            if (buffer[1] != STX)
                throw new ArgumentException($"STX ({STX}) byte not matched, found {buffer[1]}");

            Command = buffer[2];
            TargetWeight = ReverseBytes(BitConverter.ToInt16(buffer, 3));
            MinWeight = ReverseBytes(BitConverter.ToInt16(buffer, 5));
            MaxWeight = ReverseBytes(BitConverter.ToInt16(buffer, 7));

            if (buffer[9] != DLE)
                throw new ArgumentException($"DLE ({DLE}) byte not matched, found {buffer[9]}");
            if (buffer[10] != ETX)
                throw new ArgumentException($"ETX ({ETX}) byte not matched, found {buffer[10]}");
        }

        public byte Command { get; }
        public short TargetWeight { get; }
        public short MinWeight { get; }
        public short MaxWeight { get; }

        private short ReverseBytes(short value) => (short)((value & 0x00FFU) << 8 | (value & 0xFF00U) >> 8);
    }
}
