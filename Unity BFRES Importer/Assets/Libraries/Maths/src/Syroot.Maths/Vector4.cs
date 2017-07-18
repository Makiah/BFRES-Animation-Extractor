using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a four-dimensional vector which uses integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector4 : IEquatable<Vector4>, IEquatableByRef<Vector4>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector4"/> with the X, Y, Z and W components being 0.
        /// </summary>
        public static readonly Vector4 Zero = new Vector4();

        /// <summary>
        /// A <see cref="Vector4"/> with the X, Y, Z and W components being 1.
        /// </summary>
        public static readonly Vector4 One = new Vector4(1, 1, 1, 1);

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
        /// The Z integer component.
        /// </summary>
        public int Z;

        /// <summary>
        /// The W integer component.
        /// </summary>
        public int W;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4"/> struct with the given values for the X, Y, Z and W
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        /// <param name="w">The value of the W component.</param>
        public Vector4(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the given <see cref="Vector4"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector4"/>.</param>
        /// <returns>The result.</returns>
        public static Vector4 operator +(Vector4 a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector4"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4"/>.</param>
        /// <param name="b">The second <see cref="Vector4"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        /// <summary>
        /// Negates the given <see cref="Vector4"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector4"/> to negate.</param>
        /// <returns>The negated result.</returns>
        public static Vector4 operator -(Vector4 a)
        {
            return new Vector4(-a.X, -a.Y, -a.Z, -a.W);
        }

        /// <summary>
        /// Subtracts the first <see cref="Vector4"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4"/>.</param>
        /// <param name="b">The second <see cref="Vector4"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector4"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector4"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector4 operator *(Vector4 a, float s)
        {
            return new Vector4((int)(a.X * s), (int)(a.Y * s), (int)(a.Z * s), (int)(a.W * s));
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector4"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4"/>.</param>
        /// <param name="b">The second <see cref="Vector4"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        /// <summary>
        /// Divides the given <see cref="Vector4"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector4"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector4 operator /(Vector4 a, float s)
        {
            return new Vector4((int)(a.X / s), (int)(a.Y / s), (int)(a.Z / s), (int)(a.W / s));
        }

        /// <summary>
        /// Divides the first <see cref="Vector4"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4"/>.</param>
        /// <param name="b">The second <see cref="Vector4"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector4 operator /(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4"/> are the same as
        /// the components of the second specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4"/> are the same.</returns>
        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4"/> are not the
        /// same as the components of the second specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4"/> are not the same.</returns>
        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector4F"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector4F"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector4"/>.</returns>
        public static explicit operator Vector4(Vector4F vector)
        {
            return new Vector4((int)vector.X, (int)vector.Y, (int)vector.Z, (int)vector.W);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector4U"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector4U"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector4"/>.</returns>
        public static explicit operator Vector4(Vector4U vector)
        {
            return new Vector4((int)vector.X, (int)vector.Y, (int)vector.Z, (int)vector.W);
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
        /// Gets a value indicating whether the components of this <see cref="Vector4"/> are the same as the components
        /// of the second specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector4"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector4"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector4))
            {
                return false;
            }
            Vector4 vector4 = (Vector4)obj;
            return Equals(ref vector4);
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
        /// Gets a string describing the components of this <see cref="Vector4"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector4"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Z={2},W={3}}}", X, Y, Z, W);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4"/> is equal to another <see cref="Vector4"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector4"/> to compare with this <see cref="Vector4"/>.</param>
        /// <returns>true if the current <see cref="Vector4"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector4 other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4"/> is equal to another <see cref="Vector4"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector4"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector4"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector4 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }
    }
}