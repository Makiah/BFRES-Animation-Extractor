using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a 4-component color with the components red, green, blue and alpha, representing each value as a
    /// float.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ColorF : IEquatable<ColorF>, IEquatableByRef<ColorF>, INearlyEquatable<ColorF>,
        INearlyEquatableByRef<ColorF>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        #region ---- Pre-defined colors ----

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFFFF00.
        /// </summary>
        public static readonly ColorF Transparent = new ColorF(1f, 1f, 1f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F0F8FFFF.
        /// </summary>
        public static readonly ColorF AliceBlue = new ColorF(0.941f, 0.973f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FAEBD7FF.
        /// </summary>
        public static readonly ColorF AntiqueWhite = new ColorF(0.98f, 0.922f, 0.843f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00FFFFFF.
        /// </summary>
        public static readonly ColorF Aqua = new ColorF(0f, 1f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #7FFFD4FF.
        /// </summary>
        public static readonly ColorF Aquamarine = new ColorF(0.498f, 1f, 0.831f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F0FFFFFF.
        /// </summary>
        public static readonly ColorF Azure = new ColorF(0.941f, 1f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F5F5DCFF.
        /// </summary>
        public static readonly ColorF Beige = new ColorF(0.961f, 0.961f, 0.863f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFE4C4FF.
        /// </summary>
        public static readonly ColorF Bisque = new ColorF(1f, 0.894f, 0.769f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #000000FF.
        /// </summary>
        public static readonly ColorF Black = new ColorF(0f, 0f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFEBCDFF.
        /// </summary>
        public static readonly ColorF BlanchedAlmond = new ColorF(1f, 0.922f, 0.804f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #0000FFFF.
        /// </summary>
        public static readonly ColorF Blue = new ColorF(0f, 0f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #8A2BE2FF.
        /// </summary>
        public static readonly ColorF BlueViolet = new ColorF(0.541f, 0.169f, 0.886f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #A52A2AFF.
        /// </summary>
        public static readonly ColorF Brown = new ColorF(0.647f, 0.165f, 0.165f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #DEB887FF.
        /// </summary>
        public static readonly ColorF BurlyWood = new ColorF(0.871f, 0.722f, 0.529f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #5F9EA0FF.
        /// </summary>
        public static readonly ColorF CadetBlue = new ColorF(0.373f, 0.62f, 0.627f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #7FFF00FF.
        /// </summary>
        public static readonly ColorF Chartreuse = new ColorF(0.498f, 1f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #D2691EFF.
        /// </summary>
        public static readonly ColorF Chocolate = new ColorF(0.824f, 0.412f, 0.118f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF7F50FF.
        /// </summary>
        public static readonly ColorF Coral = new ColorF(1f, 0.498f, 0.314f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #6495EDFF.
        /// </summary>
        public static readonly ColorF CornflowerBlue = new ColorF(0.392f, 0.584f, 0.929f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFF8DCFF.
        /// </summary>
        public static readonly ColorF Cornsilk = new ColorF(1f, 0.973f, 0.863f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #DC143CFF.
        /// </summary>
        public static readonly ColorF Crimson = new ColorF(0.863f, 0.078f, 0.235f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00FFFFFF.
        /// </summary>
        public static readonly ColorF Cyan = new ColorF(0f, 1f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00008BFF.
        /// </summary>
        public static readonly ColorF DarkBlue = new ColorF(0f, 0f, 0.545f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #008B8BFF.
        /// </summary>
        public static readonly ColorF DarkCyan = new ColorF(0f, 0.545f, 0.545f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #B8860BFF.
        /// </summary>
        public static readonly ColorF DarkGoldenrod = new ColorF(0.722f, 0.525f, 0.043f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #A9A9A9FF.
        /// </summary>
        public static readonly ColorF DarkGray = new ColorF(0.663f, 0.663f, 0.663f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #006400FF.
        /// </summary>
        public static readonly ColorF DarkGreen = new ColorF(0f, 0.392f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #BDB76BFF.
        /// </summary>
        public static readonly ColorF DarkKhaki = new ColorF(0.741f, 0.718f, 0.42f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #8B008BFF.
        /// </summary>
        public static readonly ColorF DarkMagenta = new ColorF(0.545f, 0f, 0.545f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #556B2FFF.
        /// </summary>
        public static readonly ColorF DarkOliveGreen = new ColorF(0.333f, 0.42f, 0.184f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF8C00FF.
        /// </summary>
        public static readonly ColorF DarkOrange = new ColorF(1f, 0.549f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #9932CCFF.
        /// </summary>
        public static readonly ColorF DarkOrchid = new ColorF(0.6f, 0.196f, 0.8f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #8B0000FF.
        /// </summary>
        public static readonly ColorF DarkRed = new ColorF(0.545f, 0f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #E9967AFF.
        /// </summary>
        public static readonly ColorF DarkSalmon = new ColorF(0.914f, 0.588f, 0.478f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #8FBC8BFF.
        /// </summary>
        public static readonly ColorF DarkSeaGreen = new ColorF(0.561f, 0.737f, 0.545f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #483D8BFF.
        /// </summary>
        public static readonly ColorF DarkSlateBlue = new ColorF(0.282f, 0.239f, 0.545f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #2F4F4FFF.
        /// </summary>
        public static readonly ColorF DarkSlateGray = new ColorF(0.184f, 0.31f, 0.31f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00CED1FF.
        /// </summary>
        public static readonly ColorF DarkTurquoise = new ColorF(0f, 0.808f, 0.82f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #9400D3FF.
        /// </summary>
        public static readonly ColorF DarkViolet = new ColorF(0.58f, 0f, 0.827f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF1493FF.
        /// </summary>
        public static readonly ColorF DeepPink = new ColorF(1f, 0.078f, 0.576f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00BFFFFF.
        /// </summary>
        public static readonly ColorF DeepSkyBlue = new ColorF(0f, 0.749f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #696969FF.
        /// </summary>
        public static readonly ColorF DimGray = new ColorF(0.412f, 0.412f, 0.412f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #1E90FFFF.
        /// </summary>
        public static readonly ColorF DodgerBlue = new ColorF(0.118f, 0.565f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #B22222FF.
        /// </summary>
        public static readonly ColorF Firebrick = new ColorF(0.698f, 0.133f, 0.133f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFAF0FF.
        /// </summary>
        public static readonly ColorF FloralWhite = new ColorF(1f, 0.98f, 0.941f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #228B22FF.
        /// </summary>
        public static readonly ColorF ForestGreen = new ColorF(0.133f, 0.545f, 0.133f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF00FFFF.
        /// </summary>
        public static readonly ColorF Fuchsia = new ColorF(1f, 0f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #DCDCDCFF.
        /// </summary>
        public static readonly ColorF Gainsboro = new ColorF(0.863f, 0.863f, 0.863f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F8F8FFFF.
        /// </summary>
        public static readonly ColorF GhostWhite = new ColorF(0.973f, 0.973f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFD700FF.
        /// </summary>
        public static readonly ColorF Gold = new ColorF(1f, 0.843f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #DAA520FF.
        /// </summary>
        public static readonly ColorF Goldenrod = new ColorF(0.855f, 0.647f, 0.125f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #808080FF.
        /// </summary>
        public static readonly ColorF Gray = new ColorF(0.502f, 0.502f, 0.502f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #008000FF.
        /// </summary>
        public static readonly ColorF Green = new ColorF(0f, 0.502f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #ADFF2FFF.
        /// </summary>
        public static readonly ColorF GreenYellow = new ColorF(0.678f, 1f, 0.184f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F0FFF0FF.
        /// </summary>
        public static readonly ColorF Honeydew = new ColorF(0.941f, 1f, 0.941f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF69B4FF.
        /// </summary>
        public static readonly ColorF HotPink = new ColorF(1f, 0.412f, 0.706f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #CD5C5CFF.
        /// </summary>
        public static readonly ColorF IndianRed = new ColorF(0.804f, 0.361f, 0.361f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #4B0082FF.
        /// </summary>
        public static readonly ColorF Indigo = new ColorF(0.294f, 0f, 0.51f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFFF0FF.
        /// </summary>
        public static readonly ColorF Ivory = new ColorF(1f, 1f, 0.941f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F0E68CFF.
        /// </summary>
        public static readonly ColorF Khaki = new ColorF(0.941f, 0.902f, 0.549f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #E6E6FAFF.
        /// </summary>
        public static readonly ColorF Lavender = new ColorF(0.902f, 0.902f, 0.98f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFF0F5FF.
        /// </summary>
        public static readonly ColorF LavenderBlush = new ColorF(1f, 0.941f, 0.961f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #7CFC00FF.
        /// </summary>
        public static readonly ColorF LawnGreen = new ColorF(0.486f, 0.988f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFACDFF.
        /// </summary>
        public static readonly ColorF LemonChiffon = new ColorF(1f, 0.98f, 0.804f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #ADD8E6FF.
        /// </summary>
        public static readonly ColorF LightBlue = new ColorF(0.678f, 0.847f, 0.902f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F08080FF.
        /// </summary>
        public static readonly ColorF LightCoral = new ColorF(0.941f, 0.502f, 0.502f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #E0FFFFFF.
        /// </summary>
        public static readonly ColorF LightCyan = new ColorF(0.878f, 1f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FAFAD2FF.
        /// </summary>
        public static readonly ColorF LightGoldenrodYellow = new ColorF(0.98f, 0.98f, 0.824f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #D3D3D3FF.
        /// </summary>
        public static readonly ColorF LightGray = new ColorF(0.827f, 0.827f, 0.827f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #90EE90FF.
        /// </summary>
        public static readonly ColorF LightGreen = new ColorF(0.565f, 0.933f, 0.565f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFB6C1FF.
        /// </summary>
        public static readonly ColorF LightPink = new ColorF(1f, 0.714f, 0.757f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFA07AFF.
        /// </summary>
        public static readonly ColorF LightSalmon = new ColorF(1f, 0.627f, 0.478f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #20B2AAFF.
        /// </summary>
        public static readonly ColorF LightSeaGreen = new ColorF(0.125f, 0.698f, 0.667f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #87CEFAFF.
        /// </summary>
        public static readonly ColorF LightSkyBlue = new ColorF(0.529f, 0.808f, 0.98f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #778899FF.
        /// </summary>
        public static readonly ColorF LightSlateGray = new ColorF(0.467f, 0.533f, 0.6f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #B0C4DEFF.
        /// </summary>
        public static readonly ColorF LightSteelBlue = new ColorF(0.69f, 0.769f, 0.871f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFFE0FF.
        /// </summary>
        public static readonly ColorF LightYellow = new ColorF(1f, 1f, 0.878f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00FF00FF.
        /// </summary>
        public static readonly ColorF Lime = new ColorF(0f, 1f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #32CD32FF.
        /// </summary>
        public static readonly ColorF LimeGreen = new ColorF(0.196f, 0.804f, 0.196f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FAF0E6FF.
        /// </summary>
        public static readonly ColorF Linen = new ColorF(0.98f, 0.941f, 0.902f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF00FFFF.
        /// </summary>
        public static readonly ColorF Magenta = new ColorF(1f, 0f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #800000FF.
        /// </summary>
        public static readonly ColorF Maroon = new ColorF(0.502f, 0f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #66CDAAFF.
        /// </summary>
        public static readonly ColorF MediumAquamarine = new ColorF(0.4f, 0.804f, 0.667f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #0000CDFF.
        /// </summary>
        public static readonly ColorF MediumBlue = new ColorF(0f, 0f, 0.804f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #BA55D3FF.
        /// </summary>
        public static readonly ColorF MediumOrchid = new ColorF(0.729f, 0.333f, 0.827f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #9370DBFF.
        /// </summary>
        public static readonly ColorF MediumPurple = new ColorF(0.576f, 0.439f, 0.859f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #3CB371FF.
        /// </summary>
        public static readonly ColorF MediumSeaGreen = new ColorF(0.235f, 0.702f, 0.443f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #7B68EEFF.
        /// </summary>
        public static readonly ColorF MediumSlateBlue = new ColorF(0.482f, 0.408f, 0.933f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00FA9AFF.
        /// </summary>
        public static readonly ColorF MediumSpringGreen = new ColorF(0f, 0.98f, 0.604f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #48D1CCFF.
        /// </summary>
        public static readonly ColorF MediumTurquoise = new ColorF(0.282f, 0.82f, 0.8f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #C71585FF.
        /// </summary>
        public static readonly ColorF MediumVioletRed = new ColorF(0.78f, 0.082f, 0.522f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #191970FF.
        /// </summary>
        public static readonly ColorF MidnightBlue = new ColorF(0.098f, 0.098f, 0.439f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F5FFFAFF.
        /// </summary>
        public static readonly ColorF MintCream = new ColorF(0.961f, 1f, 0.98f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFE4E1FF.
        /// </summary>
        public static readonly ColorF MistyRose = new ColorF(1f, 0.894f, 0.882f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFE4B5FF.
        /// </summary>
        public static readonly ColorF Moccasin = new ColorF(1f, 0.894f, 0.71f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFDEADFF.
        /// </summary>
        public static readonly ColorF NavajoWhite = new ColorF(1f, 0.871f, 0.678f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #000080FF.
        /// </summary>
        public static readonly ColorF Navy = new ColorF(0f, 0f, 0.502f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FDF5E6FF.
        /// </summary>
        public static readonly ColorF OldLace = new ColorF(0.992f, 0.961f, 0.902f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #808000FF.
        /// </summary>
        public static readonly ColorF Olive = new ColorF(0.502f, 0.502f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #6B8E23FF.
        /// </summary>
        public static readonly ColorF OliveDrab = new ColorF(0.42f, 0.557f, 0.137f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFA500FF.
        /// </summary>
        public static readonly ColorF Orange = new ColorF(1f, 0.647f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF4500FF.
        /// </summary>
        public static readonly ColorF OrangeRed = new ColorF(1f, 0.271f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #DA70D6FF.
        /// </summary>
        public static readonly ColorF Orchid = new ColorF(0.855f, 0.439f, 0.839f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #EEE8AAFF.
        /// </summary>
        public static readonly ColorF PaleGoldenrod = new ColorF(0.933f, 0.91f, 0.667f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #98FB98FF.
        /// </summary>
        public static readonly ColorF PaleGreen = new ColorF(0.596f, 0.984f, 0.596f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #AFEEEEFF.
        /// </summary>
        public static readonly ColorF PaleTurquoise = new ColorF(0.686f, 0.933f, 0.933f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #DB7093FF.
        /// </summary>
        public static readonly ColorF PaleVioletRed = new ColorF(0.859f, 0.439f, 0.576f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFEFD5FF.
        /// </summary>
        public static readonly ColorF PapayaWhip = new ColorF(1f, 0.937f, 0.835f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFDAB9FF.
        /// </summary>
        public static readonly ColorF PeachPuff = new ColorF(1f, 0.855f, 0.725f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #CD853FFF.
        /// </summary>
        public static readonly ColorF Peru = new ColorF(0.804f, 0.522f, 0.247f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFC0CBFF.
        /// </summary>
        public static readonly ColorF Pink = new ColorF(1f, 0.753f, 0.796f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #DDA0DDFF.
        /// </summary>
        public static readonly ColorF Plum = new ColorF(0.867f, 0.627f, 0.867f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #B0E0E6FF.
        /// </summary>
        public static readonly ColorF PowderBlue = new ColorF(0.69f, 0.878f, 0.902f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #800080FF.
        /// </summary>
        public static readonly ColorF Purple = new ColorF(0.502f, 0f, 0.502f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF0000FF.
        /// </summary>
        public static readonly ColorF Red = new ColorF(1f, 0f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #BC8F8FFF.
        /// </summary>
        public static readonly ColorF RosyBrown = new ColorF(0.737f, 0.561f, 0.561f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #4169E1FF.
        /// </summary>
        public static readonly ColorF RoyalBlue = new ColorF(0.255f, 0.412f, 0.882f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #8B4513FF.
        /// </summary>
        public static readonly ColorF SaddleBrown = new ColorF(0.545f, 0.271f, 0.075f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FA8072FF.
        /// </summary>
        public static readonly ColorF Salmon = new ColorF(0.98f, 0.502f, 0.447f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F4A460FF.
        /// </summary>
        public static readonly ColorF SandyBrown = new ColorF(0.957f, 0.643f, 0.376f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #2E8B57FF.
        /// </summary>
        public static readonly ColorF SeaGreen = new ColorF(0.18f, 0.545f, 0.341f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFF5EEFF.
        /// </summary>
        public static readonly ColorF SeaShell = new ColorF(1f, 0.961f, 0.933f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #A0522DFF.
        /// </summary>
        public static readonly ColorF Sienna = new ColorF(0.627f, 0.322f, 0.176f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #C0C0C0FF.
        /// </summary>
        public static readonly ColorF Silver = new ColorF(0.753f, 0.753f, 0.753f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #87CEEBFF.
        /// </summary>
        public static readonly ColorF SkyBlue = new ColorF(0.529f, 0.808f, 0.922f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #6A5ACDFF.
        /// </summary>
        public static readonly ColorF SlateBlue = new ColorF(0.416f, 0.353f, 0.804f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #708090FF.
        /// </summary>
        public static readonly ColorF SlateGray = new ColorF(0.439f, 0.502f, 0.565f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFAFAFF.
        /// </summary>
        public static readonly ColorF Snow = new ColorF(1f, 0.98f, 0.98f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #00FF7FFF.
        /// </summary>
        public static readonly ColorF SpringGreen = new ColorF(0f, 1f, 0.498f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #4682B4FF.
        /// </summary>
        public static readonly ColorF SteelBlue = new ColorF(0.275f, 0.51f, 0.706f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #D2B48CFF.
        /// </summary>
        public static readonly ColorF Tan = new ColorF(0.824f, 0.706f, 0.549f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #008080FF.
        /// </summary>
        public static readonly ColorF Teal = new ColorF(0f, 0.502f, 0.502f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #D8BFD8FF.
        /// </summary>
        public static readonly ColorF Thistle = new ColorF(0.847f, 0.749f, 0.847f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FF6347FF.
        /// </summary>
        public static readonly ColorF Tomato = new ColorF(1f, 0.388f, 0.278f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #40E0D0FF.
        /// </summary>
        public static readonly ColorF Turquoise = new ColorF(0.251f, 0.878f, 0.816f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #EE82EEFF.
        /// </summary>
        public static readonly ColorF Violet = new ColorF(0.933f, 0.51f, 0.933f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F5DEB3FF.
        /// </summary>
        public static readonly ColorF Wheat = new ColorF(0.961f, 0.871f, 0.702f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFFFFFF.
        /// </summary>
        public static readonly ColorF White = new ColorF(1f, 1f, 1f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #F5F5F5FF.
        /// </summary>
        public static readonly ColorF WhiteSmoke = new ColorF(0.961f, 0.961f, 0.961f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #FFFF00FF.
        /// </summary>
        public static readonly ColorF Yellow = new ColorF(1f, 1f, 0f);

        /// <summary>
        /// A <see cref="ColorF"/> with an RGBA value of #9ACD32FF.
        /// </summary>
        public static readonly ColorF YellowGreen = new ColorF(0.604f, 0.804f, 0.196f);

        #endregion

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 4;

        /// <summary>
        /// Gets the size of this structure.
        /// </summary>
        public const int SizeInBytes = ValueCount * sizeof(float);

        // ---- MEMBERS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// The amount of red between 0f (no red) and 1f (full red).
        /// </summary>
        public float R;

        /// <summary>
        /// The amount of green between 0f (no green) and 1f (full green).
        /// </summary>
        public float G;

        /// <summary>
        /// The amount of blue between 0f (no blue) and 1f (full blue).
        /// </summary>
        public float B;

        /// <summary>
        /// The opacity amount between 0f (fully transparent) and 1f (fully opaque).
        /// </summary>
        public float A;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorF"/> struct with the specified components and full opacity.
        /// </summary>
        /// <param name="red">The amount of red.</param>
        /// <param name="green">The amount of green.</param>
        /// <param name="blue">The amount of blue.</param>
        public ColorF(float red, float green, float blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 1f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorF"/> struct with the specified color and opacity amount.
        /// </summary>
        /// <param name="color">The color which to use as a base.</param>
        /// <param name="alpha">The opacity amount of the color.</param>
        public ColorF(ColorF color, float alpha)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorF"/> struct with the specified component values.
        /// </summary>
        /// <param name="red">The amount of red.</param>
        /// <param name="green">The amount of green.</param>
        /// <param name="blue">The amount of blue.</param>
        /// <param name="alpha">The opacity amount.</param>
        public ColorF(float red, float green, float blue, float alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="ColorF"/> are the same as
        /// the components of the second specified <see cref="ColorF"/>.
        /// </summary>
        /// <param name="a">The first <see cref="ColorF"/> to compare.</param>
        /// <param name="b">The second <see cref="ColorF"/> to compare.</param>
        /// <returns><c>true</c>, if the components of both <see cref="ColorF"/> are the same.</returns>
        public static bool operator ==(ColorF a, ColorF b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="ColorF"/> are not the same
        /// as the components of the second specified<see cref="ColorF"/>.
        /// </summary>
        /// <param name="a">The first <see cref="ColorF"/> to compare.</param>
        /// <param name="b">The second <see cref="ColorF"/> to compare.</param>
        /// <returns><c>true</c>, if the components of both <see cref="ColorF"/> are not the same.</returns>
        public static bool operator !=(ColorF a, ColorF b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert from.</param>
        /// <returns>The retrieved <see cref="ColorF"/>.</returns>
        public static implicit operator ColorF(Color color)
        {
            return new ColorF((float)color.R / 255f, (float)color.G / 255f, (float)color.B / 255f,
                (float)color.A / 255f);
        }
        
        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="ColorF"/> are the same as the components
        /// of the second specified <see cref="ColorF"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="ColorF"/>.</param>
        /// <returns><c>true</c>, if the components of both <see cref="ColorF"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ColorF))
            {
                return false;
            }
            ColorF colorF = (ColorF)obj;
            return Equals(ref colorF);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash *= 31 + R.GetHashCode();
                hash *= 31 + G.GetHashCode();
                hash *= 31 + B.GetHashCode();
                hash *= 31 + A.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="ColorF"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="ColorF"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{R={0},G={1},B={2},A={3}}}", R, G, B, A);
        }

        /// <summary>
        /// Indicates whether the current <see cref="ColorF"/> is equal to another <see cref="ColorF"/>.
        /// </summary>
        /// <param name="other">A <see cref="ColorF"/> to compare with this <see cref="ColorF"/>.</param>
        /// <returns><c>true</c> if the current <see cref="ColorF"/> is equal to the other parameter; otherwise <c>false</c>.
        /// </returns>
        public bool Equals(ColorF other)
        {
            return Equals(ref other);
        }
        
        /// <summary>
        /// Indicates whether the current <see cref="ColorF"/> is equal to another <see cref="ColorF"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="ColorF"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="ColorF"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref ColorF other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        /// <summary>
        /// Indicates whether the current <see cref="ColorF"/> is nearly equal to another <see cref="ColorF"/>.
        /// </summary>
        /// <param name="other">A <see cref="ColorF"/> to compare with this <see cref="ColorF"/>.</param>
        /// <returns><c>true</c> if the current <see cref="ColorF"/> is nearly equal to the other parameter; otherwise
        /// <c>false</c>.</returns>
        public bool NearlyEquals(ColorF other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="ColorF"/> is nearly equal to another <see cref="ColorF"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="ColorF"/> to compare with this <see cref="ColorF"/>.</param>
        /// <returns><c>true</c> if the current <see cref="ColorF"/> is nearly equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool NearlyEquals(ref ColorF other)
        {
            return R.NearlyEquals(other.R) && G.NearlyEquals(other.G) && B.NearlyEquals(other.B)
                && A.NearlyEquals(other.A);
        }
    }
}
