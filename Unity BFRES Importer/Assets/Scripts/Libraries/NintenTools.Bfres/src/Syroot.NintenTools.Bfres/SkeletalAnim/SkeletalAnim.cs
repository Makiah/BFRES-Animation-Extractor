using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FSKA subfile in a <see cref="ResFile"/>, storing armature animations of <see cref="Bone"/>
    /// instances in a <see cref="Skeleton"/>.
    /// </summary>
    [DebuggerDisplay(nameof(SkeletalAnim) + " {" + nameof(Name) + "}")]
    public class SkeletalAnim : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FSKA";

        private const uint _flagsMaskScale = 0b00000000_00000000_00000011_00000000;
        private const uint _flagsMaskRotate = 0b00000000_00000000_01110000_00000000;

        // ---- FIELDS -------------------------------------------------------------------------------------------------
        
        private uint _flags;
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in
        /// <see cref="ResDict{SkeletalAnim}"/> instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the file which originally supplied the data of this instance.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SkeletalAnimFlagsScale"/> mode used to store scaling values.
        /// </summary>
        public SkeletalAnimFlagsScale FlagsScale
        {
            get { return (SkeletalAnimFlagsScale)(_flags & _flagsMaskScale); }
            set { _flags &= ~_flagsMaskScale | (uint)value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="SkeletalAnimFlagsRotate"/> mode used to store rotation values.
        /// </summary>
        public SkeletalAnimFlagsRotate FlagsRotate
        {
            get { return (SkeletalAnimFlagsRotate)(_flags & _flagsMaskRotate); }
            set { _flags &= ~_flagsMaskRotate | (uint)value; }
        }

        /// <summary>
        /// Gets or sets the total number of frames this animation plays.
        /// </summary>
        public int FrameCount { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes required to bake all <see cref="AnimCurve"/> instances of all
        /// <see cref="BoneAnims"/>.
        /// </summary>
        public uint BakedSize { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="BoneAnim"/> instances creating the animation.
        /// </summary>
        public IList<BoneAnim> BoneAnims { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Skeleton"/> instance affected by this animation.
        /// </summary>
        public Skeleton BindSkeleton { get; set; }

        /// <summary>
        /// Gets or sets the indices of the <see cref="Bone"/> instances in the <see cref="Skeleton.Bones"/> dictionary
        /// to bind for each animation. <see cref="UInt16.MaxValue"/> specifies no binding.
        /// </summary>
        public ushort[] BindIndices { get; set; }

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
            _flags = loader.ReadUInt32();
            FrameCount = loader.ReadInt32();
            ushort numBoneAnim = loader.ReadUInt16();
            ushort numUserData = loader.ReadUInt16();
            int numCurve = loader.ReadInt32();
            BakedSize = loader.ReadUInt32();
            BoneAnims = loader.LoadList<BoneAnim>(numBoneAnim);
            BindSkeleton = loader.Load<Skeleton>();
            BindIndices = loader.LoadCustom(() => loader.ReadUInt16s(numBoneAnim));
            UserData = loader.LoadDict<UserData>();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.SaveString(Name);
            saver.SaveString(Path);
            saver.Write(_flags);
            saver.Write(FrameCount);
            saver.Write((ushort)BoneAnims.Count);
            saver.Write((ushort)UserData.Count);
            saver.Write(BoneAnims.Sum((x) => x.Curves.Count));
            saver.Write(BakedSize);
            saver.SaveList(BoneAnims);
            saver.Save(BindSkeleton);
            saver.SaveCustom(BindIndices, () => saver.Write(BindIndices));
            saver.SaveDict(UserData);
        }
    }
    
    /// <summary>
    /// Represents flags specifying how animation data is stored or should be played.
    /// </summary>
    [Flags]
    public enum SkeletalAnimFlags : uint
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

    /// <summary>
    /// Represents the data format in which scaling values are stored.
    /// </summary>
    public enum SkeletalAnimFlagsScale : uint
    {
        /// <summary>
        /// No scaling.
        /// </summary>
        None,

        /// <summary>
        /// Default scaling.
        /// </summary>
        Standard = 1 << 8,

        /// <summary>
        /// Autodesk Maya scaling.
        /// </summary>
        Maya = 2 << 8,

        /// <summary>
        /// Autodesk Softimage scaling.
        /// </summary>
        Softimage = 3 << 8
    }

    /// <summary>
    /// Represents the data format in which rotation values are stored.
    /// </summary>
    public enum SkeletalAnimFlagsRotate : uint
    {
        /// <summary>
        /// Quaternion, 4 components.
        /// </summary>
        Quaternion,

        /// <summary>
        /// Euler XYZ, 3 components.
        /// </summary>
        EulerXYZ = 1 << 12
    }
}