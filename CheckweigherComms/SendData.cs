namespace CheckweigherComms
{
    class SendData
    {
        private const byte DLE = 0x10;
        private const byte STX = 0x2;
        private const byte ETX = 0x3;
        private const int bytesLength = 91;

        public byte Version { get; set; } = 1;
        public byte Error { get; set; } = 30;
        public byte Status { get; set; }
        public byte CalibrationStatus { get; set; }
        public short LiveWeight { get; set; }
        public short TargetWeight { get; set; }
        public short MinWeight { get; set; }
        public short MaxWeight { get; set; }
        public uint GoodPacks { get; set; }
        public uint UnderweightPacks { get; set; }
        public uint OverweightPacks { get; set; }
        public uint OverlengthPacks { get; set; }
        public uint TimedDiags { get; set; }
        public byte GeneralDiags3 { get; set; }
        public uint GeneralDiags2 { get; set; }
        public uint GeneralDiags1 { get; set; }
        public uint WeigherDiags { get; set; }
        public byte DigitalInputs { get; set; }
        public byte DigitalOutputs { get; set; }
        public ushort RatePerSecond { get; set; }
        public uint ADBits { get; set; }
        public ushort DynamicCalibrationWeight1 { get; set; }
        public ushort DynamicCalibrationWeight2 { get; set; }
        public ushort DynamicCalibrationWeight3 { get; set; }
        public ushort DynamicCalibrationWeight4 { get; set; }
        public ushort DynamicCalibrationWeight5 { get; set; }
        public ushort DynamicCalibrationWeight6 { get; set; }
        public ushort DynamicCalibrationWeight7 { get; set; }
        public ushort DynamicCalibrationWeight8 { get; set; }
        public ushort DynamicCalibrationWeight9 { get; set; }
        public ushort DynamicCalibrationWeight10 { get; set; }
        public ushort DynamicCalibrationWeight11 { get; set; }
        public ushort DynamicCalibrationWeight12 { get; set; }
        public ushort DynamicCalibrationWeight13 { get; set; }
        public ushort DynamicCalibrationWeight14 { get; set; }
        public ushort DynamicCalibrationWeight15 { get; set; }
        public ushort DynamicCalibrationWeight16 { get; set; }
        public short StandardDeviation { get; set; }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[bytesLength];
            bytes[0] = DLE;
            bytes[1] = STX;
            bytes[2] = Error;
            bytes[3] = Version;
            bytes[4] = Status;
            bytes[5] = CalibrationStatus;
            InsertBytes(LiveWeight, bytes, 6);
            InsertBytes(TargetWeight, bytes, 8);
            InsertBytes(MinWeight, bytes, 10);
            InsertBytes(MaxWeight, bytes, 12);
            InsertBytes(GoodPacks, bytes, 14);
            InsertBytes(UnderweightPacks, bytes, 18);
            InsertBytes(OverweightPacks, bytes, 22);
            InsertBytes(OverlengthPacks, bytes, 26);
            InsertBytes(TimedDiags, bytes, 30);
            bytes[34] = GeneralDiags3;
            InsertBytes(GeneralDiags2, bytes, 35);
            InsertBytes(GeneralDiags1, bytes, 39);
            InsertBytes(WeigherDiags, bytes, 43);
            bytes[47] = DigitalInputs;
            bytes[48] = DigitalOutputs;
            InsertBytes(RatePerSecond, bytes, 49);
            InsertBytes(ADBits, bytes, 51);
            InsertBytes(DynamicCalibrationWeight1, bytes, 55);
            InsertBytes(DynamicCalibrationWeight2, bytes, 57);
            InsertBytes(DynamicCalibrationWeight3, bytes, 59);
            InsertBytes(DynamicCalibrationWeight4, bytes, 61);
            InsertBytes(DynamicCalibrationWeight5, bytes, 63);
            InsertBytes(DynamicCalibrationWeight6, bytes, 65);
            InsertBytes(DynamicCalibrationWeight7, bytes, 67);
            InsertBytes(DynamicCalibrationWeight8, bytes, 69);
            InsertBytes(DynamicCalibrationWeight9, bytes, 71);
            InsertBytes(DynamicCalibrationWeight10, bytes, 73);
            InsertBytes(DynamicCalibrationWeight11, bytes, 75);
            InsertBytes(DynamicCalibrationWeight12, bytes, 77);
            InsertBytes(DynamicCalibrationWeight13, bytes, 79);
            InsertBytes(DynamicCalibrationWeight14, bytes, 81);
            InsertBytes(DynamicCalibrationWeight15, bytes, 83);
            InsertBytes(DynamicCalibrationWeight16, bytes, 85);
            InsertBytes(StandardDeviation, bytes, 87);
            bytes[89] = DLE;
            bytes[90] = ETX;
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
