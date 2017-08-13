using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a parameter value in a <see cref="UserData"/> section, passing data to shader variables.
    /// </summary>
    [DebuggerDisplay(nameof(ShaderParam) + " {" + nameof(Name) + "}")]
    public class ShaderParam : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the type of the value.
        /// </summary>
        public ShaderParamType Type { get; set; }

        /// <summary>
        /// Gets the offset in the <see cref="Material.ShaderParamData"/> byte array in bytes.
        /// </summary>
        public ushort DataOffset { get; set; }

        public ushort DependedIndex { get; set; }

        public ushort DependIndex { get; set; }

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in
        /// <see cref="ResDict{ShaderParam}"/> instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the size of the value in bytes.
        /// </summary>
        public uint DataSize
        {
            get
            {
                if (Type <= ShaderParamType.Float4)
                {
                    return sizeof(float) * (((uint)Type & 0x03) + 1);
                }
                if (Type <= ShaderParamType.Float4x4)
                {
                    uint cols = ((uint)Type & 0x03) + 1;
                    uint rows = (((uint)Type - (uint)ShaderParamType.Reserved2) >> 2) + 2;
                    return sizeof(float) * cols * rows;
                }
                switch (Type)
                {
                    case ShaderParamType.Srt2D: return Srt2D.SizeInBytes;
                    case ShaderParamType.Srt3D: return Srt3D.SizeInBytes;
                    case ShaderParamType.TexSrt: return TexSrt.SizeInBytes;
                    case ShaderParamType.TexSrtEx: return TexSrtEx.SizeInBytes;
                }
                throw new ResException($"Cannot retrieve size of unknown {nameof(ShaderParamType)} {Type}.");
            }
        }

        // TODO: Methods to retrieve the strongly-typed shader param value.

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            Type = loader.ReadEnum<ShaderParamType>(true);
            if (loader.ResFile.Version >= 0x03030000)
            {
                byte sizData = loader.ReadByte();
                DataOffset = loader.ReadUInt16();
                int offset = loader.ReadInt32(); // Uniform variable offset.
                uint callbackPointer = loader.ReadUInt32();
                DependedIndex = loader.ReadUInt16();
                DependIndex = loader.ReadUInt16();
                Name = loader.LoadString();
            }
            else
            {
                // GUESS
                loader.Seek(1);
                DataOffset = loader.ReadUInt16();
                int offset = loader.ReadInt32(); // Uniform variable offset.
                Name = loader.LoadString();
            }
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write(Type, true);
            if (saver.ResFile.Version >= 0x03030000)
            {
                saver.Write((byte)DataSize);
                saver.Write(DataOffset);
                saver.Write(-1); // Offset
                saver.Write(0); // CallbackPointer
                saver.Write(DependedIndex);
                saver.Write(DependIndex);
                saver.SaveString(Name);
            }
            else
            {
                saver.Seek(1);
                saver.Write(DataOffset);
                saver.Write(-1); // Offset
                saver.SaveString(Name);
            }
        }
    }

    /// <summary>
    /// Represents the data types in which <see cref="ShaderParam"/> instances can store their value.
    /// </summary>
    public enum ShaderParamType : byte
    {
        /// <summary>
        /// The value is a single <see cref="System.Boolean"/>.
        /// </summary>
        Bool,

        /// <summary>
        /// The value is a <see cref="Maths.Vector2Bool"/>.
        /// </summary>
        Bool2,

        /// <summary>
        /// The value is a <see cref="Maths.Vector3Bool"/>.
        /// </summary>
        Bool3,

        /// <summary>
        /// The value is a <see cref="Maths.Vector4Bool"/>.
        /// </summary>
        Bool4,

        /// <summary>
        /// The value is a single <see cref="System.Int32"/>.
        /// </summary>
        Int,

        /// <summary>
        /// The value is a <see cref="Maths.Vector2"/>.
        /// </summary>
        Int2,

        /// <summary>
        /// The value is a <see cref="Maths.Vector3"/>.
        /// </summary>
        Int3,

        /// <summary>
        /// The value is a <see cref="Maths.Vector4"/>.
        /// </summary>
        Int4,

        /// <summary>
        /// The value is a single <see cref="System.UInt32"/>.
        /// </summary>
        UInt,

        /// <summary>
        /// The value is a <see cref="Maths.Vector2U"/>.
        /// </summary>
        UInt2,

        /// <summary>
        /// The value is a <see cref="Maths.Vector3U"/>.
        /// </summary>
        UInt3,

        /// <summary>
        /// The value is a <see cref="Maths.Vector4U"/>.
        /// </summary>
        UInt4,

        /// <summary>
        /// The value is a single <see cref="System.Single"/>.
        /// </summary>
        Float,

        /// <summary>
        /// The value is a <see cref="Maths.Vector2F"/>.
        /// </summary>
        Float2,

        /// <summary>
        /// The value is a <see cref="Maths.Vector3F"/>.
        /// </summary>
        Float3,

        /// <summary>
        /// The value is a <see cref="Maths.Vector4F"/>.
        /// </summary>
        Float4,

        /// <summary>
        /// An invalid type for <see cref="ShaderParam"/> values, only used for internal computations.
        /// </summary>
        Reserved2,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix2"/>.
        /// </summary>
        Float2x2,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix2x3"/>.
        /// </summary>
        Float2x3,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix2x4"/>.
        /// </summary>
        Float2x4,

        /// <summary>
        /// An invalid type for <see cref="ShaderParam"/> values, only used for internal computations.
        /// </summary>
        Reserved3,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix3x2"/>.
        /// </summary>
        Float3x2,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix3"/>.
        /// </summary>
        Float3x3,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix3x4"/>.
        /// </summary>
        Float3x4,

        /// <summary>
        /// An invalid type for <see cref="ShaderParam"/> values, only used for internal computations.
        /// </summary>
        Reserved4,

        /// <summary>
        /// The value is a <see cref="System.Single"/>.
        /// </summary>
        Float4x2,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix4x3"/>.
        /// </summary>
        Float4x3,

        /// <summary>
        /// The value is a <see cref="Maths.Matrix4"/>.
        /// </summary>
        Float4x4,

        /// <summary>
        /// The value is a <see cref="Srt2D"/>.
        /// </summary>
        Srt2D,

        /// <summary>
        /// The value is a <see cref="Srt3D"/>.
        /// </summary>
        Srt3D,

        /// <summary>
        /// The value is a <see cref="TexSrt"/>.
        /// </summary>
        TexSrt,

        /// <summary>
        /// The value is a <see cref="TexSrtEx"/>.
        /// </summary>
        TexSrtEx
    }
}