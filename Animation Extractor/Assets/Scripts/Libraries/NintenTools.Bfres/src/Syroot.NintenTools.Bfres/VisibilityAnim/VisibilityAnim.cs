using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FVIS subfile in a <see cref="ResFile"/>, storing visibility animations of <see cref="Bone"/> or
    /// <see cref="Material"/> instances.
    /// </summary>
    [DebuggerDisplay(nameof(VisibilityAnim) + " {" + nameof(Name) + "}")]
    public class VisibilityAnim : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FVIS";

        private const ushort _flagsMask = 0b00000000_00000111;
        private const ushort _flagsMaskType = 0b00000001_00000000;

        // ---- FIELDS -------------------------------------------------------------------------------------------------
        
        private ushort _flags;
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in
        /// <see cref="ResDict{VisibilityAnim}"/> instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the file which originally supplied the data of this instance.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets flags controlling how animation data is stored or how the animation should be played.
        /// </summary>
        public VisibilityAnimFlags Flags
        {
            get { return (VisibilityAnimFlags)(_flags & _flagsMask); }
            set { _flags &= (ushort)(~_flagsMask | (ushort)value); }
        }

        /// <summary>
        /// Gets or sets the kind of data the animation controls.
        /// </summary>
        public VisibilityAnimType Type
        {
            get { return (VisibilityAnimType)(_flags & _flagsMaskType); }
            set { _flags &= (ushort)(~_flagsMaskType | (ushort)value); }
        }

        /// <summary>
        /// Gets or sets the total number of frames this animation plays.
        /// </summary>
        public int FrameCount { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes required to bake all <see cref="Curves"/>.
        /// </summary>
        public uint BakedSize { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Model"/> instance affected by this animation.
        /// </summary>
        public Model BindModel { get; set; }

        /// <summary>
        /// Gets or sets the indices of entries in the <see cref="Skeleton.Bones"/> or <see cref="Model.Materials"/>
        /// dictionaries to bind to for each animation. <see cref="UInt16.MaxValue"/> specifies no binding.
        /// </summary>
        public ushort[] BindIndices { get; set; }

        /// <summary>
        /// Gets or sets the names of entries in the <see cref="Skeleton.Bones"/> or <see cref="Model.Materials"/>
        /// dictionaries to bind to for each animation.
        /// </summary>
        public IList<string> Names { get; set; }

        /// <summary>
        /// Gets or sets <see cref="AnimCurve"/> instances animating properties of objects stored in this section.
        /// </summary>
        public IList<AnimCurve> Curves { get; set; }

        /// <summary>
        /// Gets or sets boolean values storing the initial visibility for each <see cref="Bone"/> or
        /// <see cref="Material"/>.
        /// </summary>
        public bool[] BaseDataList { get; set; }

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
            _flags = loader.ReadUInt16();
            ushort numUserData = loader.ReadUInt16();
            FrameCount = loader.ReadInt32();
            ushort numAnim = loader.ReadUInt16();
            ushort numCurve = loader.ReadUInt16();
            BakedSize = loader.ReadUInt32();
            BindModel = loader.Load<Model>();
            BindIndices = loader.LoadCustom(() => loader.ReadUInt16s(numAnim));
            Names = loader.LoadCustom(() => loader.LoadStrings(numAnim)); // Offset to name list.
            Curves = loader.LoadList<AnimCurve>(numCurve);
            BaseDataList = loader.LoadCustom(() =>
            {
                bool[] baseData = new bool[numAnim];
                int i = 0;
                while (i < numAnim)
                {
                    byte b = loader.ReadByte();
                    for (int j = 0; j < 8 && i < numAnim; j++)
                    {
                        baseData[i++] = b.GetBit(j);
                    }
                }
                return baseData;
            });
            UserData = loader.LoadDict<UserData>();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.SaveString(Name);
            saver.SaveString(Path);
            saver.Write(_flags);
            saver.Write((ushort)UserData.Count);
            saver.Write(FrameCount);
            saver.Write((ushort)Names.Count);
            saver.Write((ushort)Curves.Count);
            saver.Write(BakedSize);
            saver.Save(BindModel);
            saver.SaveCustom(BindIndices, () => saver.Write(BindIndices));
            saver.SaveStrings(Names);
            saver.SaveList(Curves);
            saver.SaveCustom(BaseDataList, () =>
            {
                int i = 0;
                while (i < BaseDataList.Length)
                {
                    byte b = 0;
                    for (int j = 0; j < 8 && i < BaseDataList.Length; j++)
                    {
                        b.SetBit(j, BaseDataList[i++]);
                    }
                    saver.Write(b);
                }
            });
            saver.SaveDict(UserData);
        }
    }
    
    /// <summary>
    /// Represents flags specifying how animation data is stored or should be played.
    /// </summary>
    [Flags]
    public enum VisibilityAnimFlags : ushort
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
    /// Represents the kind of data the visibility animation controls.
    /// </summary>
    public enum VisibilityAnimType : ushort
    {
        /// <summary>
        /// Bone visiblity is controlled.
        /// </summary>
        Bone,

        /// <summary>
        /// Material visibility is controlled.
        /// </summary>
        Material = 1 << 8
    }
}