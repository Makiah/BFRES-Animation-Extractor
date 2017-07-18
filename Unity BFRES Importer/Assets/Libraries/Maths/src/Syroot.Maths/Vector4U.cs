using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a four-dimensional vector which uses unsigned integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector4U : IEquatable<Vector4U>, IEquatableByRef<Vector4U>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector4U"/> with the X, Y, Z and W components being 0.
        /// </summary>
        public static readonly Vector4U Zero = new Vector4U();

        /// <summary>
        /// A <see cref="Vector4U"/> with the X, Y, Z and W components being 1.
        /// </summary>
        public static readonly Vector4U One = new Vector4U(1, 1, 1, 1);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 4;

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

        /// <summary>
        /// The Z unsigned integer component.
        /// </summary>
        public uint Z;

        /// <summary>
        /// The W unsigned integer component.
        /// </summary>
        public uint W;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4U"/> struct with the given values for the X, Y, Z and W
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        /// <param name="w">The value of the W component.</param>
        public Vector4U(uint x, uint y, uint z, uint w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the given <see cref="Vector4U"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector4U"/>.</param>
        /// <returns>The result.</returns>
        public static Vector4U operator +(Vector4U a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector4U"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4U"/>.</param>
        /// <param name="b">The second <see cref="Vector4U"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector4U operator +(Vector4U a, Vector4U b)
        {
            return new Vector4U(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }
        
        /// <summary>
        /// Subtracts the first <see cref="Vector4U"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4U"/>.</param>
        /// <param name="b">The second <see cref="Vector4U"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector4U operator -(Vector4U a, Vector4U b)
        {
            return new Vector4U(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector4U"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector4U"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector4U operator *(Vector4U a, uint s)
        {
            return new Vector4U(a.X * s, a.Y * s, a.Z * s, a.W * s);
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector4U"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4U"/>.</param>
        /// <param name="b">The second <see cref="Vector4U"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector4U operator *(Vector4U a, Vector4U b)
        {
            return new Vector4U(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        /// <summary>
        /// Divides the given <see cref="Vector4U"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector4U"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector4U operator /(Vector4U a, uint s)
        {
            return new Vector4U(a.X / s, a.Y / s, a.Z / s, a.W / s);
        }

        /// <summary>
        /// Divides the first <see cref="Vector4U"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4U"/>.</param>
        /// <param name="b">The second <see cref="Vector4U"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector4U operator /(Vector4U a, Vector4U b)
        {
            return new Vector4U(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4U"/> are the same as
        /// the components of the second specified <see cref="Vector4U"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4U"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4U"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4U"/> are the same.</returns>
        public static bool operator ==(Vector4U a, Vector4U b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4U"/> are not the
        /// same as the components of the second specified <see cref="Vector4U"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4U"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4U"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4U"/> are not the same.</returns>
        public static bool operator !=(Vector4U a, Vector4U b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector4"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector4"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector4U"/>.</returns>
        public static explicit operator Vector4U(Vector4 vector)
        {
            return new Vector4U((uint)vector.X, (uint)vector.Y, (uint)vector.Z, (uint)vector.W);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector4F"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector4F"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector4U"/>.</returns>
        public static explicit operator Vector4U(Vector4F vector)
        {
            return new Vector4U((uint)vector.X, (uint)vector.Y, (uint)vector.Z, (uint)vector.W);
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
                    case 2: return Z;
                    case 3: return W;
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
                    case 2: Z = value; break;
                    case 3: W = value; break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index),
                            $"Index must be between 0 and {ValueCount}.");
                }
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="Vector4U"/> are the same as the components
        /// of the second specified <see cref="Vector4U"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector4U"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector4U"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector4U))
            {
                return false;
            }
            Vector4U vector4U = (Vector4U)obj;
            return Equals(ref vector4U);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 353;
                hash *= 104 + X.GetHashCode();
                hash *= 104 + Y.GetHashCode();
                hash *= 104 + Z.GetHashCode();
                hash *= 104 + W.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector4U"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector4U"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Z={2},W={3}}}", X, Y, Z, W);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4U"/> is equal to another <see cref="Vector4U"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector4U"/> to compare with this <see cref="Vector4U"/>.</param>
        /// <returns>true if the current <see cref="Vector4U"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector4U other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4U"/> is equal to another <see cref="Vector4U"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector4U"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector4U"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector4U other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }
    }
}