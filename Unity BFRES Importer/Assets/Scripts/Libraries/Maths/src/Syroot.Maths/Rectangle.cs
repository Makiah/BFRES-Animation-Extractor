using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a rectangle specifying a position and size which uses integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Rectangle : IEquatable<Rectangle>, IEquatableByRef<Rectangle>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Rectangle"/> at position 0, 0 and a width and height of 0.
        /// </summary>
        public static readonly Rectangle Zero = new Rectangle();

        /// <summary>
        /// A <see cref="Rectangle"/> at position 0, 0 and a width and height of 1.
        /// </summary>
        public static readonly Rectangle One = new Rectangle(0, 0, 1, 1);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 4;

        /// <summary>
        /// Gets the size of this structure.
        /// </summary>
        public const int SizeInBytes = ValueCount * sizeof(int);

        // ---- MEMBERS ------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// The X integer component.
        /// </summary>
        public int X;

        /// <summary>
        /// The Y integer component.
        /// </summary>
        public int Y;

        /// <summary>
        /// The integer width of the rectangle.
        /// </summary>
        public int Width;

        /// <summary>
        /// The integer height of the rectangle.
        /// </summary>
        public int Height;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> structure with the given values for the position
        /// and size components.
        /// </summary>
        /// <param name="x">The value of the X position component.</param>
        /// <param name="y">The value of the Y position component.</param>
        /// <param name="width">The value of the width size component.</param>
        /// <param name="height">The value of the height size component.</param>
        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> structure with the given values for the position
        /// and size components.
        /// </summary>
        /// <param name="position">The values of the position components.</param>
        /// <param name="size">The values of the size components.</param>
        public Rectangle(Vector2 position, Vector2 size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets or sets the positional <see cref="Vector2"/> of this rectangle, representing the <see cref="X"/> and
        /// <see cref="Y"/> members.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the sizing <see cref="Vector2"/> of this rectangle, representing the <see cref="Width"/> and
        /// <see cref="Height"/> members.
        /// </summary>
        public Vector2 Size
        {
            get
            {
                return new Vector2(Width, Height);
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
        public int X2
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
        public int Y2
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
        /// Gets a value indicating whether the components of the first specified <see cref="Rectangle"/> are the same
        /// as the components of the second specified <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Rectangle"/> to compare.</param>
        /// <param name="b">The second <see cref="Rectangle"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Rectangle"/> are the same.</returns>
        public static bool operator ==(Rectangle a, Rectangle b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Rectangle"/> are not the
        /// same as the components of the second specified <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Rectangle"/> to compare.</param>
        /// <param name="b">The second <see cref="Rectangle"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Rectangle"/> are not the same.</returns>
        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="RectangleF"/>
        /// </summary>
        /// <param name="rectangle">The <see cref="RectangleF"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Rectangle"/>.</returns>
        public static explicit operator Rectangle(RectangleF rectangle)
        {
            return new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height);
        }
        
        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="Rectangle"/> are the same as the
        /// components of the second specified <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Rectangle"/>.</param>
        /// <returns>true, if the components of both <see cref="Rectangle"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            Rectangle? rectangleI = obj as Rectangle?;
            if (rectangleI.HasValue)
            {
                return rectangleI == this;
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
        /// Gets a string describing the components of this <see cref="Rectangle"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Rectangle"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Width={2},Height={3}}}", X, Y, Width,
                Height);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Rectangle"/> is equal to another <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="other">A <see cref="Rectangle"/> to compare with this <see cref="Rectangle"/>.</param>
        /// <returns>true if the current <see cref="Rectangle"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Rectangle other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Rectangle"/> is equal to another <see cref="Rectangle"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Rectangle"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Rectangle"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Rectangle other)
        {
            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
        }
    }
}
