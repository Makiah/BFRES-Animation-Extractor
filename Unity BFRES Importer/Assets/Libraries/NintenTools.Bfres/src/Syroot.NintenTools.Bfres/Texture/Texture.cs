using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;
using Syroot.NintenTools.Bfres.GX2;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FMDL subfile in a <see cref="ResFile"/>, storing multi-dimensional texture data.
    /// </summary>
    [DebuggerDisplay(nameof(Texture) + " {" + nameof(Name) + "}")]
    public class Texture : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FTEX";
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the shape of the texture.
        /// </summary>
        public GX2SurfaceDim Dim { get; set; }

        /// <summary>
        /// Gets or sets the width of the texture.
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the texture.
        /// </summary>
        public uint Height { get; set; }

        /// <summary>
        /// Gets or sets the depth of the texture.
        /// </summary>
        public uint Depth { get; set; }

        /// <summary>
        /// Gets or sets the number of mipmaps stored in the <see cref="MipData"/>.
        /// </summary>
        public uint MipCount { get; set; }

        /// <summary>
        /// Gets or sets the desired texture data buffer format.
        /// </summary>
        public GX2SurfaceFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the number of samples for the texture.
        /// </summary>
        public GX2AAMode AAMode { get; set; }

        /// <summary>
        /// Gets or sets the texture data usage hint.
        /// </summary>
        public GX2SurfaceUse Use { get; set; }

        /// <summary>
        /// Gets or sets the tiling mode.
        /// </summary>
        public GX2TileMode TileMode { get; set; }

        /// <summary>
        /// Gets or sets the swizzling value.
        /// </summary>
        public uint Swizzle { get; set; }

        /// <summary>
        /// Gets or sets the swizzling alignment.
        /// </summary>
        public uint Alignment { get; set; }

        /// <summary>
        /// Gets or sets the pixel swizzling stride.
        /// </summary>
        public uint Pitch { get; set; }

        /// <summary>
        /// Gets or sets the offsets in the <see cref="MipData"/> array to the data of the mipmap level corresponding
        /// to the array index.
        /// </summary>
        public uint[] MipOffsets { get; set; }

        public uint ViewMipFirst { get; set; }

        public uint ViewMipCount { get; set; }

        public uint ViewSliceFirst { get; set; }

        public uint ViewSliceCount { get; set; }

        /// <summary>
        /// Gets or sets the source channel to map to the R (red) channel.
        /// </summary>
        public GX2CompSel CompSelR { get; set; }

        /// <summary>
        /// Gets or sets the source channel to map to the G (green) channel.
        /// </summary>
        public GX2CompSel CompSelG { get; set; }

        /// <summary>
        /// Gets or sets the source channel to map to the B (blue) channel.
        /// </summary>
        public GX2CompSel CompSelB { get; set; }

        /// <summary>
        /// Gets or sets the source channel to map to the A (alpha) channel.
        /// </summary>
        public GX2CompSel CompSelA { get; set; }

        public uint[] Regs { get; set; }

        public uint ArrayLength { get; set; }

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in <see cref="ResDict{Texture}"/>
        /// instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the file which originally supplied the data of this instance.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the raw texture data bytes.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the raw mipmap level data bytes for all levels.
        /// </summary>
        public byte[] MipData { get; set; }

        /// <summary>
        /// Gets or sets customly attached <see cref="UserData"/> instances.
        /// </summary>
        public ResDict<UserData> UserData { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            loader.CheckSignature(_signature);
            Dim = loader.ReadEnum<GX2SurfaceDim>(true);
            Width = loader.ReadUInt32();
            Height = loader.ReadUInt32();
            Depth = loader.ReadUInt32();
            MipCount = loader.ReadUInt32();
            Format = loader.ReadEnum<GX2SurfaceFormat>(true);
            AAMode = loader.ReadEnum<GX2AAMode>(true);
            Use = loader.ReadEnum<GX2SurfaceUse>(true);
            uint sizData = loader.ReadUInt32();
            uint imagePointer = loader.ReadUInt32();
            uint sizMipData = loader.ReadUInt32();
            uint mipPointer = loader.ReadUInt32();
            TileMode = loader.ReadEnum<GX2TileMode>(true);
            Swizzle = loader.ReadUInt32();
            Alignment = loader.ReadUInt32();
            Pitch = loader.ReadUInt32();
            MipOffsets = loader.ReadUInt32s(13);
            ViewMipFirst = loader.ReadUInt32();
            ViewMipCount = loader.ReadUInt32();
            ViewSliceFirst = loader.ReadUInt32();
            ViewSliceCount = loader.ReadUInt32();
            CompSelR = loader.ReadEnum<GX2CompSel>(true);
            CompSelG = loader.ReadEnum<GX2CompSel>(true);
            CompSelB = loader.ReadEnum<GX2CompSel>(true);
            CompSelA = loader.ReadEnum<GX2CompSel>(true);
            Regs = loader.ReadUInt32s(5);
            uint handle = loader.ReadUInt32();
            ArrayLength = loader.ReadUInt32(); // Possibly just a byte.
            Name = loader.LoadString();
            Path = loader.LoadString();
            Data = loader.LoadCustom(() => loader.ReadBytes((int)sizData));
            MipData = loader.LoadCustom(() => loader.ReadBytes((int)sizMipData));
            UserData = loader.LoadDict<UserData>();
            ushort numUserData = loader.ReadUInt16();
            loader.Seek(2);
        }

        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.Write(Dim, true);
            saver.Write(Width);
            saver.Write(Height);
            saver.Write(Depth);
            saver.Write(MipCount);
            saver.Write(Format, true);
            saver.Write(AAMode, true);
            saver.Write(Use, true);
            saver.Write(Data.Length);
            saver.Write(0); // ImagePointer
            saver.Write(MipData == null ? 0 : MipData.Length);
            saver.Write(0); // MipPointer
            saver.Write(TileMode, true);
            saver.Write(Swizzle);
            saver.Write(Alignment);
            saver.Write(Pitch);
            saver.Write(MipOffsets);
            saver.Write(ViewMipFirst);
            saver.Write(ViewMipCount);
            saver.Write(ViewSliceFirst);
            saver.Write(ViewSliceCount);
            saver.Write(CompSelR, true);
            saver.Write(CompSelG, true);
            saver.Write(CompSelB, true);
            saver.Write(CompSelA, true);
            saver.Write(Regs);
            saver.Write(0); // Handle
            saver.Write(ArrayLength);
            saver.SaveString(Name);
            saver.SaveString(Path);
            saver.SaveBlock(Data, saver.ResFile.Alignment, () => saver.Write(Data));
            saver.SaveBlock(MipData, saver.ResFile.Alignment, () => saver.Write(MipData));
            saver.SaveDict(UserData);
            saver.Write((ushort)UserData.Count);
            saver.Seek(2);
        }
    }
}