using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a three-dimensional vector which uses integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector3 : IEquatable<Vector3>, IEquatableByRef<Vector3>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector3"/> with the X, Y and Z components being 0.
        /// </summary>
        public static readonly Vector3 Zero = new Vector3();

        /// <summary>
        /// A <see cref="Vector3"/> with the X, Y and Z components being 1.
        /// </summary>
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 3;

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

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> structure with the given values for the X, Y and Z
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        public Vector3(int x, int y, int z)
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
        /// Returns the given <see cref="Vector3"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/>.</param>
        /// <returns>The result.</returns>
        public static Vector3 operator +(Vector3 a)
        {
            return a;
        }

        /// <summary>
        /// Adds the first <see cref="Vector3"/> to the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/>.</param>
        /// <param name="b">The second <see cref="Vector3"/>.</param>
        /// <returns>The addition result.</returns>
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        /// <summary>
        /// Negates the given <see cref="Vector3"/>.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/> to negate.</param>
        /// <returns>The negated result.</returns>
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.X, -a.Y, -a.Z);
        }

        /// <summary>
        /// Subtracts the first <see cref="Vector3"/> from the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/>.</param>
        /// <param name="b">The second <see cref="Vector3"/>.</param>
        /// <returns>The subtraction result.</returns>
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Multiplicates the given <see cref="Vector3"/> by the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector3 operator *(Vector3 a, float s)
        {
            return new Vector3((int)(a.X * s), (int)(a.Y * s), (int)(a.Z * s));
        }

        /// <summary>
        /// Multiplicates the first <see cref="Vector3"/> by the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/>.</param>
        /// <param name="b">The second <see cref="Vector3"/>.</param>
        /// <returns>The multiplication result.</returns>
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        /// <summary>
        /// Divides the given <see cref="Vector3"/> through the scalar.
        /// </summary>
        /// <param name="a">The <see cref="Vector3"/>.</param>
        /// <param name="s">The scalar.</param>
        /// <returns>The division result.</returns>
        public static Vector3 operator /(Vector3 a, float s)
        {
            return new Vector3((int)(a.X / s), (int)(a.Y / s), (int)(a.Z / s));
        }

        /// <summary>
        /// Divides the first <see cref="Vector3"/> through the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/>.</param>
        /// <param name="b">The second <see cref="Vector3"/>.</param>
        /// <returns>The division result.</returns>
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector3"/> are the same as
        /// the components of the second specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector3"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector3"/> are the same.</returns>
        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector3"/> are not the same
        /// as the components of the second specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector3"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector3"/> are not the same.</returns>
        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !a.Equals(ref b);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector3F"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector3F"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector3"/>.</returns>
        public static explicit operator Vector3(Vector3F vector)
        {
            return new Vector3((int)vector.X, (int)vector.Y, (int)vector.Z);
        }

        /// <summary>
        /// Explicit conversion from <see cref="Vector3U"/>
        /// </summary>
        /// <param name="vector">The <see cref="Vector3U"/> to convert from.</param>
        /// <returns>The retrieved <see cref="Vector3"/>.</returns>
        public static explicit operator Vector3(Vector3U vector)
        {
            return new Vector3((int)vector.X, (int)vector.Y, (int)vector.Z);
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
        /// Gets a value indicating whether the components of this <see cref="Vector3"/> are the same as the components
        /// of the second specified <see cref="Vector3"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector3"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector3"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector3))
            {
                return false;
            }
            Vector3 vector3 = (Vector3)obj;
            return Equals(ref vector3);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 131;
                hash *= 521 + X;
                hash *= 521 + Y;
                hash *= 521 + Z;
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector3"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector3"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Z={2}}}", X, Y, Z);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector3"/> is equal to another <see cref="Vector3"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector3"/> to compare with this <see cref="Vector3"/>.</param>
        /// <returns>true if the current <see cref="Vector3"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector3 other)
        {
            return Equals(ref other);
        }
        
        /// <summary>
        /// Indicates whether the current <see cref="Vector3"/> is equal to another <see cref="Vector3"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector3"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector3"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>
        /// Gets the angle between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3"/>.</param>
        /// <returns>The angle between this and the other vector.</returns>
        public float Angle(Vector3 vector)
        {
            return Angle(ref vector);
        }

        /// <summary>
        /// Gets the angle between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3"/>.</param>
        /// <returns>The angle between this and the other vector.</returns>
        public float Angle(ref Vector3 vector)
        {
            return (float)Math.Acos(Dot(vector) / (Length * vector.Length));
        }

        /// <summary>
        /// Gets the cross product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3"/>.</param>
        /// <returns>The cross product between this and the other vector.</returns>
        public Vector3 Cross(Vector3 vector)
        {
            return Cross(ref vector);
        }

        /// <summary>
        /// Gets the cross product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3"/>.</param>
        /// <returns>The cross product between this and the other vector.</returns>
        public Vector3 Cross(ref Vector3 vector)
        {
            return new Vector3(
                Y * vector.Z - Z * vector.Y,
                Z * vector.X - X * vector.Z,
                X * vector.Y - Y * vector.X);
        }

        /// <summary>
        /// Gets the dot product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3"/>.</param>
        /// <returns>The dot product between this and the other vector.</returns>
        public float Dot(Vector3 vector)
        {
            return Dot(ref vector);
        }

        /// <summary>
        /// Gets the dot product between this and the other <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The other <see cref="Vector3"/>.</param>
        /// <returns>The dot product between this and the other vector.</returns>
        public float Dot(ref Vector3 vector)
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
        public Vector3 Normalized()
        {
            return this / Length;
        }
    }
}
