using System;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;
using Syroot.NintenTools.Bfres.GX2;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an attribute of a <see cref="VertexBuffer"/> describing the data format, type and layout of a
    /// specific data subset in the buffer.
    /// </summary>
    [DebuggerDisplay(nameof(VertexAttrib) + " {" + nameof(Name) + "}")]
    public class VertexAttrib : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in
        /// <see cref="ResDict{VertexAttrib}"/> instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the index of the buffer storing the data in the <see cref="VertexBuffer.Buffers"/> list.
        /// </summary>
        public byte BufferIndex { get; set; }

        /// <summary>
        /// Gets or sets the offset in bytes to the attribute in each vertex.
        /// </summary>
        public ushort Offset { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="GX2AttribFormat"/> determining the type in which attribute data is available.
        /// </summary>
        public GX2AttribFormat Format { get; set; }
        
        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            Name = loader.LoadString();
            BufferIndex = loader.ReadByte();
            loader.Seek(1);
            Offset = loader.ReadUInt16();
            Format = loader.ReadEnum<GX2AttribFormat>(true);
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.SaveString(Name);
            saver.Write(BufferIndex);
            saver.Seek(1);
            saver.Write(Offset);
            saver.Write(Format, true);
        }
    }
}