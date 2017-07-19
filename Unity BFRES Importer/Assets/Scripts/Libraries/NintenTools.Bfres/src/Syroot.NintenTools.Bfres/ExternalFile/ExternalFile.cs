using System.IO;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a file attachment to a <see cref="ResFile"/> which can be of arbitrary data.
    /// </summary>
    public class ExternalFile : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the raw data stored by the external file.
        /// </summary>
        public byte[] Data { get; set; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Opens and returns a <see cref="MemoryStream"/> on the raw <see cref="Data"/> byte array, which optionally
        /// can be written to.
        /// </summary>
        /// <param name="writable"><c>true</c> to allow write access to the raw data.</param>
        /// <returns>The opened <see cref="MemoryStream"/> instance.</returns>
        public MemoryStream GetStream(bool writable = false)
        {
            return new MemoryStream(Data, writable);
        }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            uint ofsData = loader.ReadOffset();
            uint sizData = loader.ReadUInt32();
            Data = loader.LoadCustom(() => loader.ReadBytes((int)sizData), ofsData);
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.SaveBlock(Data, saver.ResFile.Alignment, () => saver.Write(Data));
            saver.Write(Data.Length);
        }
    }
}