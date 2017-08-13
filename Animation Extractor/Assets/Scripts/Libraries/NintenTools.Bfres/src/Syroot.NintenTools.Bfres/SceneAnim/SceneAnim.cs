using System;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FSCN subfile in a <see cref="ResFile"/>, storing scene animations controlling camera, light and
    /// fog settings.
    /// </summary>
    [DebuggerDisplay(nameof(SceneAnim) + " {" + nameof(Name) + "}")]
    public class SceneAnim : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FSCN";
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in <see cref="ResDict{SceneAnim}"/>
        /// instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path of the file which originally supplied the data of this instance.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CameraAnim"/> instances.
        /// </summary>
        public ResDict<CameraAnim> CameraAnims { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LightAnim"/> instances.
        /// </summary>
        public ResDict<LightAnim> LightAnims { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="FogAnim"/> instances.
        /// </summary>
        public ResDict<FogAnim> FogAnims { get; set; }

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
            ushort numUserData = loader.ReadUInt16();
            ushort numCameraAnim = loader.ReadUInt16();
            ushort numLightAnim = loader.ReadUInt16();
            ushort numFogAnim = loader.ReadUInt16();
            CameraAnims = loader.LoadDict<CameraAnim>();
            LightAnims = loader.LoadDict<LightAnim>();
            FogAnims = loader.LoadDict<FogAnim>();
            UserData = loader.LoadDict<UserData>();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.SaveString(Name);
            saver.SaveString(Path);
            saver.Write((ushort)UserData.Count);
            saver.Write((ushort)CameraAnims.Count);
            saver.Write((ushort)LightAnims.Count);
            saver.Write((ushort)FogAnims.Count);
            saver.SaveDict(CameraAnims);
            saver.SaveDict(LightAnims);
            saver.SaveDict(FogAnims);
            saver.SaveDict(UserData);
        }
    }
}