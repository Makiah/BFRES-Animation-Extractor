using System.Collections.Generic;
using System.IO;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a data buffer holding vertices for a <see cref="Model"/> subfile.
    /// </summary>
    public class VertexBuffer : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FVTX";
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the number of bones influencing the vertices stored in this buffer. 0 influences equal
        /// rigidbodies (no skinning), 1 equal rigid skinning and 2 or more smooth skinning.
        /// </summary>
        public byte VertexSkinCount { get; set; }

        /// <summary>
        /// Gets the number of vertices stored by the <see cref="Buffers"/>. It is calculated from the size of the first
        /// <see cref="Buffer"/> in bytes divided by the <see cref="Buffer.Stride"/>.
        /// </summary>
        public uint VertexCount
        {
            get
            {
                Buffer firstBuffer = Buffers[0];
                int dataSize = firstBuffer.Data[0].Length;
                
                // Throw an exception if the stride does not yield complete elements.
                if (dataSize % firstBuffer.Stride != 0)
                {
                    throw new InvalidDataException($"Stride of {firstBuffer} does not yield complete elements."); 
                }

                return (uint)(dataSize / firstBuffer.Stride);
            }
        }

        /// <summary>
        /// Gets or sets the dictionary of <see cref="VertexAttrib"/> instances describing how to interprete data in the
        /// <see cref="Buffers"/>.
        /// </summary>
        public ResDict<VertexAttrib> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Buffer"/> instances storing raw unformatted vertex data.
        /// </summary>
        public IList<Buffer> Buffers { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            loader.CheckSignature(_signature);
            byte numVertexAttrib = loader.ReadByte();
            byte numBuffer = loader.ReadByte();
            ushort idx = loader.ReadUInt16();
            uint vertexCount = loader.ReadUInt32();
            VertexSkinCount = loader.ReadByte();
            loader.Seek(3);
            uint ofsVertexAttribList = loader.ReadOffset(); // Only load dict.
            Attributes = loader.LoadDict<VertexAttrib>();
            Buffers = loader.LoadList<Buffer>(numBuffer);
            uint userPointer = loader.ReadUInt32();
        }

        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.Write((byte)Attributes.Count);
            saver.Write((byte)Buffers.Count);
            saver.Write((ushort)saver.CurrentIndex);
            saver.Write(VertexCount);
            saver.Write(VertexSkinCount);
            saver.Seek(3);
            saver.SaveList(Attributes.Values);
            saver.SaveDict(Attributes);
            saver.SaveList(Buffers);
            saver.Write(0); // UserPointer
        }
    }
}