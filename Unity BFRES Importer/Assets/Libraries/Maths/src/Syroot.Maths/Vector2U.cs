using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a two-dimensional vector which uses unsigned integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2U : IEquatable<Vector2U>, IEquatableByRef<Vector2U>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector2U"/> with the X and Y components being 0.
        /// </summary>
        public static readonly Vector2U Zero = new Vector2U();

        /// <summary>
        /// A <see cref="Vector2U"/> with the X and Y components being 1.
        /// </summary>
        public static readonly Vector2U One = new Vector2U(1, 1);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 2;

        /// <summary>
        /// Gets the size of this structure.
        /// </summary>
        public const int SizeInBytes = ValueCount * sizeof(uint);

        // ---- MEMBERS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// The X unsigned integer component.
        /// </summary>
        public uint X;

        /// <summary>
        /// The Y unsigned integer component.
        /// </summary>
        public uint Y;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2U"/> structure with the given values for the X and Y
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        public Vector2U(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the given <see cref="Vector2U"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector2U"/>.</param>
        /// <returns>The result.</returns>
        public static Vector2U operator +(Vector2U a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector2U"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2U"/>.</param>
        /// <param name="b">The second <see cref="Vector2U"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector2U operator +(Vector2U a, Vector2U b)
        {
            return new Vector2U(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Subtracts the first <see cref="Vector2U"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2U"/>.</param>
        /// <param name="b">The second <see cref="Vector2U"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector2U operator -(Vector2U a, Vector2U b)
        {
            return new Vector2U(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector2U"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector2U"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector2U operator *(Vector2U a, float s)
        {
            return new Vector2U((uint)(a.X * s), (uint)(a.Y * s));
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector2U"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2U"/>.</param>
        /// <param name="b">The second <see cref="Vector2U"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector2U operator *(Vector2U a, Vector2U b)
        {
            return new Vector2U(a.X * b.X, a.Y * b.Y);
        }

        /// <summary>
        /// Divides the given <see cref="Vector2U"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector2U"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector2U operator /(Vector2U a, float s)
        {
            return new Vector2U((uint)(a.X / s), (uint)(a.Y / s));
        }

        /// <summary>
        /// Divides the first <see cref="Vector2U"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2U"/>.</param>
        /// <param name="b">The second <see cref="Vector2U"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector2U operator /(Vector2U a, Vector2U b)
        {
            return new Vector2U(a.X / b.X, a.Y / b.Y);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector2U"/> are the same as
        /// the components of the second specified <see cref="Vector2U"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2U"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector2U"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector2U"/> are the same.</returns>
        public static bool operator ==(Vector2U a, Vector2U b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector2U"/> are not the same
        /// as the components of the second specified <see cref="Vector2"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2U"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector2U"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector2U"/> are not the same.</returns>
        public static bool operator !=(Vector2U a, Vector2U b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector2"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector2"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector2U"/>.</returns>
        public static explicit operator Vector2U(Vector2 vector)
        {
            return new Vector2U((uint)vector.X, (uint)vector.Y);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector2F"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector2F"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector2U"/>.</returns>
        public static explicit operator Vector2U(Vector2F vector)
        {
            return new Vector2U((uint)vector.X, (uint)vector.Y);
        }

        /// <summary>
        /// Gets or sets the component at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the component.</param>
        /// <returns>The value of the component.</returns>
        public uint this[int index]
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
        /// Gets a value indicating whether the components of this <see cref="Vector2U"/> are the same as the components
        /// of the second specified <see cref="Vector2U"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector2U"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector2U"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2U))
            {
                return false;
            }
            Vector2U vector2U = (Vector2U)obj;
            return Equals(ref vector2U);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 1531;
                hash *= 1319 + (int)X;
                hash *= 1319 + (int)Y;
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector2U"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector2U"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1}}}", X, Y);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector2U"/> is equal to another <see cref="Vector2U"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector2U"/> to compare with this <see cref="Vector2U"/>.</param>
        /// <returns>true if the current <see cref="Vector2U"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector2U other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector2U"/> is equal to another <see cref="Vector2U"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector2U"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector2U"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector2U other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Returns a value indicating whether the <see cref="Vector2U"/> lies within the provided
        /// <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> tested for intersection.</param>
        /// <returns><c>true</c> when the <see cref="Vector2U"/> lies within the rectangle, otherwise <c>false</c>.
        /// </returns>
        public bool Intersects(Rectangle rectangle)
        {
            return X >= rectangle.X && X < rectangle.X + rectangle.Width
                && Y >= rectangle.Y && Y < rectangle.Y + rectangle.Height;
        }
    }
}
