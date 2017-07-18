using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a four-dimensional vector which uses integer values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector4Bool : IEquatable<Vector4Bool>, IEquatableByRef<Vector4Bool>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector4"/> with the X, Y, Z and W components being false.
        /// </summary>
        public static readonly Vector4Bool Zero = new Vector4Bool();

        /// <summary>
        /// A <see cref="Vector4"/> with the X, Y, Z and W components being true.
        /// </summary>
        public static readonly Vector4Bool One = new Vector4Bool(true, true, true, true);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 4;

        /// <summary>
        /// Gets the size of this structure.
        /// </summary>
        public const int SizeInBytes = ValueCount * sizeof(bool);

        // ---- MEMBERS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// The X boolean component.
        /// </summary>
        public bool X;

        /// <summary>
        /// The Y boolean component.
        /// </summary>
        public bool Y;

        /// <summary>
        /// The Z boolean component.
        /// </summary>
        public bool Z;

        /// <summary>
        /// The W boolean component.
        /// </summary>
        public bool W;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4Bool"/> struct with the given values for the X, Y, Z and
        /// W components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        /// <param name="z">The value of the Z component.</param>
        /// <param name="w">The value of the W component.</param>
        public Vector4Bool(bool x, bool y, bool z, bool w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4Bool"/> are the same
        /// as the components of the second specified <see cref="Vector4Bool"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4Bool"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4Bool"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4Bool"/> are the same.</returns>
        public static bool operator ==(Vector4Bool a, Vector4Bool b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector4Bool"/> are not the
        /// same as the components of the second specified <see cref="Vector4Bool"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector4Bool"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector4Bool"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector4Bool"/> are not the same.</returns>
        public static bool operator !=(Vector4Bool a, Vector4Bool b)
        {
            return !a.Equals(ref b);
        }
        
        /// <summary>
        /// Gets or sets the component at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the component.</param>
        /// <returns>The value of the component.</returns>
        public bool this[int index]
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
        /// Gets a value indicating whether the components of this <see cref="Vector4Bool"/> are the same as the
        /// components of the second specified <see cref="Vector4Bool"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector4Bool"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector4Bool"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector4Bool))
            {
                return false;
            }
            Vector4Bool vector4Bool = (Vector4Bool)obj;
            return Equals(ref vector4Bool);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < ValueCount; i++)
            {
                if (this[i]) hash &= 1 << i;
            }
            return hash;
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Vector4Bool"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector4Bool"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1},Z={2},W={3}}}", X, Y, Z, W);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4Bool"/> is equal to another <see cref="Vector4Bool"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector4Bool"/> to compare with this <see cref="Vector4Bool"/>.</param>
        /// <returns>true if the current <see cref="Vector4Bool"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector4Bool other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector4Bool"/> is equal to another <see cref="Vector4Bool"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector4Bool"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector4Bool"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector4Bool other)
        {
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }
    }
}