using Syroot.Maths;
using Syroot.NintenTools.Bfres.Core;
using Syroot.NintenTools.Bfres.GX2;

namespace Syroot.NintenTools.Bfres
{
    /// <summary>
    /// Represents GX2 GPU configuration to determine how polygons are rendered.
    /// </summary>
    public class RenderState : IResData
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        private const uint _flagsMaskMode = 0b00000000_00000000_00000000_00000011;
        private const uint _flagsMaskBlendMode = 0b00000000_00000000_00000000_00110000;

        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private uint _flags;
        
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        public RenderStateFlagsMode FlagsMode
        {
            get { return (RenderStateFlagsMode)(_flags & _flagsMaskMode); }
            set { _flags &= ~_flagsMaskMode | (uint)value; }
        }

        public RenderStateFlagsBlendMode FlagsBlendMode
        {
            get { return (RenderStateFlagsBlendMode)(_flags & _flagsMaskBlendMode); }
            set { _flags &= ~_flagsMaskBlendMode | (uint)value; }
        }

        /// <summary>
        /// Gets or sets GX2 polygon drawing settings controlling if and how triangles are rendered.
        /// </summary>
        public PolygonControl PolygonControl { get; set; }

        /// <summary>
        /// Gets or sets GX2 settings controlling how depth and stencil buffer checks are performed and handled.
        /// </summary>
        public DepthControl DepthControl { get; set; }

        /// <summary>
        /// Gets or sets GX2 settings controlling additional alpha blending options.
        /// </summary>
        public AlphaControl AlphaControl { get; set; }
        
        /// <summary>
        /// Gets or sets the reference value used for alpha testing.
        /// </summary>
        public float AlphaRefValue { get; set; }

        /// <summary>
        /// Gets or sets GX2 settings controlling additional color blending options.
        /// </summary>
        public ColorControl ColorControl { get; set; }

        /// <summary>
        /// Gets or sets the blend target index.
        /// </summary>
        public uint BlendTarget { get; set; }

        /// <summary>
        /// Gets or sets GX2 settings controlling color and alpha blending.
        /// </summary>
        public BlendControl BlendControl { get; set; }
        
        /// <summary>
        /// Gets or sets the blend color to perform blending with.
        /// </summary>
        public Vector4F BlendColor { get; set; }

        // ---- METHODS ------------------------------------------------------------------------------------------------

        void IResData.Load(ResFileLoader loader)
        {
            _flags = loader.ReadUInt32();
            PolygonControl = new PolygonControl() { Value = loader.ReadUInt32() };
            DepthControl = new DepthControl() { Value = loader.ReadUInt32() };
            AlphaControl = new AlphaControl() { Value = loader.ReadUInt32() };
            AlphaRefValue = loader.ReadSingle();
            ColorControl = new ColorControl() { Value = loader.ReadUInt32() };
            BlendTarget = loader.ReadUInt32();
            BlendControl = new BlendControl() { Value = loader.ReadUInt32() };
            BlendColor = loader.ReadVector4F();
        }
        
        void IResData.Save(ResFileSaver saver)
        {
            saver.Write(_flags);
            saver.Write(PolygonControl.Value);
            saver.Write(DepthControl.Value);
            saver.Write(AlphaControl.Value);
            saver.Write(AlphaRefValue);
            saver.Write(ColorControl.Value);
            saver.Write(BlendTarget);
            saver.Write(BlendControl.Value);
            saver.Write(BlendColor);
        }
    }
    
    public enum RenderStateFlagsMode : uint
    {
        Custom,
        Opaque,
        AlphaMask,
        Translucent
    }

    public enum RenderStateFlagsBlendMode : uint
    {
        None,
        Color,
        Logical
    }
}