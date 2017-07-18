using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a pattern animation info in a <see cref="TexPatternMatAnim"/> instance.
    /// </summary>
    [DebuggerDisplay(nameof(PatternAnimInfo) + " {" + nameof(Name) + "}")]
    public class PatternAnimInfo : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the index of the curve in the <see cref="TexPatternMatAnim"/>.
        /// </summary>
        public sbyte CurveIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the texture in the <see cref="Material"/>.
        /// </summary>
        public sbyte SubBindIndex { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="Sampler"/> in the <see cref="Material"/>.
        /// </summary>
        public string Name { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            CurveIndex = loader.ReadSByte();
            SubBindIndex = loader.ReadSByte();
            loader.Seek(2);
            Name = loader.LoadString();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write(CurveIndex);
            saver.Write(SubBindIndex);
            saver.Seek(2);
            saver.SaveString(Name);
        }
    }
}