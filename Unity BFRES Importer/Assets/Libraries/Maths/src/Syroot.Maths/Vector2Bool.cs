using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a two-dimensional vector which uses boolean values.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2Bool : IEquatable<Vector2Bool>, IEquatableByRef<Vector2Bool>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Vector2"/> with the X and Y components being false.
        /// </summary>
        public static readonly Vector2Bool Zero = new Vector2Bool();

        /// <summary>
        /// A <see cref="Vector2"/> with the X and Y components being true.
        /// </summary>
        public static readonly Vector2Bool One = new Vector2Bool(true, true);

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = 2;

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

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2Bool"/> structure with the given values for the X and Y
        /// components.
        /// </summary>
        /// <param name="x">The value of the X component.</param>
        /// <param name="y">The value of the Y component.</param>
        public Vector2Bool(bool x, bool y)
        {
            X = x;
            Y = y;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector2Bool"/> are the same
        /// as the components of the second specified <see cref="Vector2Bool"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2Bool"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector2Bool"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector2Bool"/> are the same.</returns>
        public static bool operator ==(Vector2Bool a, Vector2Bool b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Vector2Bool"/> are not the
        /// same as the components of the second specified <see cref="Vector2Bool"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Vector2Bool"/> to compare.</param>
        /// <param name="b">The second <see cref="Vector2Bool"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Vector2Bool"/> are not the same.</returns>
        public static bool operator !=(Vector2Bool a, Vector2Bool b)
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
        /// Gets a value indicating whether the components of this <see cref="Vector2Bool"/> are the same as the
        /// components of the second specified <see cref="Vector2Bool"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Vector2Bool"/>.</param>
        /// <returns>true, if the components of both <see cref="Vector2Bool"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2Bool))
            {
                return false;
            }
            Vector2Bool vector2Bool = (Vector2Bool)obj;
            return Equals(ref vector2Bool);
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
        /// Gets a string describing the components of this <see cref="Vector2Bool"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Vector2Bool"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "{{X={0},Y={1}}}", X, Y);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector2Bool"/> is equal to another <see cref="Vector2Bool"/>.
        /// </summary>
        /// <param name="other">A <see cref="Vector2Bool"/> to compare with this <see cref="Vector2Bool"/>.</param>
        /// <returns>true if the current <see cref="Vector2Bool"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Vector2Bool other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Vector2Bool"/> is equal to another <see cref="Vector2Bool"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Vector2Bool"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Vector2Bool"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Vector2Bool other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
