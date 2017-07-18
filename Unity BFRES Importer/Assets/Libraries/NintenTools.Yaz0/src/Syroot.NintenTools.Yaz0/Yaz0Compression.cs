using System.IO;
using Syroot.BinaryData;

namespace Syroot.NintenTools.Yaz0
{
    /// <summary>
    /// Represents a collection of methods to decompress Yaz0-compressed data.
    /// </summary>
    /// <remarks>If data is decompressed into a <see cref="MemoryStream"/>, no buffering has to be done, which is why
    /// there are specific overloads for <see cref="MemoryStream"/> instances. Buffering is required because of the high
    /// amount of seeking to read self-referencing data chunks.</remarks>
    public static class Yaz0Compression
    {
        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Decompresses the Yaz0-compressed contents of the file with the given name and writes them into the file
        /// with the given output name. The decompression is done in memory before it is written back to the output
        /// stream.
        /// </summary>
        /// <param name="inputFile">The name of the file from which the Yaz0-compressed data will be read.</param>
        /// <param name="outputFile">The name of the file to which the decompressed data will be written.</param>
        /// <returns>The number of decompressed bytes written to the output file.</returns>
        public static int Decompress(string inputFile, string outputFile)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                return Decompress(input, output);
            }
        }

        /// <summary>
        /// Decompresses the Yaz0-compressed contents of the file with the given name and writes them into the given
        /// output <see cref="Stream"/>. The decompression is done in memory before it is written back to the output
        /// stream. The stream stays open after this method returned the number of decompressed bytes written.
        /// </summary>
        /// <param name="inputFile">The name of the file from which the Yaz0-compressed data will be read.</param>
        /// <param name="output">The output <see cref="Stream"/> to which the decompressed data will be written.</param>
        /// <returns>The number of decompressed bytes written to the output stream.</returns>
        public static int Decompress(string inputFile, Stream output)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Decompress(input, output);
            }
        }

        /// <summary>
        /// Decompresses the Yaz0-compressed contents of the file with the given name and writes them into the given
        /// output <see cref="MemoryStream"/>. The stream stays open after this method returned the number of
        /// decompressed bytes written.
        /// </summary>
        /// <param name="inputFile">The name of the file from which the Yaz0-compressed data will be read.</param>
        /// <param name="output">The output <see cref="MemoryStream"/> to which the decompressed data will be written
        /// directly.</param>
        /// <returns>The number of decompressed bytes written to the output stream.</returns>
        public static int Decompress(string inputFile, MemoryStream output)
        {
            using (FileStream input = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Decompress(input, output);
            }
        }

        /// <summary>
        /// Decompresses the Yaz0-compressed contents of the input <see cref="Stream"/> and writes them into the file
        /// with the given output name. The decompression is done in memory before it is written back to the output
        /// stream. The stream stays open after this method returned the number of decompressed bytes written.
        /// </summary>
        /// <param name="input">The input <see cref="Stream"/> from which the Yaz0-compressed data will be read.</param>
        /// <param name="outputFile">The name of the file to which the decompressed data will be written.</param>
        /// <returns>The number of decompressed bytes written to the output file.</returns>
        public static int Decompress(Stream input, string outputFile)
        {
            using (FileStream output = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                return Decompress(input, output);
            }
        }

        /// <summary>
        /// Decompresses the Yaz0-compressed contents of the input <see cref="Stream"/> and writes them into the given
        /// output <see cref="Stream"/>. The decompression is done in memory before it is written back to the output
        /// stream. Both streams stay open after this method returned the number of decompressed bytes written.
        /// </summary>
        /// <param name="input">The input <see cref="Stream"/> from which the Yaz0-compressed data will be read.</param>
        /// <param name="output">The output <see cref="Stream"/> to which the decompressed data will be written.</param>
        /// <returns>The number of decompressed bytes written to the output stream.</returns>
        public static int Decompress(Stream input, Stream output)
        {
            // For any stream not being a memory stream, write to a memory buffer first before writing it to the output.
            int decompressedBytes;
            using (MemoryStream decompressionBuffer = new MemoryStream())
            {
                decompressedBytes = Decompress(input, decompressionBuffer);
                decompressionBuffer.WriteTo(output);
            }
            return decompressedBytes;
        }

        /// <summary>
        /// Decompresses the Yaz0-compressed contents of the input <see cref="Stream"/> and writes them directly into
        /// the given output <see cref="MemoryStream"/>. Both streams stay open after this method returned the number of
        /// decompressed bytes written.
        /// </summary>
        /// <param name="input">The input <see cref="Stream"/> from which the Yaz0-compressed data will be read.</param>
        /// <param name="output">The output <see cref="MemoryStream"/> to which the decompressed data will be written
        /// directly.</param>
        /// <returns>The number of decompressed bytes written to the output stream.</returns>
        public static int Decompress(Stream input, MemoryStream output)
        {
            using (BinaryDataReader reader = new BinaryDataReader(input, true))
            using (BinaryDataWriter writer = new BinaryDataWriter(output, true))
            {
                reader.ByteOrder = ByteOrder.BigEndian;

                // Read and check the header.
                if (reader.ReadString(4) != "Yaz0")
                {
                    throw new Yaz0Exception("Invalid Yaz0 header.");
                }
                uint decompressedSize = reader.ReadUInt32();
                reader.Position += 8; // Padding

                // Decompress the data.
                int decompressedBytes = 0;
                while (decompressedBytes < decompressedSize)
                {
                    // Read the configuration byte of a decompression setting group, and go through each bit of it.
                    byte groupConfig = reader.ReadByte();
                    for (int i = 7; i >= 0; i--)
                    {
                        // Check if bit of the current chunk is set.
                        if ((groupConfig & (1 << i)) == (1 << i))
                        {
                            // Bit is set, copy 1 raw byte to the output.
                            writer.Write(reader.ReadByte());
                            decompressedBytes++;
                        }
                        else if (decompressedBytes < decompressedSize) // This does not make sense for last byte.
                        {
                            // Bit is not set and data copying configuration follows, either 2 or 3 bytes long.
                            ushort dataBackSeekOffset = reader.ReadUInt16();
                            int dataSize;
                            // If the nibble of the first back seek offset byte is 0, the config is 3 bytes long.
                            byte nibble = (byte)(dataBackSeekOffset >> 12/*1 byte (8 bits) + 1 nibble (4 bits)*/);
                            if (nibble == 0)
                            {
                                // Nibble is 0, the number of bytes to read is in third byte, which is (size + 0x12).
                                dataSize = reader.ReadByte() + 0x12;
                            }
                            else
                            {
                                // Nibble is not 0, and determines (size + 0x02) of bytes to read.
                                dataSize = nibble + 0x02;
                                // Remaining bits are the real back seek offset.
                                dataBackSeekOffset &= 0x0FFF;
                            }
                            // Since bytes can be reread right after they were written, write and read bytes one by one.
                            for (int j = 0; j < dataSize; j++)
                            {
                                // Read one byte from the current back seek position.
                                writer.Position -= dataBackSeekOffset + 1;
                                byte readByte = (byte)writer.BaseStream.ReadByte();
                                // Write the byte to the end of the memory stream.
                                writer.Seek(0, SeekOrigin.End);
                                writer.Write(readByte);
                                decompressedBytes++;
                            }
                        }
                    }
                }
                return decompressedBytes;
            }
        }
    }
}