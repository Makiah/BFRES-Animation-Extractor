using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a three-dimensional vector which uses float values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector3F : IEquatable<Vector3F>, IEquatableByRef<Vector3F>, INearlyEquatable<Vector3F>,
        INearlyEquatableByRef<Vector3F>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector3F"/> with the X, Y and Z components being 0f.
        /// </summary>
        public static readonly Vector3F Zero = new Vector3F();

        /// <summary>
        /// A <see cref="Vector3F"/> with the X, Y and Z components being 1f.
        /// </summary>
        public static readonly Vector3F One = new Vector3F(1f, 1f, 1f);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 3;

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

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3F"/> struct with the given values for the X, Y and Z
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        public Vector3F(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the length of this vector.
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Returns the given <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector3F"/>.</param>
        /// <returns>The result.</returns>
        public static Vector3F operator +(Vector3F a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector3F"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3F"/>.</param>
        /// <param name="b">The second <see cref="Vector3F"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector3F operator +(Vector3F a, Vector3F b)
        {
            return new Vector3F(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Negates the given <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector3F"/> to negate.</param>
        /// <returns>The negated result.</returns>
        public static Vector3F operator -(Vector3F a)
        {
            return new Vector3F(-a.X, -a.Y, -a.Z);
        }

        /// <summary>
        /// Subtracts the first <see cref="Vector3F"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3F"/>.</param>
        /// <param name="b">The second <see cref="Vector3F"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector3F operator -(Vector3F a, Vector3F b)
        {
            return new Vector3F(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector3F"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector3F"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector3F operator *(Vector3F a, float s)
        {
            return new Vector3F(a.X * s, a.Y * s, a.Z * s);
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector3F"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3F"/>.</param>
        /// <param name="b">The second <see cref="Vector3F"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector3F operator *(Vector3F a, Vector3F b)
        {
            return new Vector3F(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        /// <summary>
        /// Divides the given <see cref="Vector3F"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector3F"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector3F operator /(Vector3F a, float s)
        {
            return new Vector3F(a.X / s, a.Y / s, a.Z / s);
        }

        /// <summary>
        /// Divides the first <see cref="Vector3F"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3F"/>.</param>
        /// <param name="b">The second <see cref="Vector3F"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector3F operator /(Vector3F a, Vector3F b)
        {
            return new Vector3F(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector3F"/> are the same as
        /// the components of the second specified <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3F"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector3F"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector3F"/> are the same.</returns>
        public static bool operator ==(Vector3F a, Vector3F b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector3F"/> are not the
        /// same as the components of the second specified <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3F"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector3F"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector3F"/> are not the same.</returns>
        public static bool operator !=(Vector3F a, Vector3F b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector3F"/>.</returns>
        public static implicit operator Vector3F(Vector3 vector)
        {
            return new Vector3F(vector.X, vector.Y, vector.Z);
        }

        /// <summary>
        /// Implicit conversion from <see cref="Vector3U"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector3U"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector3F"/>.</returns>
        public static implicit operator Vector3F(Vector3U vector)
        {
            return new Vector3F(vector.X, vector.Y, vector.Z);
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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index),
                            $"Index must be between 0 and {ValueCount}.");
                }
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="Vector3F"/> are the same as the components
        /// of the second specified <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector3F"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector3F"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector3F))
            {
                return false;
            }
            Vector3F vector3F = (Vector3F)obj;
            return Equals(ref vector3F);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 263;
                hash *= 383 + X.GetHashCode();
                hash *= 383 + Y.GetHashCode();
                hash *= 383 + Z.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector3F"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector3F"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Z={2}}}", X, Y, Z);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector3F"/> is equal to another <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector3F"/> to compare with this <see cref="Vector3F"/>.</param>
        /// <returns>true if the current <see cref="Vector3F"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector3F other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector3F"/> is equal to another <see cref="Vector3F"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector3F"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector3F"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector3F other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector3F"/> is nearly equal to another <see cref="Vector3F"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector3F"/> to compare with this <see cref="Vector3F"/>.</param>
        /// <returns>true if the current <see cref="Vector3F"/> is nearly equal to the other parameter; otherwise,
        /// false.</returns>
        public bool NearlyEquals(Vector3F other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector3F"/> is nearly equal to another <see cref="Vector3F"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector3F"/> to compare with this <see cref="Vector3F"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Vector3F"/> is nearly equal to the other parameter;
        /// otherwise, <c>false</c>.</returns>
        public bool NearlyEquals(ref Vector3F other)
        {
            return X.NearlyEquals(other.X) && Y.NearlyEquals(other.Y) && Z.NearlyEquals(other.Z);
        }

        /// <summary>
        /// Gets the angle between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3F"/>.</param>
        /// <returns>The angle between this and the other vector.</returns>
        public float Angle(Vector3F vector)
        {
            return Angle(ref vector);
        }

        /// <summary>
        /// Gets the angle between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3F"/>.</param>
        /// <returns>The angle between this and the other vector.</returns>
        public float Angle(ref Vector3F vector)
        {
            return (float)Math.Acos(Dot(vector) / (Length * vector.Length));
        }

        /// <summary>
        /// Gets the cross product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3F"/>.</param>
        /// <returns>The cross product between this and the other vector.</returns>
        public Vector3F Cross(Vector3F vector)
        {
            return Cross(ref vector);
        }

        /// <summary>
        /// Gets the cross product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3F"/>.</param>
        /// <returns>The cross product between this and the other vector.</returns>
        public Vector3F Cross(ref Vector3F vector)
        {
            return new Vector3F(
                Y * vector.Z - Z * vector.Y,
                Z * vector.X - X * vector.Z,
                X * vector.Y - Y * vector.X);
        }

        /// <summary>
        /// Gets the dot product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3F"/>.</param>
        /// <returns>The dot product between this and the other vector.</returns>
        public float Dot(Vector3F vector)
        {
            return Dot(ref vector);
        }

        /// <summary>
        /// Gets the dot product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3F"/>.</param>
        /// <returns>The dot product between this and the other vector.</returns>
        public float Dot(ref Vector3F vector)
        {
            return X * vector.X + Y * vector.Y + Z * vector.Z;
        }

        /// <summary>
        /// Normalizes the length of this vector.
        /// </summary>
        public void Normalize()
        {
            this /= Length;
        }

        /// <summary>
        /// Returns a normalized version of this vector.
        /// </summary>
        public Vector3F Normalized()
        {
            return this / Length;
        }
    }
}
