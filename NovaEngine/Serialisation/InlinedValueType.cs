namespace NovaEngine.Serialisation
{
    /// <summary>The type of an inlined value.</summary>
    internal enum InlinedValueType
    {
        /// <summary>A one-byte boolean value.</summary>
        Bool,

        /// <summary>A signed byte.</summary>
        SByte,

        /// <summary>An unsigned byte.</summary>
        Byte,

        /// <summary>A unicode character.</summary>
        Char,

        /// <summary>A two-byte signed integer.</summary>
        Short,

        /// <summary>A two-byte unsigned integer.</summary>
        UShort,

        /// <summary>A four-byte signed integer.</summary>
        Int,

        /// <summary>A four-byte unsigned integer.</summary>
        UInt,

        /// <summary>An eight-byte signed integer.</summary>
        Long,

        /// <summary>An eight-byte unsigned integer.</summary>
        ULong,

        /// <summary>A four-byte floating-point number.</summary>
        Float,

        /// <summary>An eight-byte floating-point number.</summary>
        Double,

        /// <summary>A decimal number.</summary>
        Decimal,

        /// <summary>A length-prefixed string.</summary>
        String,

        /// <summary>An enumeration.</summary>
        Enum,

        /// <summary>An <see langword="unmanaged"/> <see langword="struct"/>.</summary>
        Unmanaged
    }
}
