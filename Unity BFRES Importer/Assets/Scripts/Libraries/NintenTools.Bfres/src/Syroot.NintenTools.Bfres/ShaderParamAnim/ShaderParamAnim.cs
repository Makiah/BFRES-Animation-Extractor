using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FSHU subfile in a <see cref="ResFile"/>, storing shader parameter animations of a
    /// <see cref="Model"/> instance.
    /// </summary>
    [DebuggerDisplay(nameof(ShaderParamAnim) + " {" + nameof(Name) + "}")]
    public class ShaderParamAnim : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FSHU";
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in
        /// <see cref="ResDict{ShaderParamAnim}"/> instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the file which originally supplied the data of this instance.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets flags controlling how animation data is stored or how the animation should be played.
        /// </summary>
        public ShaderParamAnimFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the total number of frames this animation plays.
        /// </summary>
        public int FrameCount { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes required to bake all <see cref="AnimCurve"/> instances of all
        /// <see cref="ShaderParamMatAnims"/>.
        /// </summary>
        public uint BakedSize { get; set; }
        
        /// <summary>
        /// Gets or sets the <see cref="Model"/> instance affected by this animation.
        /// </summary>
        public Model BindModel { get; set; }

        /// <summary>
        /// Gets the indices of the <see cref="Material"/> instances in the <see cref="Model.Materials"/> dictionary to
        /// bind for each animation. <see cref="UInt16.MaxValue"/> specifies no binding.
        /// </summary>
        public ushort[] BindIndices { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShaderParamMatAnim"/> instances creating the animation.
        /// </summary>
        public IList<ShaderParamMatAnim> ShaderParamMatAnims { get; set; }

        /// <summary>
        /// Gets or sets customly attached <see cref="UserData"/> instances.
        /// </summary>
        public ResDict<UserData> UserData { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            loader.CheckSignature(_signature);
            Name = loader.LoadString();
            Path = loader.LoadString();
            Flags = loader.ReadEnum<ShaderParamAnimFlags>(true);
            FrameCount = loader.ReadInt32();
            ushort numMatAnim = loader.ReadUInt16();
            ushort numUserData = loader.ReadUInt16();
            int numParamAnim = loader.ReadInt32();
            int numCurve = loader.ReadInt32();
            BakedSize = loader.ReadUInt32();
            BindModel = loader.Load<Model>();
            BindIndices = loader.LoadCustom(() => loader.ReadUInt16s(numMatAnim));
            ShaderParamMatAnims = loader.LoadList<ShaderParamMatAnim>(numMatAnim);
            UserData = loader.LoadDict<UserData>();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.SaveString(Name);
            saver.SaveString(Path);
            saver.Write(Flags, true);
            saver.Write(FrameCount);
            saver.Write((ushort)ShaderParamMatAnims.Count);
            saver.Write((ushort)UserData.Count);
            saver.Write(ShaderParamMatAnims.Sum((x) => x.ParamAnimInfos.Count));
            saver.Write(ShaderParamMatAnims.Sum((x) => x.Curves.Count));
            saver.Write(BakedSize);
            saver.Save(BindModel);
            saver.SaveCustom(BindIndices, () => saver.Write(BindIndices));
            saver.SaveList(ShaderParamMatAnims);
            saver.SaveDict(UserData);
        }
    }
    
    /// <summary>
    /// Represents flags specifying how animation data is stored or should be played.
    /// </summary>
    [Flags]
    public enum ShaderParamAnimFlags : uint
    {
        /// <summary>
        /// The stored curve data has been baked.
        /// </summary>
        BakedCurve = 1 << 0,

        /// <summary>
        /// The animation repeats from the start after the last frame has been played.
        /// </summary>
        Looping = 1 << 2
    }
}