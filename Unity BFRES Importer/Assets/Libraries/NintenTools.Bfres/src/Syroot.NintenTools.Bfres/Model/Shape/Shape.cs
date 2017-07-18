using System;
using System.Collections.Generic;
using System.Diagnostics;
using Syroot.NintenTools.Bfres.Core;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents an FSHP section in a <see cref="Model"/> subfile.
    /// </summary>
    [DebuggerDisplay(nameof(Shape) + " {" + nameof(Name) + "}")]
    public class Shape : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const string _signature = "FSHP";
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the name with which the instance can be referenced uniquely in <see cref="ResDict{Shape}"/>
        /// instances.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets flags determining which data is available for this instance.
        /// </summary>
        public ShapeFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the index of the material to apply to the shapes surface in the owning
        /// <see cref="Model.Materials"/> list.
        /// </summary>
        public ushort MaterialIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the <see cref="Bone"/> to which this instance is directly attached to. The bone
        /// must be part of the skeleton referenced by the owning <see cref="Model.Skeleton"/> instance.
        /// </summary>
        public ushort BoneIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the <see cref="VertexBuffer"/> in the owning <see cref="Model.VertexBuffers"/>
        /// list.
        /// </summary>
        public ushort VertexBufferIndex { get; set; }

        /// <summary>
        /// Gets or sets the bounding radius spanning the shape.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Gets or sets the number of bones influencing the vertices stored in this buffer. 0 influences equal
        /// rigidbodies (no skinning), 1 equal rigid skinning and 2 or more smooth skinning.
        /// </summary>
        public byte VertexSkinCount { get; set; }

        /// <summary>
        /// Gets or sets a value with unknown purpose.
        /// </summary>
        public byte TargetAttribCount { get; set; }

        /// <summary>
        /// Gets or sets the list of <see cref="Meshes"/> which are used to represent different level of details of the
        /// shape.
        /// </summary>
        public IList<Mesh> Meshes { get; set; }
        
        public IList<ushort> SkinBoneIndices { get; set; }
        
        public ResDict<KeyShape> KeyShapes { get; set; }
        
        public IList<Bounding> SubMeshBoundings { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="BoundingNode"/> instances forming the bounding tree with which parts of a mesh
        /// are culled when not visible.
        /// </summary>
        public IList<BoundingNode> SubMeshBoundingNodes { get; set; }

        public IList<ushort> SubMeshBoundingIndices { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="VertexBuffer"/> instance storing the data which forms the shape's surface. Saved
        /// depending on <see cref="VertexBufferIndex"/>.
        /// </summary>
        internal VertexBuffer VertexBuffer { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            loader.CheckSignature(_signature);
            Name = loader.LoadString();
            Flags = loader.ReadEnum<ShapeFlags>(true);
            ushort idx = loader.ReadUInt16();
            MaterialIndex = loader.ReadUInt16();
            BoneIndex = loader.ReadUInt16();
            VertexBufferIndex = loader.ReadUInt16();
            ushort numSkinBoneIndex = loader.ReadUInt16();
            VertexSkinCount = loader.ReadByte();
            byte numMesh = loader.ReadByte();
            byte numKeyShape = loader.ReadByte();
            TargetAttribCount = loader.ReadByte();
            ushort numSubMeshBoundingNodes = loader.ReadUInt16(); // Padding in engine.
            Radius = loader.ReadSingle();
            VertexBuffer = loader.Load<VertexBuffer>();
            Meshes = loader.LoadList<Mesh>(numMesh);
            SkinBoneIndices = loader.LoadCustom(() => loader.ReadUInt16s(numSkinBoneIndex));
            KeyShapes = loader.LoadDict<KeyShape>();
            // TODO: At least BotW has more data following the Boundings, or that are no boundings at all.
            if (numSubMeshBoundingNodes == 0)
            {
                // Compute the count differently if the node count was padding.
                SubMeshBoundings = loader.LoadCustom(() => loader.ReadBoundings(Meshes[0].SubMeshes.Count + 1)); 
            }
            else
            {
                SubMeshBoundingNodes = loader.LoadList<BoundingNode>(numSubMeshBoundingNodes);
                SubMeshBoundings = loader.LoadCustom(() => loader.ReadBoundings(numSubMeshBoundingNodes));
                SubMeshBoundingIndices = loader.LoadCustom(() => loader.ReadUInt16s(numSubMeshBoundingNodes));
            }
            uint userPointer = loader.ReadUInt32();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.WriteSignature(_signature);
            saver.SaveString(Name);
            saver.Write(Flags, true);
            saver.Write((ushort)saver.CurrentIndex);
            saver.Write(MaterialIndex);
            saver.Write(BoneIndex);
            saver.Write(VertexBufferIndex);
            saver.Write((ushort)SkinBoneIndices.Count);
            saver.Write(VertexSkinCount);
            saver.Write((byte)Meshes.Count);
            saver.Write((byte)KeyShapes.Count);
            saver.Write(TargetAttribCount);
            saver.Write((ushort)SubMeshBoundingNodes?.Count);
            saver.Write(Radius);
            saver.Save(VertexBuffer);
            saver.SaveList(Meshes);
            saver.SaveCustom(SkinBoneIndices, () => saver.Write(SkinBoneIndices));
            saver.SaveDict(KeyShapes);
            if (SubMeshBoundingNodes == null)
            {
                saver.SaveCustom(SubMeshBoundings, () => saver.Write(SubMeshBoundings));
            }
            else
            {
                saver.SaveList(SubMeshBoundingNodes);
                saver.SaveCustom(SubMeshBoundings, () => saver.Write(SubMeshBoundings));
                saver.SaveCustom(SubMeshBoundingIndices, () => saver.Write(SubMeshBoundingIndices));
            }
            saver.Write(0); // UserPointer
        }
    }

    /// <summary>
    /// Represents flags determining which data is available for <see cref="Shape"/> instances.
    /// </summary>
    [Flags]
    public enum ShapeFlags : uint
    {
        /// <summary>
        /// The <see cref="Shape"/> instance references a <see cref="VertexBuffer"/>.
        /// </summary>
        HasVertexBuffer = 1 << 1,

        /// <summary>
        /// Set in some BotW models.
        /// </summary>
        Unknown2 = 1 << 2
    }
}