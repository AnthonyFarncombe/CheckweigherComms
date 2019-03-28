namespace CheckweigherComms
{
    class SendData
    {
        private const byte DLE = 0x10;
        private const byte STX = 0x2;
        private const byte ETX = 0x3;
        private const int bytesLength = 45;

        public byte Error { get; set; } = 30;
        public byte Status { get; set; }
        public short LiveWeight { get; set; }
        public short TargetWeight { get; set; }
        public short MinWeight { get; set; }
        public short MaxWeight { get; set; }
        public uint GoodPacks { get; set; }
        public uint UnderweightPacks { get; set; }
        public uint OverweightPacks { get; set; }
        public uint OverlengthPacks { get; set; }
        public byte DigitalInputs { get; set; }
        public byte DigitalOutputs { get; set; }
        public ushort RatePerSecond { get; set; }
        public uint ADBits { get; set; }
        public ushort DynamicCalibrationWeight1 { get; set; }
        public ushort DynamicCalibrationWeight2 { get; set; }
        public short StandardDeviation { get; set; }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[bytesLength];
            bytes[0] = DLE;
            bytes[1] = STX;
            bytes[2] = Error;
            bytes[3] = Status;
            InsertBytes(LiveWeight, bytes, 4);
            InsertBytes(TargetWeight, bytes, 6);
            InsertBytes(MinWeight, bytes, 8);
            InsertBytes(MaxWeight, bytes, 10);
            InsertBytes(GoodPacks, bytes, 12);
            InsertBytes(UnderweightPacks, bytes, 16);
            InsertBytes(OverweightPacks, bytes, 20);
            InsertBytes(OverlengthPacks, bytes, 24);
            bytes[28] = DigitalInputs;
            bytes[29] = DigitalOutputs;
            InsertBytes(RatePerSecond, bytes, 30);
            InsertBytes(ADBits, bytes, 32);
            InsertBytes(DynamicCalibrationWeight1, bytes, 36);
            InsertBytes(DynamicCalibrationWeight2, bytes, 38);
            InsertBytes(StandardDeviation, bytes, 40);
            bytes[42] = DLE;
            bytes[43] = ETX;
            return bytes;
        }

        private unsafe void InsertBytes(short value, byte[] buffer, int offset)
        {
            fixed (byte* ptr = &buffer[offset])
                *(short*)ptr = (short)((value & 0x00FFU) << 8 | (value & 0xFF00U) >> 8);
        }

        private unsafe void InsertBytes(ushort value, byte[] buffer, int offset)
        {
            fixed (byte* ptr = &buffer[offset])
                *(ushort*)ptr = (ushort)((value & 0x00FFU) << 8 | (value & 0xFF00U) >> 8);
        }

        private unsafe void InsertBytes(uint value, byte[] buffer, int offset)
        {
            fixed (byte* ptr = &buffer[offset])
                *(uint*)ptr = (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }
    }
}
