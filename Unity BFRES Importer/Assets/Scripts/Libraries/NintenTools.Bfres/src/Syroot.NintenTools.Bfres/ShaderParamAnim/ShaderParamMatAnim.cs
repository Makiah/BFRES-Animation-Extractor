using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a material parameter animation in a <see cref="ShaderParamAnim"/> subfile.
    /// </summary>
    [DebuggerDisplay(nameof(ShaderParamMatAnim) + " {" + nameof(Name) + "}")]
    public class ShaderParamMatAnim : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name of the animated <see cref="Material"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="ParamAnimInfo"/> instances.
        /// </summary>
        public IList<ParamAnimInfo> ParamAnimInfos { get; set; }

        /// <summary>
        /// Gets or sets <see cref="AnimCurve"/> instances animating properties of objects stored in this section.
        /// </summary>
        public IList<AnimCurve> Curves { get; set; }

        public IList<AnimConstant> Constants { get; set; }

        /// <summary>
        /// Gets or sets the index of the first <see cref="AnimCurve"/> relative to all curves of the parent
        /// <see cref="ShaderParamAnim.ShaderParamMatAnims"/> instances.
        /// </summary>
        internal int BeginCurve { get; set; }

        /// <summary>
        /// Gets or sets the index of the first <see cref="ParamAnimInfo"/> relative to all param anim infos of the
        /// parent <see cref="ShaderParamAnim.ShaderParamMatAnims"/> instances.
        /// </summary>
        internal int BeginParamAnim { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            ushort numAnimParam = loader.ReadUInt16();
            ushort numCurve = loader.ReadUInt16();
            ushort numConstant = loader.ReadUInt16();
            loader.Seek(2);
            BeginCurve = loader.ReadInt32();
            BeginParamAnim = loader.ReadInt32();
            Name = loader.LoadString();
            ParamAnimInfos = loader.LoadList<ParamAnimInfo>(numAnimParam);
            Curves = loader.LoadList<AnimCurve>(numCurve);
            Constants = loader.LoadCustom(() => loader.ReadAnimConstants(numConstant));
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write((ushort)ParamAnimInfos.Count);
            saver.Write((ushort)Curves.Count);
            saver.Write((ushort)Constants.Count);
            saver.Seek(2);
            saver.Write(BeginCurve);
            saver.Write(BeginParamAnim);
            saver.SaveString(Name);
            saver.SaveList(ParamAnimInfos);
            saver.SaveList(Curves);
            saver.SaveCustom(Constants, () => saver.Write(Constants));
        }
    }
}