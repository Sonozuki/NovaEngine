namespace NovaEngine.Serialisation
{
    /// <summary>The type of an inlined value.</summary>
    internal enum InlinedValueType
    {
        /// <summary>A one-byte boolean value.</summary>
        Bool = 1,

        /// <summary>A signed byte.</summary>
        SByte,

        /// <summary>An unsigned byte.</summary>
        Byte,

        /// <summary>A unicode character.</summary>
        Char,

        /// <summary>A 16-bit signed integer.</summary>
        Short,

        /// <summary>A 16-bit unsigned integer.</summary>
        UShort,

        /// <summary>A 32-bit signed integer.</summary>
        Int,

        /// <summary>A 32-bit unsigned integer.</summary>
        UInt,

        /// <summary>An 64-bit signed integer.</summary>
        Long,

        /// <summary>An 64-bit unsigned integer.</summary>
        ULong,

        /// <summary>A 32-bit floating-point number.</summary>
        Float,

        /// <summary>An 64-bit floating-point number.</summary>
        Double,

        /// <summary>A 128-bit floating-point number.</summary>
        Decimal,

        /// <summary>A length-prefixed string.</summary>
        String,

        /// <summary>An enumeration.</summary>
        Enum,

        /// <summary>An <see langword="unmanaged"/> <see langword="struct"/>.</summary>
        Unmanaged
    }
}
