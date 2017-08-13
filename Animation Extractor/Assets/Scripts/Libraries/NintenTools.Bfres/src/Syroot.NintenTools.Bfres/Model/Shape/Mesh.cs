using System;
using System.Collections.Generic;
using System.IO;
using Syroot.BinaryData;
using Syroot.NintenTools.Bfres.Core;
using Syroot.NintenTools.Bfres.GX2;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents the surface net of a <see cref="Shape"/> section, storing information on which
    /// index <see cref="Buffer"/> to use for referencing vertices of the shape, mostly used for different levels of
    /// detail (LoD) models.
    /// </summary>
    public class Mesh : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the <see cref="GX2PrimitiveType"/> which determines how indices are used to form polygons.
        /// </summary>
        public GX2PrimitiveType PrimitiveType { get; set; }

        /// <summary>
        /// Gets the <see cref="GX2IndexFormat"/> determining the data type of the indices in the
        /// <see cref="IndexBuffer"/>.
        /// </summary>
        public GX2IndexFormat IndexFormat { get; private set; }

        /// <summary>
        /// Gets the number of indices stored in the <see cref="IndexBuffer"/>.
        /// </summary>
        public uint IndexCount
        {
            get
            {
                // Sum indices in all bufferings together, even if only first is mostly used.
                int elementCount = 0;
                int formatSize = FormatSize;
                for (int i = 0; i < IndexBuffer.Data.Length; i++)
                {
                    int bufferingSize = IndexBuffer.Data[i].Length;
                    if (bufferingSize % formatSize != 0)
                    {
                        throw new InvalidDataException($"Cannot form complete indices from {IndexBuffer}.");
                    }
                    elementCount += bufferingSize / formatSize;
                }
                return (uint)elementCount;
            }
        }

        /// <summary>
        /// Gets or sets the list of <see cref="SubMesh"/> instances which split up a mesh into parts which can be
        /// hidden if they are not visible to optimize rendering performance.
        /// </summary>
        public IList<SubMesh> SubMeshes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Buffer"/> storing the index data.
        /// </summary>
        public Buffer IndexBuffer { get; set; }

        /// <summary>
        /// Gets or sets the offset to the first vertex element of a <see cref="VertexBuffer"/> to reference by indices.
        /// </summary>
        public uint FirstVertex { get; set; }
        
        internal int FormatSize
        {
            get
            {
                switch (IndexFormat)
                {
                    case GX2IndexFormat.UInt16:
                    case GX2IndexFormat.UInt16LittleEndian:
                        return sizeof(ushort);
                    case GX2IndexFormat.UInt32:
                    case GX2IndexFormat.UInt32LittleEndian:
                        return sizeof(uint);
                    default:
                        throw new ArgumentException($"Invalid {nameof(GX2IndexFormat)} {IndexFormat}.", nameof(IndexFormat));
                }
            }
        }

        internal ByteOrder FormatByteOrder
        {
            get
            {
                switch (IndexFormat)
                {
                    case GX2IndexFormat.UInt16LittleEndian:
                    case GX2IndexFormat.UInt32LittleEndian:
                        return ByteOrder.LittleEndian;
                    case GX2IndexFormat.UInt16:
                    case GX2IndexFormat.UInt32:
                        return ByteOrder.BigEndian;
                    default:
                        throw new ArgumentException($"Invalid {nameof(GX2IndexFormat)} {IndexFormat}.", nameof(IndexFormat));
                }
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the indices stored in the <see cref="IndexBuffer"/> as <see cref="UInt32"/> instances.
        /// </summary>
        /// <returns>The indices stored in the <see cref="IndexBuffer"/>.</returns>
        public IEnumerable<uint> GetIndices()
        {
            using (BinaryDataReader reader = new BinaryDataReader(new MemoryStream(IndexBuffer.Data[0])))
            {
                reader.ByteOrder = FormatByteOrder;

                // Read and return the elements.
                uint elementCount = IndexCount;
                switch (IndexFormat)
                {
                    case GX2IndexFormat.UInt16:
                    case GX2IndexFormat.UInt16LittleEndian:
                        for (; elementCount > 0; elementCount--)
                        {
                            yield return reader.ReadUInt16();
                        }
                        break;
                    default:
                        for (; elementCount > 0; elementCount--)
                        {
                            yield return reader.ReadUInt32();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Stores the given <paramref name="indices"/> in the <see cref="IndexBuffer"/> in the provided
        /// <paramref name="format"/>, or the current <see cref="IndexFormat"/> if none was specified.
        /// </summary>
        /// <param name="indices">The indices to store in the <see cref="IndexBuffer"/>.</param>
        /// <param name="format">The <see cref="GX2IndexFormat"/> to use or <c>null</c> to use the current format.
        /// </param>
        public void SetIndices(IList<uint> indices, GX2IndexFormat? format = null)
        {
            IndexFormat = format ?? IndexFormat;
            IndexBuffer.Data = new byte[1][] { new byte[indices.Count * FormatSize] };
            using (BinaryDataWriter writer = new BinaryDataWriter(new MemoryStream(IndexBuffer.Data[0], true)))
            {
                writer.ByteOrder = FormatByteOrder;

                // Write the elements.
                switch (IndexFormat)
                {
                    case GX2IndexFormat.UInt16:
                    case GX2IndexFormat.UInt16LittleEndian:
                        foreach (uint index in indices)
                        {
                            writer.Write((ushort)index);
                        }
                        break;
                    default:
                        writer.Write(indices);
                        break;
                }
            }
        }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            PrimitiveType = loader.ReadEnum<GX2PrimitiveType>(true);
            IndexFormat = loader.ReadEnum<GX2IndexFormat>(true);
            uint indexCount = loader.ReadUInt32();
            ushort numSubMesh = loader.ReadUInt16();
            loader.Seek(2);
            SubMeshes = loader.LoadList<SubMesh>(numSubMesh);
            IndexBuffer = loader.Load<Buffer>();
            FirstVertex = loader.ReadUInt32();
        }

        void IResData.Save(ResFileSaver saver)
        {
            saver.Write(PrimitiveType, true);
            saver.Write(IndexFormat, true);
            saver.Write(IndexCount);
            saver.Write((ushort)SubMeshes.Count);
            saver.Seek(2);
            saver.SaveList(SubMeshes);
            saver.Save(IndexBuffer);
            saver.Write(FirstVertex);
        }
    }
}