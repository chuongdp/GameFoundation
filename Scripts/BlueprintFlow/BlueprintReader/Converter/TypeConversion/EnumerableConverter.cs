// Copyright 2009-2021 Josh Close
// This file is a part of CsvHelper and is dual licensed under MS-PL and Apache 2.0.
// See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html for MS-PL and http://opensource.org/licenses/Apache-2.0 for Apache 2.0.
// https://github.com/JoshClose/CsvHelper

namespace BlueprintFlow.BlueprintReader.Converter.TypeConversion
{
    using System;
    using System.Collections;

    /// <summary>
    ///     Throws an exception when used. This is here so that it's apparent
    ///     that there is no support for <see cref="IEnumerable" /> type conversion. A custom
    ///     converter will need to be created to have a field convert to and
    ///     from an IEnumerable.
    /// </summary>
    public class EnumerableConverter : DefaultTypeConverter
    {
        /// <summary>
        ///     Throws an exception.
        /// </summary>
        /// <param name="text">The string to convert to an object.</param>
        /// <param name="typeInfo"></param>
        /// <returns>The object created from the string.</returns>
        public override object ConvertFromString(string text, Type typeInfo)
        {
            var message = "Converting IEnumerable types is not supported for a single field. " + "If you want to do this, create your own ITypeConverter and register " + "it in the TypeConverterFactory by calling AddConverter.";
            throw new(message);
        }

        /// <summary>
        ///     Throws an exception.
        /// </summary>
        /// <param name="value">The object to convert to a string.</param>
        /// <param name="typeInfo"></param>
        /// <returns>The string representation of the object.</returns>
        public override string ConvertToString(object value, Type typeInfo)
        {
            var message = "Converting IEnumerable types is not supported for a single field. " + "If you want to do this, create your own ITypeConverter and register " + "it in the TypeConverterFactory by calling AddConverter.";
            throw new(message);
        }
    }
}