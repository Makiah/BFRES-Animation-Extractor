using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Syroot.BinaryData.Core;

namespace Syroot.BinaryData
{
    /// <summary>
    /// Represents an extended <see cref="BinaryWriter"/> supporting special file format data types.
    /// </summary>
    [DebuggerDisplay("BinaryDataWriter, Position={Position}")]
    public class BinaryDataWriter : BinaryWriter
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private ByteOrder _byteOrder;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataWriter"/> class based on the specified stream and
        /// using UTF-8 encoding.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <exception cref="ArgumentException">The stream does not support writing or is already closed.</exception>
        /// <exception cref="ArgumentNullException">output is null.</exception>
        public BinaryDataWriter(Stream output)
            : this(output, new UTF8Encoding(), false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataWriter"/> class based on the specified stream, UTF-8
        /// encoding and optionally leaves the stream open.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="leaveOpen"><c>true</c> to leave the stream open after the <see cref="BinaryDataWriter"/> object
        /// is disposed; otherwise <c>false</c>.</param>
        /// <exception cref="ArgumentException">The stream does not support writing or is already closed.</exception>
        /// <exception cref="ArgumentNullException">output is null.</exception>
        public BinaryDataWriter(Stream output, bool leaveOpen)
            : this(output, new UTF8Encoding(), leaveOpen)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataWriter"/> class based on the specified stream and
        /// character encoding.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="ArgumentException">The stream does not support writing or is already closed.</exception>
        /// <exception cref="ArgumentNullException">output or encoding is null.</exception>
        public BinaryDataWriter(Stream output, Encoding encoding)
            : this(output, encoding, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryDataWriter"/> class based on the specified stream and
        /// character encoding, and optionally leaves the stream open.
        /// </summary>
        /// <param name="output">The output stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen"><c>true</c> to leave the stream open after the <see cref="BinaryDataWriter"/> object
        /// is disposed; otherwise <c>false</c>.</param>
        /// <exception cref="ArgumentException">The stream does not support writing or is already closed.</exception>
        /// <exception cref="ArgumentNullException">output or encoding is null.</exception>
        public BinaryDataWriter(Stream output, Encoding encoding, bool leaveOpen)
            : base(output, encoding, leaveOpen)
        {
            Encoding = encoding;
            ByteOrder = ByteOrderHelper.SystemByteOrder;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the byte order used to parse binary data with.
        /// </summary>
        public ByteOrder ByteOrder
        {
            get
            {
                return _byteOrder;
            }
            set
            {
                _byteOrder = value;
                NeedsReversion = _byteOrder != ByteOrderHelper.SystemByteOrder;
            }
        }

        /// <summary>
        /// Gets the encoding used for string related operations where no other encoding has been provided. Due to the
        /// way the underlying <see cref="BinaryWriter"/> is instantiated, it can only be specified at creation time.
        /// </summary>
        public Encoding Encoding
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether multibyte data requires to be reversed before being written, according to
        /// the set <see cref="ByteOrder"/>.
        /// </summary>
        public bool NeedsReversion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the position within the current stream. This is a shortcut to the base stream Position
        /// property.
        /// </summary>
        public long Position
        {
            get { return BaseStream.Position; }
            set { BaseStream.Position = value; }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Aligns the reader to the next given byte multiple.
        /// </summary>
        /// <param name="alignment">The byte multiple.</param>
        public void Align(int alignment)
        {
            Seek((-Position % alignment + alignment) % alignment);
        }

        /// <summary>
        /// Allocates space for an <see cref="Offset"/> which can be satisfied later on.
        /// </summary>
        /// <returns>An <see cref="Offset"/> to satisfy later on.</returns>
        public Offset ReserveOffset()
        {
            return new Offset(this);
        }

        /// <summary>
        /// Allocates space for a given number of <see cref="Offset"/> instances which can be satisfied later on.
        /// </summary>
        /// <param name="count">The number of <see cref="Offset"/> instances to reserve.</param>
        /// <returns>An array of <see cref="Offset"/> instances to satisfy later on.</returns>
        public Offset[] ReserveOffset(int count)
        {
            Offset[] offsets = new Offset[count];
            for (int i = 0; i < count; i++)
            {
                offsets[i] = ReserveOffset();
            }
            return offsets;
        }
        
        /// <summary>
        /// Sets the position within the current stream. This is a shortcut to the base stream Seek method.
        /// </summary>
        /// <param name="offset">A byte offset relative to the current position in the stream.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Seek(long offset)
        {
            return Seek(offset, SeekOrigin.Current);
        }

        /// <summary>
        /// Sets the position within the current stream. This is a shortcut to the base stream Seek method.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain
        /// the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public long Seek(long offset, SeekOrigin origin)
        {
            return BaseStream.Seek(offset, origin);
        }

        /// <summary>
        /// Creates a <see cref="SeekTask"/> to restore the current position after it has been disposed.
        /// </summary>
        /// <returns>The <see cref="SeekTask"/> to be disposed to restore to the current position.</returns>
        public SeekTask TemporarySeek()
        {
            return TemporarySeek(0, SeekOrigin.Current);
        }

        /// <summary>
        /// Creates a <see cref="SeekTask"/> with the given parameters. As soon as the returned <see cref="SeekTask"/>
        /// is disposed, the previous stream position will be restored.
        /// </summary>
        /// <param name="offset">A byte offset relative to the current position in the stream.</param>
        /// <returns>A <see cref="SeekTask"/> to be disposed to undo the seek.</returns>
        public SeekTask TemporarySeek(long offset)
        {
            return TemporarySeek(offset, SeekOrigin.Current);
        }

        /// <summary>
        /// Creates a <see cref="SeekTask"/> with the given parameters. As soon as the returned <see cref="SeekTask"/>
        /// is disposed, the previous stream position will be restored.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="origin">A value of type <see cref="SeekOrigin"/> indicating the reference point used to obtain
        /// the new position.</param>
        /// <returns>A <see cref="SeekTask"/> to be disposed to undo the seek.</returns>
        public SeekTask TemporarySeek(long offset, SeekOrigin origin)
        {
            return new SeekTask(BaseStream, offset, origin);
        }
        
        /// <summary>
        /// Writes a <see cref="Boolean"/> value in the given format to the current stream, with 0 representing
        /// <c>false</c> and 1 representing <c>true</c>.
        /// </summary>
        /// <param name="value">The <see cref="Boolean"/> value to write.</param>
        /// <param name="format">The binary format in which the <see cref="Boolean"/> will be written.</param>
        public void Write(Boolean value, BinaryBooleanFormat format)
        {
            switch (format)
            {
                case BinaryBooleanFormat.NonZeroByte:
                    base.Write(value);
                    break;
                case BinaryBooleanFormat.NonZeroWord:
                    Write(value ? (Int16)1 : (Int16)0);
                    break;
                case BinaryBooleanFormat.NonZeroDword:
                    Write(value ? 1 : 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format),
                        "The specified binary boolean format is invalid.");
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Boolean"/> values to the current stream, with 0 representing
        /// <c>false</c> and 1 representing <c>true</c>.
        /// </summary>
        /// <param name="values">The <see cref="Boolean"/> values to write.</param>
        public void Write(IEnumerable<Boolean> values)
        {
            foreach (Boolean value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Boolean"/> values in the given format to the current stream, with 0
        /// representing <c>false</c> and 1 representing <c>true</c>.
        /// </summary>
        /// <param name="values">The <see cref="Boolean"/> values to write.</param>
        /// <param name="format">The binary format in which the <see cref="Boolean"/> values will be written.</param>
        public void Write(IEnumerable<Boolean> values, BinaryBooleanFormat format)
        {
            foreach (bool value in values)
            {
                Write(value, format);
            }
        }

        /// <summary>
        /// Writes a <see cref="DateTime"/> value to this stream.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to write.</param>
        public void Write(DateTime value)
        {
            Write(value, BinaryDateTimeFormat.NetTicks);
        }

        /// <summary>
        /// Writes a <see cref="DateTime"/> value to this stream. The <see cref="DateTime"/> will be available in the
        /// specified binary format.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to write.</param>
        /// <param name="format">The binary format in which the <see cref="DateTime"/> will be written.</param>
        public void Write(DateTime value, BinaryDateTimeFormat format)
        {
            switch (format)
            {
                case BinaryDateTimeFormat.CTime:
                    Write((uint)(new DateTime(1970, 1, 1) - value.ToLocalTime()).TotalSeconds);
                    break;
                case BinaryDateTimeFormat.NetTicks:
                    Write(value.Ticks);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format),
                        "The specified binary date time format is invalid.");
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="DateTime"/> values to this stream.
        /// </summary>
        /// <param name="values">The <see cref="DateTime"/> values to write.</param>
        public void Write(IEnumerable<DateTime> values)
        {
            foreach (DateTime value in values)
            {
                Write(value, BinaryDateTimeFormat.NetTicks);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="DateTime"/> values to this stream. The <see cref="DateTime"/> values
        /// will be available in the specified binary format.
        /// </summary>
        /// <param name="values">The <see cref="DateTime"/> values to write.</param>
        /// <param name="format">The binary format in which the <see cref="DateTime"/> values will be written.</param>
        public void Write(IEnumerable<DateTime> values, BinaryDateTimeFormat format)
        {
            foreach (DateTime value in values)
            {
                Write(value, format);
            }
        }

        /// <summary>
        /// Writes an 16-byte floating point value to this stream and advances the current position of the stream by
        /// sixteen bytes.
        /// </summary>
        /// <param name="value">The <see cref="Decimal"/> value to write.</param>
        public override void Write(Decimal value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = DecimalToBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Decimal"/> values to the current stream and advances the current
        /// position by that number of <see cref="Decimal"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="Decimal"/> values to write.</param>
        public void Write(IEnumerable<Decimal> values)
        {
            foreach (Decimal value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an 8-byte floating point value to this stream and advances the current position of the stream by
        /// eight bytes.
        /// </summary>
        /// <param name="value">The <see cref="Double"/> value to write.</param>
        public override void Write(Double value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Double"/> values to the current stream and advances the current position
        /// by that number of <see cref="Double"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="Double"/> values to write.</param>
        public void Write(IEnumerable<Double> values)
        {
            foreach (Double value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an enum value to this stream and advances the current position of the stream by the size of the
        /// underlying enum type size. Optionally validates the value to be defined in the enum type.
        /// </summary>
        /// <param name="value">The enum value to write.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="ArgumentOutOfRangeException"/> if the value is not
        /// defined in the enum type.</param>
        public void Write<T>(T value, bool strict) where T : struct, IComparable, IFormattable // enum
        {
            WriteEnum(typeof(T), value, strict);
        }
        
        /// <summary>
        /// Writes an enumeration of enum values to this stream and advances the current position of the stream by the
        /// size of the underlying enum type size multiplied by the number of values. Optionally validates the values to
        /// be defined in the enum type.
        /// </summary>
        /// <param name="values">The enum values to write.</param>
        /// <param name="strict"><c>true</c> to raise an <see cref="ArgumentOutOfRangeException"/> if a value is not
        /// defined in the enum type.</param>
        public void Write<T>(IEnumerable<T> values, bool strict) where T : struct, IComparable, IFormattable // enum
        {
            foreach (T value in values)
            {
                Write(value, strict);
            }
        }

        /// <summary>
        /// Writes an 2-byte signed integer to this stream and advances the current position of the stream by two bytes.
        /// </summary>
        /// <param name="value">The <see cref="Int16"/> value to write.</param>
        public override void Write(Int16 value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Int16"/> values to the current stream and advances the current position
        /// by that number of <see cref="Int16"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="Int16"/> values to write.</param>
        public void Write(IEnumerable<Int16> values)
        {
            foreach (Int16 value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an 4-byte signed integer to this stream and advances the current position of the stream by four
        /// bytes.
        /// </summary>
        /// <param name="value">The <see cref="Int32"/> value to write.</param>
        public override void Write(Int32 value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Int32"/> values to the current stream and advances the current position
        /// by that number of <see cref="Int32"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="Int32"/> values to write.</param>
        public void Write(IEnumerable<Int32> values)
        {
            foreach (Int32 value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an 8-byte signed integer to this stream and advances the current position of the stream by eight
        /// bytes.
        /// </summary>
        /// <param name="value">The <see cref="Int64"/> value to write.</param>
        public override void Write(Int64 value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Int64"/> values to the current stream and advances the current position
        /// by that number of <see cref="Int64"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="Int64"/> values to write.</param>
        public void Write(IEnumerable<Int64> values)
        {
            foreach (Int64 value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an object or enumerable of objects to this stream.
        /// </summary>
        /// <param name="value">The object or enumerable of objects to write.</param>
        public void WriteObject(object value)
        {
            if (value == null)
            {
                return;
            }
            WriteObject(null, BinaryMemberAttribute.Default, value.GetType(), value);
        }

        /// <summary>
        /// Writes an 4-byte floating point value to this stream and advances the current position of the stream by four
        /// bytes.
        /// </summary>
        /// <param name="value">The <see cref="Single"/> value to write.</param>
        public override void Write(Single value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="Single"/> values to the current stream and advances the current position
        /// by that number of <see cref="Single"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="Single"/> values to write.</param>
        public void Write(IEnumerable<Single> values)
        {
            foreach (Single value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes a string to this stream in the current encoding of the <see cref="BinaryDataWriter"/> and advances
        /// the current position of the stream in accordance with the encoding used and the specific characters being
        /// written to the stream. The string will be available in the specified binary format.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to write.</param>
        /// <param name="format">The binary format in which the string will be written.</param>
        public void Write(String value, BinaryStringFormat format)
        {
            Write(value, format, Encoding);
        }

        /// <summary>
        /// Writes a string to this stream with the given encoding and advances the current position of the stream in
        /// accordance with the encoding used and the specific characters being written to the stream. The string will
        /// be available in the specified binary format.
        /// </summary>
        /// <param name="value">The <see cref="String"/> value to write.</param>
        /// <param name="format">The binary format in which the string will be written.</param>
        /// <param name="encoding">The encoding used for converting the string.</param>
        public void Write(String value, BinaryStringFormat format, Encoding encoding)
        {
            switch (format)
            {
                case BinaryStringFormat.ByteLengthPrefix:
                    WriteByteLengthPrefixString(value, encoding);
                    break;
                case BinaryStringFormat.WordLengthPrefix:
                    WriteWordLengthPrefixString(value, encoding);
                    break;
                case BinaryStringFormat.DwordLengthPrefix:
                    WriteDwordLengthPrefixString(value, encoding);
                    break;
                case BinaryStringFormat.VariableLengthPrefix:
                    WriteVariableLengthPrefixString(value, encoding);
                    break;
                case BinaryStringFormat.ZeroTerminated:
                    WriteZeroTerminatedString(value, encoding);
                    break;
                case BinaryStringFormat.NoPrefixOrTermination:
                    WriteNoPrefixOrTerminationString(value, encoding);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format),
                        "The specified binary string format is invalid.");
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="String"/> values to this in the current encoding of the
        /// <see cref="BinaryDataWriter"/>.
        /// </summary>
        /// <param name="values">The <see cref="String"/> value to write.</param>
        public void Write(IEnumerable<String> values)
        {
            foreach (String value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="String"/> values to this stream in the current encoding of the
        /// <see cref="BinaryDataWriter"/>. The strings will be available in the specified binary format.
        /// </summary>
        /// <param name="values">The <see cref="String"/> values to write.</param>
        /// <param name="format">The binary format in which the strings will be written.</param>
        public void Write(IEnumerable<String> values, BinaryStringFormat format)
        {
            foreach (String value in values)
            {
                Write(value, format);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="String"/> values to this stream with the given encoding. The strings
        /// will be available in the specified binary format.
        /// </summary>
        /// <param name="values">The <see cref="String"/> values to write.</param>
        /// <param name="format">The binary format in which the strings will be written.</param>
        /// <param name="encoding">The encoding used for converting the strings.</param>
        public void Write(IEnumerable<String> values, BinaryStringFormat format, Encoding encoding)
        {
            foreach (String value in values)
            {
                Write(value, format, encoding);
            }
        }

        /// <summary>
        /// Writes an 2-byte unsigned integer value to this stream and advances the current position of the stream by
        /// two bytes.
        /// </summary>
        /// <param name="value">The <see cref="UInt16"/> value to write.</param>
        public override void Write(UInt16 value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="UInt16"/> values to the current stream and advances the current position
        /// by that number of <see cref="UInt16"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="UInt16"/> values to write.</param>
        public void Write(IEnumerable<UInt16> values)
        {
            foreach (UInt16 value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an 4-byte unsigned integer value to this stream and advances the current position of the stream by
        /// four bytes.
        /// </summary>
        /// <param name="value">The <see cref="UInt32"/> value to write.</param>
        public override void Write(UInt32 value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="UInt32"/> values to the current stream and advances the current position
        /// by that number of <see cref="UInt32"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="UInt32"/> values to write.</param>
        public void Write(IEnumerable<UInt32> values)
        {
            foreach (UInt32 value in values)
            {
                Write(value);
            }
        }

        /// <summary>
        /// Writes an 8-byte unsigned integer value to this stream and advances the current position of the stream by
        /// eight bytes.
        /// </summary>
        /// <param name="value">The <see cref="UInt64"/> value to write.</param>
        public override void Write(UInt64 value)
        {
            if (NeedsReversion)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                WriteReversed(bytes);
            }
            else
            {
                base.Write(value);
            }
        }

        /// <summary>
        /// Writes an enumeration of <see cref="UInt64"/> values to the current stream and advances the current position
        /// by that number of <see cref="UInt64"/> values multiplied with the size of a single value.
        /// </summary>
        /// <param name="values">The <see cref="UInt64"/> values to write.</param>
        public void Write(IEnumerable<UInt64> values)
        {
            foreach (UInt64 value in values)
            {
                Write(value);
            }
        }

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private void WriteReversed(byte[] bytes)
        {
            Array.Reverse(bytes);
            base.Write(bytes);
        }
        
        // ---- Decimal methods ----

        private byte[] DecimalToBytes(decimal value)
        {
            // Get the bytes of the decimal.
            byte[] bytes = new byte[sizeof(decimal)];
            Buffer.BlockCopy(Decimal.GetBits(value), 0, bytes, 0, sizeof(decimal));
            return bytes;
        }

        // ---- Enum methods ----

        private void WriteEnum(Type type, object value, bool strict)
        {
            // Validate the value to be defined in the enum.
            if (strict && !EnumExtensions.IsValid(type, value))
            {
                throw new InvalidDataException($"Value {value} to write is not defined in the given enum type {type}.");
            }
            WriteObject(null, BinaryMemberAttribute.Default, Enum.GetUnderlyingType(type), value);
        }

        // ---- Object methods ----

        private void WriteObject(object instance, BinaryMemberAttribute attribute, Type type, object value)
        {
            if (attribute.Converter == null)
            {
                if (value == null)
                {
                    return;
                }
                if (type == typeof(String))
                {
                    Write((String)value, attribute.StringFormat);
                }
                else if (type.TryGetEnumerableElementType(out Type elementType))
                {
                    foreach (object element in (IEnumerable)value)
                    {
                        WriteObject(null, BinaryMemberAttribute.Default, elementType, element);
                    }
                }
                else if (type == typeof(Boolean))
                {
                    Write((Boolean)value, attribute.BooleanFormat);
                }
                else if (type == typeof(Byte))
                {
                    Write((Byte)value);
                }
                else if (type == typeof(DateTime))
                {
                    Write((DateTime)value, attribute.DateTimeFormat);
                }
                else if (type == typeof(Decimal))
                {
                    Write((Decimal)value);
                }
                else if (type == typeof(Double))
                {
                    Write((Double)value);
                }
                else if (type == typeof(Int16))
                {
                    Write((Int16)value);
                }
                else if (type == typeof(Int32))
                {
                    Write((Int32)value);
                }
                else if (type == typeof(Int64))
                {
                    Write((Int64)value);
                }
                else if (type == typeof(SByte))
                {
                    Write((SByte)value);
                }
                else if (type == typeof(Single))
                {
                    Write((Single)value);
                }
                else if (type == typeof(UInt16))
                {
                    Write((UInt16)value);
                }
                else if (type == typeof(UInt32))
                {
                    Write((UInt32)value);
                }
                else if (type == typeof(UInt64))
                {
                    Write((UInt32)value);
                }
                else if (type.GetTypeInfo().IsEnum)
                {
                    WriteEnum(type, value, attribute.Strict);
                }
                else
                {
                    WriteCustomObject(type, value, Position);
                }
            }
            else
            {
                // Let a converter do all the work.
                IBinaryConverter converter = BinaryConverterCache.GetConverter(attribute.Converter);
                converter.Write(this, instance, attribute, value);
            }
        }

        private void WriteCustomObject(Type type, object instance, long startOffset)
        {
            TypeData typeData = TypeData.GetTypeData(type);

            // Write inherited members first if required.
            if (typeData.Attribute.Inherit && typeData.TypeInfo.BaseType != null)
            {
                WriteCustomObject(typeData.TypeInfo.BaseType, instance, startOffset);
            }

            // Write members.
            foreach (MemberData member in typeData.Members)
            {
                // Reposition the stream according to offset.
                if (member.Attribute.OffsetOrigin == OffsetOrigin.Begin)
                {
                    Position = startOffset + member.Attribute.Offset;
                }
                else
                {
                    Position += member.Attribute.Offset;
                }

                // Get the value to write.
                object value;
                switch (member.MemberInfo)
                {
                    case FieldInfo field:
                        value = field.GetValue(instance);
                        break;
                    case PropertyInfo property:
                        value = property.GetValue(instance);
                        break;
                    default:
                        throw new InvalidOperationException($"Tried to write an invalid member {member.MemberInfo}.");
                }

                // Write the value and respect settings stored in the member attribute.
                Type elementType = member.Type.GetEnumerableElementType();
                if (elementType == null)
                {
                    WriteObject(instance, member.Attribute, member.Type, value);
                }
                else
                {
                    foreach (object element in (IEnumerable)value)
                    {
                        WriteObject(instance, member.Attribute, member.Type, element);
                    }
                }
            }
        }
        
        // ---- String methods ----

        private void WriteByteLengthPrefixString(string value, Encoding encoding)
        {
            Write((byte)value.Length);
            Write(encoding.GetBytes(value));
        }

        private void WriteWordLengthPrefixString(string value, Encoding encoding)
        {
            Write((short)value.Length);
            Write(encoding.GetBytes(value));
        }

        private void WriteDwordLengthPrefixString(string value, Encoding encoding)
        {
            Write(value.Length);
            Write(encoding.GetBytes(value));
        }

        private void WriteVariableLengthPrefixString(string value, Encoding encoding)
        {
            Write7BitEncodedInt(value.Length);
            Write(encoding.GetBytes(value));
        }

        private void WriteZeroTerminatedString(string value, Encoding encoding)
        {
            Write(encoding.GetBytes(value));
            Write((byte)0);
        }

        private void WriteNoPrefixOrTerminationString(string value, Encoding encoding)
        {
            Write(encoding.GetBytes(value));
        }
    }
}
