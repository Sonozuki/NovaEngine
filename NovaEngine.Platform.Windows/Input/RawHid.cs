namespace NovaEngine.Platform.Windows.Input
{
    /// <summary>Describes the format of the raw input from a Human Interface Device (HID).</summary>
    internal struct RawHid
    {
        /*********
        ** Fields
        *********/
        /// <summary>The size, in <see langword="byte"/>s, of each HID input in <see cref="RawData"/>.</summary>
        public int Size;

        /// <summary>The number of HID inputs in <see cref="RawData"/>.</summary>
        public int Count;

        /// <summary>The raw input data, as an array of <see langword="byte"/>s.</summary>
        public byte RawData;
    }
}
