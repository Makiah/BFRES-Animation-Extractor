using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FMAT subsection of a <see cref="Model"/> subfile, storing information on with which textures and
    /// how technically a surface is drawn.
    /// </summary>
    [DebuggerDisplay(nameof(Material) + " {" + nameof(Name) + "}")]
    public class Material : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FMAT";
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in <see cref="ResDict{Material}"/>
        /// instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets flags specifying how a <see cref="Material"/> is rendered.
        /// </summary>
        public MaterialFlags Flags { get; set; }

        public ResDict<RenderInfo> RenderInfos { get; set; }

        public RenderState RenderState { get; set; }

        public ShaderAssign ShaderAssign { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="TextureRef"/> instances referencing the <see cref="Texture"/> instances
        /// required to draw the material.
        /// </summary>
        public IList<TextureRef> TextureRefs { get; set; }

        /// <summary>
        /// Gets or sets a dictionary of <see cref="Sampler"/> instances which configure how to draw
        /// <see cref="Texture"/> instances referenced by the <see cref="TextureRefs"/> list.
        /// </summary>
        public ResDict<Sampler> Samplers { get; set; }

        public ResDict<ShaderParam> ShaderParams { get; set; }

        /// <summary>
        /// Gets or sets the raw data block which stores <see cref="ShaderParam"/> values.
        /// </summary>
        public byte[] ShaderParamData { get; set; }

        /// <summary>
        /// Gets or sets customly attached <see cref="UserData"/> instances.
        /// </summary>
        public ResDict<UserData> UserData { get; set; }

        /// <summary>
        /// Gets or sets a set of bits determining whether <see cref="ShaderParam"/> instances are volatile.
        /// </summary>
        // TODO: Wrap into a bool array.
        public byte[] VolatileFlags { get; set; }

        // TODO: Methods to access ShaderParam variable values.

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            loader.CheckSignature(_signature);
            Name = loader.LoadString();
            Flags = loader.ReadEnum<MaterialFlags>(true);
            ushort idx = loader.ReadUInt16();
            ushort numRenderInfo = loader.ReadUInt16();
            byte numSampler = loader.ReadByte();
            byte numTextureRef = loader.ReadByte();
            ushort numShaderParam = loader.ReadUInt16();
            ushort numShaderParamVolatile = loader.ReadUInt16();
            ushort sizParamSource = loader.ReadUInt16();
            ushort sizParamRaw = loader.ReadUInt16();
            ushort numUserData = loader.ReadUInt16();
            RenderInfos = loader.LoadDict<RenderInfo>();
            RenderState = loader.Load<RenderState>();
            ShaderAssign = loader.Load<ShaderAssign>();
            TextureRefs = loader.LoadList<TextureRef>(numTextureRef);
            uint ofsSamplerList = loader.ReadOffset(); // Only use dict.
            Samplers = loader.LoadDict<Sampler>();
            uint ofsShaderParamList = loader.ReadOffset(); // Only use dict.
            ShaderParams = loader.LoadDict<ShaderParam>();
            ShaderParamData = loader.LoadCustom(() => loader.ReadBytes(sizParamSource));
            UserData = loader.LoadDict<UserData>();
            VolatileFlags = loader.LoadCustom(() => loader.ReadBytes((int)Math.Ceiling(numShaderParam / 8f)));
            uint userPointer = loader.ReadUInt32();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.SaveString(Name);
            saver.Write(Flags, true);
            saver.Write((ushort)saver.CurrentIndex);
            saver.Write((ushort)RenderInfos.Count);
            saver.Write((byte)Samplers.Count);
            saver.Write((byte)TextureRefs.Count);
            saver.Write((ushort)ShaderParams.Count);
            saver.Write((ushort)VolatileFlags.Length);
            saver.Write((ushort)ShaderParamData.Length);
            saver.Write((ushort)0); // SizParamRaw
            saver.Write((ushort)UserData.Count);
            saver.SaveDict(RenderInfos);
            saver.Save(RenderState);
            saver.Save(ShaderAssign);
            saver.SaveList(TextureRefs);
            saver.SaveList(Samplers.Values);
            saver.SaveDict(Samplers);
            saver.SaveList(ShaderParams.Values);
            saver.SaveDict(ShaderParams);
            saver.SaveCustom(ShaderParamData, () => saver.Write(ShaderParamData));
            saver.SaveDict(UserData);
            saver.SaveCustom(VolatileFlags, () => saver.Write(VolatileFlags));
            saver.Write(0); // UserPointer
        }
    }

    /// <summary>
    /// Represents general flags specifying how a <see cref="Material"/> is rendered.
    /// </summary>
    public enum MaterialFlags : uint
    {
        /// <summary>
        /// The material is not rendered at all.
        /// </summary>
        None,

        /// <summary>
        /// The material is rendered.
        /// </summary>
        Visible
    }
}