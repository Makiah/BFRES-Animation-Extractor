using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents a node in a <see cref="SubMesh"/> bounding tree to determine when to show which sub mesh of a
    /// <see cref="Mesh"/>.
    /// </summary>
    public class BoundingNode : IResData
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        public ushort LeftChildIndex { get; set; }

        public ushort RightChildIndex { get; set; }

        public ushort Unknown { get; set; }

        public ushort NextSibling { get; set; }
        
        public ushort SubMeshIndex { get; set; }

        public ushort SubMeshCount { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            LeftChildIndex = loader.ReadUInt16();
            RightChildIndex = loader.ReadUInt16();
            Unknown = loader.ReadUInt16();
            NextSibling = loader.ReadUInt16();
            SubMeshIndex = loader.ReadUInt16();
            SubMeshCount = loader.ReadUInt16();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write(LeftChildIndex);
            saver.Write(RightChildIndex);
            saver.Write(Unknown);
            saver.Write(NextSibling);
            saver.Write(SubMeshIndex);
            saver.Write(SubMeshCount);
        }
    }
}