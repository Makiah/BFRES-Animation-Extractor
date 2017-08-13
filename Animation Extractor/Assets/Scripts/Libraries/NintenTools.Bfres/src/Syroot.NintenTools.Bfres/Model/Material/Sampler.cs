using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;
using Syroot.NintenTools.Bfres.GX2;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a <see cref="Texture"/> sampler in a <see cref="UserData"/> section, storing configuration on how to
    /// draw and interpolate textures.
    /// </summary>
    [DebuggerDisplay(nameof(Sampler) + " {" + nameof(Name) + "}")]
    public class Sampler : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the internal representation of the sampler configuration.
        /// </summary>
        public TexSampler TexSampler { get; set; }

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in <see cref="ResDict{Sampler}"/>
        /// instances.
        /// </summary>
        public string Name { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            TexSampler = new TexSampler(loader.ReadUInt32s(3));
            uint handle = loader.ReadUInt32();
            Name = loader.LoadString();
            byte idx = loader.ReadByte();
            loader.Seek(3);
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write(TexSampler.Values);
            saver.Write(0); // Handle
            saver.SaveString(Name);
            saver.Write((byte)saver.CurrentIndex);
            saver.Seek(3);
        }
    }
}