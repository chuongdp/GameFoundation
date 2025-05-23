// Copyright 2009-2021 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper

namespace BlueprintFlow.BlueprintReader.Converter.TypeConversion
{
    using System;

    /// <summary>
    ///     Options for converting byte arrays.
    /// </summary>
    [Flags]
    public enum ByteArrayConverterOptions
    {
        /// <summary>
        ///     No options.
        /// </summary>
        None = 0,

        // TypeOptions

        /// <summary>
        ///     Hexadecimal encoding.
        /// </summary>
        Hexadecimal = 1,

        /// <summary>
        ///     Base64 encoding.
        /// </summary>
        Base64 = 2,

        // HexFormattingOptions

        /// <summary>
        ///     Use dashes in between hex values.
        /// </summary>
        HexDashes = 4,

        /// <summary>
        ///     Prefix hex number with 0x.
        /// </summary>
        HexInclude0x = 8,
    }
}