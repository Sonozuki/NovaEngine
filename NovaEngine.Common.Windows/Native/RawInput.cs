namespace NovaEngine.Common.Windows.Native
{
    /// <summary>Contains the raw input from a device.</summary>
    public struct RawInput
    {
        /*********
        ** Fields
        *********/
        /// <summary>The raw input header.</summary>
        public RawInputHeader Header;

        /// <summary>The raw input data.</summary>
        public RawInputData Data;
    }
}
