using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a rectangle specifying a position and size which uses float values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct RectangleF : IEquatable<RectangleF>, IEquatableByRef<RectangleF>, INearlyEquatable<RectangleF>,
        INearlyEquatableByRef<RectangleF>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="RectangleF"/> at position 0f, 0f and a width and height of 0f.
        /// </summary>
        public static readonly RectangleF Zero = new RectangleF();

        /// <summary>
        /// A <see cref="RectangleF"/> at position 0f, 0f and a width and height of 1f.
        /// </summary>
        public static readonly RectangleF One = new RectangleF(0f, 0f, 1f, 1f);

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
        /// The X float component.
        /// </summary>
        public float X;

        /// <summary>
        /// The Y float component.
        /// </summary>
        public float Y;

        /// <summary>
        /// The float width of the rectangle.
        /// </summary>
        public float Width;

        /// <summary>
        /// The float height of the rectangle.
        /// </summary>
        public float Height;
        
        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF"/> structure with the given values for the position
        /// and size components.
        /// </summary>
        /// <param name="x">The value of the X position component.</param>
        /// <param name="y">The value of the Y position component.</param>
        /// <param name="width">The value of the width size component.</param>
        /// <param name="height">The value of the height size component.</param>
        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF"/> structure with the given values for the position
        /// and size components.
        /// </summary>
        /// <param name="position">The values of the position components.</param>
        /// <param name="size">The values of the size components.</param>
        public RectangleF(Vector2F position, Vector2F size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the positional <see cref="Vector2F"/> of this rectangle, representing the <see cref="X"/> and
        /// <see cref="Y"/> members.
        /// </summary>
        public Vector2F Position
        {
            get
            {
                return new Vector2F(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the sizing <see cref="Vector2F"/> of this rectangle, representing the <see cref="Width"/> and
        /// <see cref="Height"/> members.
        /// </summary>
        public Vector2F Size
        {
            get
            {
                return new Vector2F(Width, Height);
            }
            set
            {
                Width = value.X;
                Height = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the end coordinate on the X axis.
        /// </summary>
        public float X2
        {
            get
            {
                return X + Width;
            }
            set
            {
                Width = value - X;
            }
        }

        /// <summary>
        /// Gets or sets the end coordinate on the Y axis.
        /// </summary>
        public float Y2
        {
            get
            {
                return Y + Height;
            }
            set
            {
                Height = value - Y;
            }
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="RectangleF"/> are the same
        /// as the components of the second specified <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="a">The first <see cref="RectangleF"/> to compare.</param>
        /// <param name="b">The second <see cref="RectangleF"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="RectangleF"/> are the same.</returns>
        public static bool operator ==(RectangleF a, RectangleF b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="RectangleF"/> are not the
        /// same as the components of the second specified <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="a">The first <see cref="RectangleF"/> to compare.</param>
        /// <param name="b">The second <see cref="RectangleF"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="RectangleF"/> are not the same.</returns>
        public static bool operator !=(RectangleF a, RectangleF b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to convert from.</param>
        /// <returns>The retrieved <see cref="RectangleF"/>.</returns>
        public static implicit operator RectangleF(Rectangle rectangle)
        {
            return new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
        
        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="RectangleF"/> are the same as the
        /// components of the second specified <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="RectangleF"/>.</param>
        /// <returns>true, if the components of both <see cref="RectangleF"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            RectangleF? rectangle = obj as RectangleF?;
            if (rectangle.HasValue)
            {
                return rectangle == this;
            }
            return false;
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
                hash *= 31 + X.GetHashCode();
                hash *= 31 + Y.GetHashCode();
                hash *= 31 + Width.GetHashCode();
                hash *= 31 + Height.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="RectangleF"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="RectangleF"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Width={2},Height={3}}}", X, Y, Width,
                Height);
        }

        /// <summary>
        /// Indicates whether the current <see cref="RectangleF"/> is equal to another <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="other">A <see cref="RectangleF"/> to compare with this <see cref="RectangleF"/>.</param>
        /// <returns>true if the current <see cref="RectangleF"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(RectangleF other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="RectangleF"/> is equal to another <see cref="RectangleF"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="RectangleF"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="RectangleF"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref RectangleF other)
        {
            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
        }

        /// <summary>
        /// Indicates whether the current <see cref="RectangleF"/> is nearly equal to another <see cref="RectangleF"/>.
        /// </summary>
        /// <param name="other">A <see cref="RectangleF"/> to compare with this <see cref="RectangleF"/>.</param>
        /// <returns>true if the current <see cref="RectangleF"/> is nearly equal to the other parameter; otherwise,
        /// false.</returns>
        public bool NearlyEquals(RectangleF other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="RectangleF"/> is nearly equal to another <see cref="RectangleF"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="RectangleF"/> to compare with this <see cref="RectangleF"/>.</param>
        /// <returns><c>true</c> if the current <see cref="RectangleF"/> is nearly equal to the other parameter;
        /// otherwise, <c>false</c>.</returns>
        public bool NearlyEquals(ref RectangleF other)
        {
            return X.NearlyEquals(other.X) && Y.NearlyEquals(other.Y) && Width.NearlyEquals(other.Width)
                && Height.NearlyEquals(other.Height);
        }
    }
}
