using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents the animation of a single <see cref="Bone"/> in a <see cref="SkeletalAnim"/> subfile.
    /// </summary>
    [DebuggerDisplay(nameof(BoneAnim) + " {" + nameof(Name) + "}")]
    public class BoneAnim : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const uint _flagsMaskBase = 0b00000000_00000000_00000000_00111000;
        private const uint _flagsMaskCurve = 0b00000000_00000000_11111111_11000000;
        private const uint _flagsMaskTransform = 0b00001111_10000000_00000000_00000000;

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private uint _flags;

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets a set of flags indicating whether initial transformation values exist in
        /// <see cref="BaseData"/>.
        /// </summary>
        public BoneAnimFlagsBase FlagsBase
        {
            get { return (BoneAnimFlagsBase)(_flags & _flagsMaskBase); }
            set { _flags &= ~_flagsMaskBase | (uint)value; }
        }

        /// <summary>
        /// Gets or sets a set of flags indicating whether curves animating the corresponding transformation exist.
        /// </summary>
        public BoneAnimFlagsCurve FlagsCurve
        {
            get { return (BoneAnimFlagsCurve)(_flags & _flagsMaskCurve); }
            set { _flags &= ~_flagsMaskCurve | (uint)value; }
        }

        /// <summary>
        /// Gets or sets a set of flags controlling how to transform bones.
        /// </summary>
        public BoneAnimFlagsTransform FlagsTransform
        {
            get { return (BoneAnimFlagsTransform)(_flags & _flagsMaskTransform); }
            set { _flags &= ~_flagsMaskTransform | (uint)value; }
        }

        /// <summary>
        /// Gets or sets the name of the animated <see cref="Bone"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a field with unknown purpose.
        /// </summary>
        public byte BeginRotate { get; set; }

        /// <summary>
        /// Gets or sets a field with unknown purpose.
        /// </summary>
        public byte BeginTranslate { get; set; }

        /// <summary>
        /// Gets or sets the element offset in the <see cref="BaseData"/> to an initial translation.
        /// </summary>
        public byte BeginBaseTranslate { get; set; }

        /// <summary>
        /// Gets or sets <see cref="AnimCurve"/> instances animating properties of objects stored in this section.
        /// </summary>
        public IList<AnimCurve> Curves { get; set; }

        /// <summary>
        /// Gets or sets initial transformation values. Only stores specific transformations according to
        /// <see cref="FlagsBase"/>.
        /// </summary>
        public BoneAnimData BaseData { get; set; }

        /// <summary>
        /// Gets the index of the first <see cref="AnimCurve"/> relative to all curves of the parent
        /// <see cref="SkeletalAnim.BoneAnims"/> instances.
        /// </summary>
        internal int BeginCurve { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            _flags = loader.ReadUInt32();
            Name = loader.LoadString();
            BeginRotate = loader.ReadByte();
            BeginTranslate = loader.ReadByte();
            byte numCurve = loader.ReadByte();
            BeginBaseTranslate = loader.ReadByte();
            BeginCurve = loader.ReadInt32();
            Curves = loader.LoadList<AnimCurve>(numCurve);
            BaseData = loader.LoadCustom(() => new BoneAnimData(loader, FlagsBase));
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write(_flags);
            saver.SaveString(Name);
            saver.Write(BeginRotate);
            saver.Write(BeginTranslate);
            saver.Write((byte)Curves.Count);
            saver.Write(BeginBaseTranslate);
            saver.Write(BeginCurve);
            saver.SaveList(Curves);
            saver.SaveCustom(BaseData, () => BaseData.Save(saver, FlagsBase));
        }
    }

    /// <summary>
    /// Represents if initial values exist for the corresponding transformation in the base animation data.
    /// </summary>
    [Flags]
    public enum BoneAnimFlagsBase : uint
    {
        /// <summary>
        /// Initial scaling values exist.
        /// </summary>
        Scale = 1 << 3,

        /// <summary>
        /// Initial rotation values exist.
        /// </summary>
        Rotate = 1 << 4,

        /// <summary>
        /// Initial translation values exist.
        /// </summary>
        Translate = 1 << 5
    }

    /// <summary>
    /// Represents if curves exist which animate the corresponding transformation component.
    /// </summary>
    [Flags]
    public enum BoneAnimFlagsCurve : uint
    {
        /// <summary>
        /// Curve animating the X component of a bone's scale.
        /// </summary>
        ScaleX = 1 << 6,

        /// <summary>
        /// Curve animating the Y component of a bone's scale.
        /// </summary>
        ScaleY = 1 << 7,

        /// <summary>
        /// Curve animating the Z component of a bone's scale.
        /// </summary>
        ScaleZ = 1 << 8,

        /// <summary>
        /// Curve animating the X component of a bone's rotation.
        /// </summary>
        RotateX = 1 << 9,

        /// <summary>
        /// Curve animating the Y component of a bone's rotation.
        /// </summary>
        RotateY = 1 << 10,

        /// <summary>
        /// Curve animating the Z component of a bone's rotation.
        /// </summary>
        RotateZ = 1 << 11,

        /// <summary>
        /// Curve animating the W component of a bone's rotation.
        /// </summary>
        RotateW = 1 << 12,

        /// <summary>
        /// Curve animating the X component of a bone's translation.
        /// </summary>
        TranslateX = 1 << 13,

        /// <summary>
        /// Curve animating the Y component of a bone's translation.
        /// </summary>
        TranslateY = 1 << 14,

        /// <summary>
        /// Curve animating the Z component of a bone's translation.
        /// </summary>
        TranslateZ = 1 << 15
    }

    /// <summary>
    /// Represents how a bone transformation has to be applied.
    /// </summary>
    [Flags]
    public enum BoneAnimFlagsTransform : uint // Same as BoneFlagsTransform
    {
        SegmentScaleCompensate = 1 << 23,
        ScaleUniform = 1 << 24,
        ScaleVolumeOne = 1 << 25,
        RotateZero = 1 << 26,
        TranslateZero = 1 << 27,
        ScaleOne = ScaleVolumeOne | ScaleUniform,
        RotateTranslateZero = RotateZero | TranslateZero,
        Identity = ScaleOne | RotateZero | TranslateZero
    }
}