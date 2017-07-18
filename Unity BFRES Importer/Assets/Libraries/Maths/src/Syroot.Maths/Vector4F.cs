using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a four-dimensional vector which uses float values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector4F : IEquatable<Vector4F>, IEquatableByRef<Vector4F>, INearlyEquatable<Vector4F>,
        INearlyEquatableByRef<Vector4F>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector4F"/> with the X, Y, Z and W components being 0f.
        /// </summary>
        public static readonly Vector4F Zero = new Vector4F();

        /// <summary>
        /// A <see cref="Vector4F"/> with the X, Y, Z and W components being 1f.
        /// </summary>
        public static readonly Vector4F One = new Vector4F(1f, 1f, 1f, 1f);

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
        /// The Z float component.
        /// </summary>
        public float Z;

        /// <summary>
        /// The W float component.
        /// </summary>
        public float W;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4F"/> struct with the given values for the X, Y, Z and W
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        /// <param name="w">The value of the W component.</param>
        public Vector4F(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the given <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector4F"/>.</param>
        /// <returns>The result.</returns>
        public static Vector4F operator +(Vector4F a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector4F"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4F"/>.</param>
        /// <param name="b">The second <see cref="Vector4F"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector4F operator +(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        /// <summary>
        /// Negates the given <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector4F"/> to negate.</param>
        /// <returns>The negated result.</returns>
        public static Vector4F operator -(Vector4F a)
        {
            return new Vector4F(-a.X, -a.Y, -a.Z, -a.W);
        }

        /// <summary>
        /// Subtracts the first <see cref="Vector4F"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4F"/>.</param>
        /// <param name="b">The second <see cref="Vector4F"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector4F operator -(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector4F"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector4F"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector4F operator *(Vector4F a, float s)
        {
            return new Vector4F(a.X * s, a.Y * s, a.Z * s, a.W * s);
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector4F"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4F"/>.</param>
        /// <param name="b">The second <see cref="Vector4F"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector4F operator *(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        }

        /// <summary>
        /// Divides the given <see cref="Vector4F"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector4F"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector4F operator /(Vector4F a, float s)
        {
            return new Vector4F(a.X / s, a.Y / s, a.Z / s, a.W / s);
        }

        /// <summary>
        /// Divides the first <see cref="Vector4F"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4F"/>.</param>
        /// <param name="b">The second <see cref="Vector4F"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector4F operator /(Vector4F a, Vector4F b)
        {
            return new Vector4F(a.X / b.X, a.Y / b.Y, a.Z / b.Z, a.W / b.W);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4F"/> are the same as
        /// the components of the second specified <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4F"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4F"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4F"/> are the same.</returns>
        public static bool operator ==(Vector4F a, Vector4F b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4F"/> are not the
        /// same as the components of the second specified <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4F"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4F"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4F"/> are not the same.</returns>
        public static bool operator !=(Vector4F a, Vector4F b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Vector4"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector4F"/>.</returns>
        public static implicit operator Vector4F(Vector4 vector)
        {
            return new Vector4F(vector.X, vector.Y, vector.Z, vector.W);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Vector4U"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector4U"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector4F"/>.</returns>
        public static implicit operator Vector4F(Vector4U vector)
        {
            return new Vector4F(vector.X, vector.Y, vector.Z, vector.W);
        }

        /// <summary>
        /// Gets or sets the component at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the component.</param>
        /// <returns>The value of the component.</returns>
        public float this[int index]
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
        /// Gets a value indicating whether the components of this <see cref="Vector4F"/> are the same as the components
        /// of the second specified <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector4F"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector4F"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector4F))
            {
                return false;
            }
            Vector4F vector4F = (Vector4F)obj;
            return Equals(ref vector4F);
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
                hash *= 499 + X.GetHashCode();
                hash *= 499 + Y.GetHashCode();
                hash *= 499 + Z.GetHashCode();
                hash *= 499 + W.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector4F"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector4F"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Z={2},W={3}}}", X, Y, Z, W);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4F"/> is equal to another <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector4F"/> to compare with this <see cref="Vector4F"/>.</param>
        /// <returns>true if the current <see cref="Vector4F"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector4F other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4F"/> is equal to another <see cref="Vector4F"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector4F"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector4F"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector4F other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4F"/> is nearly equal to another <see cref="Vector4F"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector4F"/> to compare with this <see cref="Vector4F"/>.</param>
        /// <returns>true if the current <see cref="Vector4F"/> is nearly equal to the other parameter; otherwise,
        /// false.</returns>
        public bool NearlyEquals(Vector4F other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4F"/> is nearly equal to another <see cref="Vector4F"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector4F"/> to compare with this <see cref="Vector4F"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Vector4F"/> is nearly equal to the other parameter;
        /// otherwise, <c>false</c>.</returns>
        public bool NearlyEquals(ref Vector4F other)
        {
            return X.NearlyEquals(other.X) && Y.NearlyEquals(other.Y) && Z.NearlyEquals(other.Z)
                && W.NearlyEquals(other.W);
        }
    }
}