using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a vertex shape animation in a <see cref="ShapeAnim"/> subfile.
    /// </summary>
    [DebuggerDisplay(nameof(VertexShapeAnim) + " {" + nameof(Name) + "}")]
    public class VertexShapeAnim : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the name of the animated <see cref="Shape"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="KeyShapeAnimInfo"/> instances.
        /// </summary>
        public IList<KeyShapeAnimInfo> KeyShapeAnimInfos { get; set; }

        /// <summary>
        /// Gets or sets <see cref="AnimCurve"/> instances animating properties of objects stored in this section.
        /// </summary>
        public IList<AnimCurve> Curves { get; set; }

        /// <summary>
        /// Gets or sets the list of base values, excluding the base shape (which is always being initialized with 0f).
        /// </summary>
        public float[] BaseDataList { get; set; }

        /// <summary>
        /// Gets or sets the index of the first <see cref="AnimCurve"/> relative to all curves of the parent
        /// <see cref="ShapeAnim.VertexShapeAnims"/> instances.
        /// </summary>
        internal int BeginCurve { get; set; }

        /// <summary>
        /// Gets or sets the index of the first <see cref="KeyShapeAnimInfo"/> relative to all key shape anim infos of
        /// the parent <see cref="ShapeAnim.VertexShapeAnims"/> instances.
        /// </summary>
        internal int BeginKeyShapeAnim { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            ushort numCurve = loader.ReadUInt16();
            ushort numKeyShapeAnim = loader.ReadUInt16();
            BeginCurve = loader.ReadInt32();
            BeginKeyShapeAnim = loader.ReadInt32();
            Name = loader.LoadString();
            KeyShapeAnimInfos = loader.LoadList<KeyShapeAnimInfo>(numKeyShapeAnim);
            Curves = loader.LoadList<AnimCurve>(numCurve);
            BaseDataList = loader.LoadCustom(() => loader.ReadSingles(numKeyShapeAnim - 1)); // Without base shape.
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write((ushort)Curves.Count);
            saver.Write((ushort)KeyShapeAnimInfos.Count);
            saver.Write(BeginCurve);
            saver.Write(BeginKeyShapeAnim);
            saver.SaveString(Name);
            saver.SaveList(KeyShapeAnimInfos);
            saver.SaveList(Curves);
            saver.SaveCustom(BaseDataList, () => saver.Write(BaseDataList));
        }
    }
}