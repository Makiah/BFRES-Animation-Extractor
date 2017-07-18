using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a three-dimensional vector which uses unsigned integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector3U : IEquatable<Vector3U>, IEquatableByRef<Vector3U>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector3U"/> with the X, Y and Z components being 0.
        /// </summary>
        public static readonly Vector3U Zero = new Vector3U();

        /// <summary>
        /// A <see cref="Vector3U"/> with the X, Y and Z components being 1.
        /// </summary>
        public static readonly Vector3U One = new Vector3U(1, 1, 1);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 3;

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

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3U"/> structure with the given values for the X, Y and Z
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        public Vector3U(uint x, uint y, uint z)
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
        /// Returns the given <see cref="Vector3U"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector3U"/>.</param>
        /// <returns>The result.</returns>
        public static Vector3U operator +(Vector3U a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector3U"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3U"/>.</param>
        /// <param name="b">The second <see cref="Vector3U"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector3U operator +(Vector3U a, Vector3U b)
        {
            return new Vector3U(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Subtracts the first <see cref="Vector3U"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3U"/>.</param>
        /// <param name="b">The second <see cref="Vector3U"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector3U operator -(Vector3U a, Vector3U b)
        {
            return new Vector3U(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector3U"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector3U"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector3U operator *(Vector3U a, float s)
        {
            return new Vector3U((uint)(a.X * s), (uint)(a.Y * s), (uint)(a.Z * s));
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector3U"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3U"/>.</param>
        /// <param name="b">The second <see cref="Vector3U"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector3U operator *(Vector3U a, Vector3U b)
        {
            return new Vector3U(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        /// <summary>
        /// Divides the given <see cref="Vector3U"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector3U"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector3U operator /(Vector3U a, float s)
        {
            return new Vector3U((uint)(a.X / s), (uint)(a.Y / s), (uint)(a.Z / s));
        }

        /// <summary>
        /// Divides the first <see cref="Vector3U"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3U"/>.</param>
        /// <param name="b">The second <see cref="Vector3U"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector3U operator /(Vector3U a, Vector3U b)
        {
            return new Vector3U(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector3U"/> are the same as
        /// the components of the second specified <see cref="Vector3U"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3U"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector3U"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector3U"/> are the same.</returns>
        public static bool operator ==(Vector3U a, Vector3U b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector3U"/> are not the
        /// same as the components of the second specified <see cref="Vector3U"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3U"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector3U"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector3U"/> are not the same.</returns>
        public static bool operator !=(Vector3U a, Vector3U b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector3"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector3"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector3U"/>.</returns>
        public static explicit operator Vector3U(Vector3 vector)
        {
            return new Vector3U((uint)vector.X, (uint)vector.Y, (uint)vector.Z);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector3F"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector3F"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector3U"/>.</returns>
        public static explicit operator Vector3U(Vector3F vector)
        {
            return new Vector3U((uint)vector.X, (uint)vector.Y, (uint)vector.Z);
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
        /// Gets a value indicating whether the components of this <see cref="Vector3U"/> are the same as the components
        /// of the second specified <see cref="Vector3U"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector3U"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector3U"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector3U))
            {
                return false;
            }
            Vector3U vector3U = (Vector3U)obj;
            return Equals(ref vector3U);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 4673;
                hash *= 251 + (int)X;
                hash *= 251 + (int)Y;
                hash *= 251 + (int)Z;
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector3U"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector3U"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Z={2}}}", X, Y, Z);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector3U"/> is equal to another <see cref="Vector3U"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector3U"/> to compare with this <see cref="Vector3U"/>.</param>
        /// <returns>true if the current <see cref="Vector3U"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector3U other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector3U"/> is equal to another <see cref="Vector3U"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector3U"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector3U"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector3U other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>
        /// Gets the angle between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3U"/>.</param>
        /// <returns>The angle between this and the other vector.</returns>
        public float Angle(Vector3U vector)
        {
            return Angle(ref vector);
        }

        /// <summary>
        /// Gets the angle between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3U"/>.</param>
        /// <returns>The angle between this and the other vector.</returns>
        public float Angle(ref Vector3U vector)
        {
            return (float)Math.Acos(Dot(vector) / (Length * vector.Length));
        }

        /// <summary>
        /// Gets the cross product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3U"/>.</param>
        /// <returns>The cross product between this and the other vector.</returns>
        public Vector3U Cross(Vector3U vector)
        {
            return Cross(ref vector);
        }

        /// <summary>
        /// Gets the cross product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3U"/>.</param>
        /// <returns>The cross product between this and the other vector.</returns>
        public Vector3U Cross(ref Vector3U vector)
        {
            return new Vector3U(
                Y * vector.Z - Z * vector.Y,
                Z * vector.X - X * vector.Z,
                X * vector.Y - Y * vector.X);
        }

        /// <summary>
        /// Gets the dot product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3U"/>.</param>
        /// <returns>The dot product between this and the other vector.</returns>
        public float Dot(Vector3U vector)
        {
            return Dot(ref vector);
        }

        /// <summary>
        /// Gets the dot product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3U"/>.</param>
        /// <returns>The dot product between this and the other vector.</returns>
        public float Dot(ref Vector3U vector)
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
        public Vector3U Normalized()
        {
            return this / Length;
        }
    }
}
