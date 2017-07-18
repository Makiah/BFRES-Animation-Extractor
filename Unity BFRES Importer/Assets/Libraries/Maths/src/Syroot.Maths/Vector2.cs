using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a two-dimensional vector which uses integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2 : IEquatable<Vector2>, IEquatableByRef<Vector2>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector2"/> with the X and Y components being 0.
        /// </summary>
        public static readonly Vector2 Zero = new Vector2();

        /// <summary>
        /// A <see cref="Vector2"/> with the X and Y components being 1.
        /// </summary>
        public static readonly Vector2 One = new Vector2(1, 1);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 2;

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

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2"/> structure with the given values for the X and Y
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the given <see cref="Vector2"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector2"/>.</param>
        /// <returns>The result.</returns>
        public static Vector2 operator +(Vector2 a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector2"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/>.</param>
        /// <param name="b">The second <see cref="Vector2"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Negates the given <see cref="Vector2"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector2"/> to negate.</param>
        /// <returns>The negated result.</returns>
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.X, -a.Y);
        }

        /// <summary>
        /// Subtracts the first <see cref="Vector2"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/>.</param>
        /// <param name="b">The second <see cref="Vector2"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector2"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector2"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector2 operator *(Vector2 a, float s)
        {
            return new Vector2((int)(a.X * s), (int)(a.Y * s));
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector2"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/>.</param>
        /// <param name="b">The second <see cref="Vector2"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X * b.X, a.Y * b.Y);
        }

        /// <summary>
        /// Divides the given <see cref="Vector2"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector2"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector2 operator /(Vector2 a, float s)
        {
            return new Vector2((int)(a.X / s), (int)(a.Y / s));
        }

        /// <summary>
        /// Divides the first <see cref="Vector2"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/>.</param>
        /// <param name="b">The second <see cref="Vector2"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X / b.X, a.Y / b.Y);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector2"/> are the same as
        /// the components of the second specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector2"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector2"/> are the same.</returns>
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector2"/> are not the same
        /// as the components of the second specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector2"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector2"/> are not the same.</returns>
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector2F"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector2F"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector2"/>.</returns>
        public static explicit operator Vector2(Vector2F vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector2U"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector2U"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector2"/>.</returns>
        public static explicit operator Vector2(Vector2U vector)
        {
            return new Vector2((int)vector.X, (int)vector.Y);
        }

        /// <summary>
        /// Gets or sets the component at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the component.</param>
        /// <returns>The value of the component.</returns>
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index),
                            $"Index must be between 0 and {ValueCount}.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index),
                            $"Index must be between 0 and {ValueCount}.");
                }
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="Vector2"/> are the same as the components
        /// of the second specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector2"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector2"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
            {
                return false;
            }
            Vector2 vector2 = (Vector2)obj;
            return Equals(ref vector2);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 43;
                hash *= 7 + X;
                hash *= 7 + Y;
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector2"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector2"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1}}}", X, Y);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector2"/> is equal to another <see cref="Vector2"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector2"/> to compare with this <see cref="Vector2"/>.</param>
        /// <returns>true if the current <see cref="Vector2"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector2 other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector2"/> is equal to another <see cref="Vector2"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector2"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector2"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Returns a value indicating whether the <see cref="Vector2"/> lies within the provided
        /// <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> tested for intersection.</param>
        /// <returns><c>true</c> when the <see cref="Vector2"/> lies within the rectangle, otherwise <c>false</c>.
        /// </returns>
        public bool Intersects(Rectangle rectangle)
        {
            return X >= rectangle.X && X < rectangle.X + rectangle.Width
                && Y >= rectangle.Y && Y < rectangle.Y + rectangle.Height;
        }
    }
}
