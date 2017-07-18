using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a 4-component color with the components red, green, blue and alpha, representing each value as a
    /// byte.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Color : IEquatable<Color>, IEquatableByRef<Color>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        #region ---- Pre-defined colors ----

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFFFF00.
        /// </summary>
        public static readonly Color Transparent = new Color(255, 255, 255, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F0F8FFFF.
        /// </summary>
        public static readonly Color AliceBlue = new Color(240, 248, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FAEBD7FF.
        /// </summary>
        public static readonly Color AntiqueWhite = new Color(250, 235, 215);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00FFFFFF.
        /// </summary>
        public static readonly Color Aqua = new Color(0, 255, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #7FFFD4FF.
        /// </summary>
        public static readonly Color Aquamarine = new Color(127, 255, 212);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F0FFFFFF.
        /// </summary>
        public static readonly Color Azure = new Color(240, 255, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F5F5DCFF.
        /// </summary>
        public static readonly Color Beige = new Color(245, 245, 220);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFE4C4FF.
        /// </summary>
        public static readonly Color Bisque = new Color(255, 228, 196);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #000000FF.
        /// </summary>
        public static readonly Color Black = new Color(0, 0, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFEBCDFF.
        /// </summary>
        public static readonly Color BlanchedAlmond = new Color(255, 235, 205);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #0000FFFF.
        /// </summary>
        public static readonly Color Blue = new Color(0, 0, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #8A2BE2FF.
        /// </summary>
        public static readonly Color BlueViolet = new Color(138, 43, 226);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #A52A2AFF.
        /// </summary>
        public static readonly Color Brown = new Color(165, 42, 42);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #DEB887FF.
        /// </summary>
        public static readonly Color BurlyWood = new Color(222, 184, 135);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #5F9EA0FF.
        /// </summary>
        public static readonly Color CadetBlue = new Color(95, 158, 160);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #7FFF00FF.
        /// </summary>
        public static readonly Color Chartreuse = new Color(127, 255, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #D2691EFF.
        /// </summary>
        public static readonly Color Chocolate = new Color(210, 105, 30);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF7F50FF.
        /// </summary>
        public static readonly Color Coral = new Color(255, 127, 80);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #6495EDFF.
        /// </summary>
        public static readonly Color CornflowerBlue = new Color(100, 149, 237);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFF8DCFF.
        /// </summary>
        public static readonly Color Cornsilk = new Color(255, 248, 220);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #DC143CFF.
        /// </summary>
        public static readonly Color Crimson = new Color(220, 20, 60);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00FFFFFF.
        /// </summary>
        public static readonly Color Cyan = new Color(0, 255, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00008BFF.
        /// </summary>
        public static readonly Color DarkBlue = new Color(0, 0, 139);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #008B8BFF.
        /// </summary>
        public static readonly Color DarkCyan = new Color(0, 139, 139);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #B8860BFF.
        /// </summary>
        public static readonly Color DarkGoldenrod = new Color(184, 134, 11);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #A9A9A9FF.
        /// </summary>
        public static readonly Color DarkGray = new Color(169, 169, 169);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #006400FF.
        /// </summary>
        public static readonly Color DarkGreen = new Color(0, 100, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #BDB76BFF.
        /// </summary>
        public static readonly Color DarkKhaki = new Color(189, 183, 107);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #8B008BFF.
        /// </summary>
        public static readonly Color DarkMagenta = new Color(139, 0, 139);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #556B2FFF.
        /// </summary>
        public static readonly Color DarkOliveGreen = new Color(85, 107, 47);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF8C00FF.
        /// </summary>
        public static readonly Color DarkOrange = new Color(255, 140, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #9932CCFF.
        /// </summary>
        public static readonly Color DarkOrchid = new Color(153, 50, 204);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #8B0000FF.
        /// </summary>
        public static readonly Color DarkRed = new Color(139, 0, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #E9967AFF.
        /// </summary>
        public static readonly Color DarkSalmon = new Color(233, 150, 122);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #8FBC8BFF.
        /// </summary>
        public static readonly Color DarkSeaGreen = new Color(143, 188, 139);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #483D8BFF.
        /// </summary>
        public static readonly Color DarkSlateBlue = new Color(72, 61, 139);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #2F4F4FFF.
        /// </summary>
        public static readonly Color DarkSlateGray = new Color(47, 79, 79);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00CED1FF.
        /// </summary>
        public static readonly Color DarkTurquoise = new Color(0, 206, 209);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #9400D3FF.
        /// </summary>
        public static readonly Color DarkViolet = new Color(148, 0, 211);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF1493FF.
        /// </summary>
        public static readonly Color DeepPink = new Color(255, 20, 147);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00BFFFFF.
        /// </summary>
        public static readonly Color DeepSkyBlue = new Color(0, 191, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #696969FF.
        /// </summary>
        public static readonly Color DimGray = new Color(105, 105, 105);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #1E90FFFF.
        /// </summary>
        public static readonly Color DodgerBlue = new Color(30, 144, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #B22222FF.
        /// </summary>
        public static readonly Color Firebrick = new Color(178, 34, 34);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFAF0FF.
        /// </summary>
        public static readonly Color FloralWhite = new Color(255, 250, 240);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #228B22FF.
        /// </summary>
        public static readonly Color ForestGreen = new Color(34, 139, 34);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF00FFFF.
        /// </summary>
        public static readonly Color Fuchsia = new Color(255, 0, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #DCDCDCFF.
        /// </summary>
        public static readonly Color Gainsboro = new Color(220, 220, 220);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F8F8FFFF.
        /// </summary>
        public static readonly Color GhostWhite = new Color(248, 248, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFD700FF.
        /// </summary>
        public static readonly Color Gold = new Color(255, 215, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #DAA520FF.
        /// </summary>
        public static readonly Color Goldenrod = new Color(218, 165, 32);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #808080FF.
        /// </summary>
        public static readonly Color Gray = new Color(128, 128, 128);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #008000FF.
        /// </summary>
        public static readonly Color Green = new Color(0, 128, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #ADFF2FFF.
        /// </summary>
        public static readonly Color GreenYellow = new Color(173, 255, 47);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F0FFF0FF.
        /// </summary>
        public static readonly Color Honeydew = new Color(240, 255, 240);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF69B4FF.
        /// </summary>
        public static readonly Color HotPink = new Color(255, 105, 180);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #CD5C5CFF.
        /// </summary>
        public static readonly Color IndianRed = new Color(205, 92, 92);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #4B0082FF.
        /// </summary>
        public static readonly Color Indigo = new Color(75, 0, 130);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFFF0FF.
        /// </summary>
        public static readonly Color Ivory = new Color(255, 255, 240);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F0E68CFF.
        /// </summary>
        public static readonly Color Khaki = new Color(240, 230, 140);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #E6E6FAFF.
        /// </summary>
        public static readonly Color Lavender = new Color(230, 230, 250);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFF0F5FF.
        /// </summary>
        public static readonly Color LavenderBlush = new Color(255, 240, 245);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #7CFC00FF.
        /// </summary>
        public static readonly Color LawnGreen = new Color(124, 252, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFACDFF.
        /// </summary>
        public static readonly Color LemonChiffon = new Color(255, 250, 205);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #ADD8E6FF.
        /// </summary>
        public static readonly Color LightBlue = new Color(173, 216, 230);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F08080FF.
        /// </summary>
        public static readonly Color LightCoral = new Color(240, 128, 128);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #E0FFFFFF.
        /// </summary>
        public static readonly Color LightCyan = new Color(224, 255, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FAFAD2FF.
        /// </summary>
        public static readonly Color LightGoldenrodYellow = new Color(250, 250, 210);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #D3D3D3FF.
        /// </summary>
        public static readonly Color LightGray = new Color(211, 211, 211);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #90EE90FF.
        /// </summary>
        public static readonly Color LightGreen = new Color(144, 238, 144);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFB6C1FF.
        /// </summary>
        public static readonly Color LightPink = new Color(255, 182, 193);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFA07AFF.
        /// </summary>
        public static readonly Color LightSalmon = new Color(255, 160, 122);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #20B2AAFF.
        /// </summary>
        public static readonly Color LightSeaGreen = new Color(32, 178, 170);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #87CEFAFF.
        /// </summary>
        public static readonly Color LightSkyBlue = new Color(135, 206, 250);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #778899FF.
        /// </summary>
        public static readonly Color LightSlateGray = new Color(119, 136, 153);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #B0C4DEFF.
        /// </summary>
        public static readonly Color LightSteelBlue = new Color(176, 196, 222);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFFE0FF.
        /// </summary>
        public static readonly Color LightYellow = new Color(255, 255, 224);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00FF00FF.
        /// </summary>
        public static readonly Color Lime = new Color(0, 255, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #32CD32FF.
        /// </summary>
        public static readonly Color LimeGreen = new Color(50, 205, 50);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FAF0E6FF.
        /// </summary>
        public static readonly Color Linen = new Color(250, 240, 230);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF00FFFF.
        /// </summary>
        public static readonly Color Magenta = new Color(255, 0, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #800000FF.
        /// </summary>
        public static readonly Color Maroon = new Color(128, 0, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #66CDAAFF.
        /// </summary>
        public static readonly Color MediumAquamarine = new Color(102, 205, 170);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #0000CDFF.
        /// </summary>
        public static readonly Color MediumBlue = new Color(0, 0, 205);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #BA55D3FF.
        /// </summary>
        public static readonly Color MediumOrchid = new Color(186, 85, 211);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #9370DBFF.
        /// </summary>
        public static readonly Color MediumPurple = new Color(147, 112, 219);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #3CB371FF.
        /// </summary>
        public static readonly Color MediumSeaGreen = new Color(60, 179, 113);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #7B68EEFF.
        /// </summary>
        public static readonly Color MediumSlateBlue = new Color(123, 104, 238);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00FA9AFF.
        /// </summary>
        public static readonly Color MediumSpringGreen = new Color(0, 250, 154);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #48D1CCFF.
        /// </summary>
        public static readonly Color MediumTurquoise = new Color(72, 209, 204);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #C71585FF.
        /// </summary>
        public static readonly Color MediumVioletRed = new Color(199, 21, 133);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #191970FF.
        /// </summary>
        public static readonly Color MidnightBlue = new Color(25, 25, 112);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F5FFFAFF.
        /// </summary>
        public static readonly Color MintCream = new Color(245, 255, 250);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFE4E1FF.
        /// </summary>
        public static readonly Color MistyRose = new Color(255, 228, 225);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFE4B5FF.
        /// </summary>
        public static readonly Color Moccasin = new Color(255, 228, 181);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFDEADFF.
        /// </summary>
        public static readonly Color NavajoWhite = new Color(255, 222, 173);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #000080FF.
        /// </summary>
        public static readonly Color Navy = new Color(0, 0, 128);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FDF5E6FF.
        /// </summary>
        public static readonly Color OldLace = new Color(253, 245, 230);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #808000FF.
        /// </summary>
        public static readonly Color Olive = new Color(128, 128, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #6B8E23FF.
        /// </summary>
        public static readonly Color OliveDrab = new Color(107, 142, 35);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFA500FF.
        /// </summary>
        public static readonly Color Orange = new Color(255, 165, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF4500FF.
        /// </summary>
        public static readonly Color OrangeRed = new Color(255, 69, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #DA70D6FF.
        /// </summary>
        public static readonly Color Orchid = new Color(218, 112, 214);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #EEE8AAFF.
        /// </summary>
        public static readonly Color PaleGoldenrod = new Color(238, 232, 170);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #98FB98FF.
        /// </summary>
        public static readonly Color PaleGreen = new Color(152, 251, 152);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #AFEEEEFF.
        /// </summary>
        public static readonly Color PaleTurquoise = new Color(175, 238, 238);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #DB7093FF.
        /// </summary>
        public static readonly Color PaleVioletRed = new Color(219, 112, 147);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFEFD5FF.
        /// </summary>
        public static readonly Color PapayaWhip = new Color(255, 239, 213);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFDAB9FF.
        /// </summary>
        public static readonly Color PeachPuff = new Color(255, 218, 185);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #CD853FFF.
        /// </summary>
        public static readonly Color Peru = new Color(205, 133, 63);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFC0CBFF.
        /// </summary>
        public static readonly Color Pink = new Color(255, 192, 203);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #DDA0DDFF.
        /// </summary>
        public static readonly Color Plum = new Color(221, 160, 221);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #B0E0E6FF.
        /// </summary>
        public static readonly Color PowderBlue = new Color(176, 224, 230);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #800080FF.
        /// </summary>
        public static readonly Color Purple = new Color(128, 0, 128);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF0000FF.
        /// </summary>
        public static readonly Color Red = new Color(255, 0, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #BC8F8FFF.
        /// </summary>
        public static readonly Color RosyBrown = new Color(188, 143, 143);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #4169E1FF.
        /// </summary>
        public static readonly Color RoyalBlue = new Color(65, 105, 225);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #8B4513FF.
        /// </summary>
        public static readonly Color SaddleBrown = new Color(139, 69, 19);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FA8072FF.
        /// </summary>
        public static readonly Color Salmon = new Color(250, 128, 114);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F4A460FF.
        /// </summary>
        public static readonly Color SandyBrown = new Color(244, 164, 96);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #2E8B57FF.
        /// </summary>
        public static readonly Color SeaGreen = new Color(46, 139, 87);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFF5EEFF.
        /// </summary>
        public static readonly Color SeaShell = new Color(255, 245, 238);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #A0522DFF.
        /// </summary>
        public static readonly Color Sienna = new Color(160, 82, 45);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #C0C0C0FF.
        /// </summary>
        public static readonly Color Silver = new Color(192, 192, 192);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #87CEEBFF.
        /// </summary>
        public static readonly Color SkyBlue = new Color(135, 206, 235);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #6A5ACDFF.
        /// </summary>
        public static readonly Color SlateBlue = new Color(106, 90, 205);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #708090FF.
        /// </summary>
        public static readonly Color SlateGray = new Color(112, 128, 144);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFAFAFF.
        /// </summary>
        public static readonly Color Snow = new Color(255, 250, 250);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #00FF7FFF.
        /// </summary>
        public static readonly Color SpringGreen = new Color(0, 255, 127);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #4682B4FF.
        /// </summary>
        public static readonly Color SteelBlue = new Color(70, 130, 180);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #D2B48CFF.
        /// </summary>
        public static readonly Color Tan = new Color(210, 180, 140);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #008080FF.
        /// </summary>
        public static readonly Color Teal = new Color(0, 128, 128);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #D8BFD8FF.
        /// </summary>
        public static readonly Color Thistle = new Color(216, 191, 216);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FF6347FF.
        /// </summary>
        public static readonly Color Tomato = new Color(255, 99, 71);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #40E0D0FF.
        /// </summary>
        public static readonly Color Turquoise = new Color(64, 224, 208);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #EE82EEFF.
        /// </summary>
        public static readonly Color Violet = new Color(238, 130, 238);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F5DEB3FF.
        /// </summary>
        public static readonly Color Wheat = new Color(245, 222, 179);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFFFFFF.
        /// </summary>
        public static readonly Color White = new Color(255, 255, 255);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #F5F5F5FF.
        /// </summary>
        public static readonly Color WhiteSmoke = new Color(245, 245, 245);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #FFFF00FF.
        /// </summary>
        public static readonly Color Yellow = new Color(255, 255, 0);

        /// <summary>
        /// A <see cref="Color"/> with an RGBA value of #9ACD32FF.
        /// </summary>
        public static readonly Color YellowGreen = new Color(154, 205, 50);

        #endregion

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 4;

        /// <summary>
        /// Gets the size of this structure.
        /// </summary>
        public const int SizeInBytes = ValueCount * sizeof(byte);

        // ---- MEMBERS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// The amount of red between 0f (no red) and 1f (full red).
        /// </summary>
        public byte R;

        /// <summary>
        /// The amount of green between 0f (no green) and 1f (full green).
        /// </summary>
        public byte G;

        /// <summary>
        /// The amount of blue between 0f (no blue) and 1f (full blue).
        /// </summary>
        public byte B;

        /// <summary>
        /// The opacity amount between 0f (fully transparent) and 1f (fully opaque).
        /// </summary>
        public byte A;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct with the specified components and full opacity.
        /// </summary>
        /// <param name="red">The amount of red.</param>
        /// <param name="green">The amount of green.</param>
        /// <param name="blue">The amount of blue.</param>
        public Color(byte red, byte green, byte blue)
        {
            R = red;
            G = green;
            B = blue;
            A = 255;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct with the specified color and opacity amount.
        /// </summary>
        /// <param name="color">The color which to use as a base.</param>
        /// <param name="alpha">The opacity amount of the color.</param>
        public Color(Color color, byte alpha)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = alpha;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct with the specified component values.
        /// </summary>
        /// <param name="red">The amount of red.</param>
        /// <param name="green">The amount of green.</param>
        /// <param name="blue">The amount of blue.</param>
        /// <param name="alpha">The opacity amount.</param>
        public Color(byte red, byte green, byte blue, byte alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Color"/> are the same as
        /// the components of the second specified <see cref="Color"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Color"/> to compare.</param>
        /// <param name="b">The second <see cref="Color"/> to compare.</param>
        /// <returns><c>true</c>, if the components of both <see cref="Color"/> are the same.</returns>
        public static bool operator ==(Color a, Color b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Color"/> are not the same
        /// as the components of the second specified<see cref="Color"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Color"/> to compare.</param>
        /// <param name="b">The second <see cref="Color"/> to compare.</param>
        /// <returns><c>true</c>, if the components of both <see cref="Color"/> are not the same.</returns>
        public static bool operator !=(Color a, Color b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="ColorF"/>
        /// </summary>
        /// <param name="color">The <see cref="ColorF"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Color"/>.</returns>
        public static explicit operator Color(ColorF color)
        {
            return new Color((byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255),
                (byte)(color.A * 255));
        }
        
        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="Color"/> are the same as the components
        /// of the second specified <see cref="Color"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Color"/>.</param>
        /// <returns><c>true</c>, if the components of both <see cref="Color"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Color))
            {
                return false;
            }
            Color color = (Color)obj;
            return Equals(ref color);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 373;
                hash *= 541 + R.GetHashCode();
                hash *= 541 + G.GetHashCode();
                hash *= 541 + B.GetHashCode();
                hash *= 541 + A.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Color"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Color"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{R={0},G={1},B={2},A={3}}}", R, G, B, A);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Color"/> is equal to another <see cref="Color"/>.
        /// </summary>
        /// <param name="other">A <see cref="Color"/> to compare with this <see cref="Color"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Color"/> is equal to the other parameter; otherwise
        /// <c>false</c>.</returns>
        public bool Equals(Color other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Color"/> is equal to another <see cref="Color"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Color"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Color"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Color other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }
    }
}
