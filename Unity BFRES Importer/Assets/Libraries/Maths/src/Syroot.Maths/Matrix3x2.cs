using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a matrix with 3 rows and 3 columns in row-major notation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix3x2 : IEquatable<Matrix3x2>, IEquatableByRef<Matrix3x2>, INearlyEquatable<Matrix3x2>,
        INearlyEquatableByRef<Matrix3x2>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Matrix3x2"/> with all components being 0f.
        /// </summary>
        public static readonly Matrix3x2 Zero = new Matrix3x2();
        
        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public const int Rows = 3;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public const int Columns = 2;

        /// <summary>
        /// Gets the amount of value types required to represent this structure.
        /// </summary>
        public const int ValueCount = Rows * Columns;

        /// <summary>
        /// Gets the size of this structure.
        /// </summary>
        public const int SizeInBytes = ValueCount * sizeof(float);

        // ---- MEMBERS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// The value in the first row and the first column.
        /// </summary>
        public float M11;
        /// <summary>
        /// The value in the first row and the second column.
        /// </summary>
        public float M12;

        /// <summary>
        /// The value in the second row and the first column.
        /// </summary>
        public float M21;
        /// <summary>
        /// The value in the second row and the second column.
        /// </summary>
        public float M22;

        /// <summary>
        /// The value in the third row and the first column.
        /// </summary>
        public float M31;
        /// <summary>
        /// The value in the third row and the second column.
        /// </summary>
        public float M32;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3x2"/> struct with the given values.
        /// </summary>
        /// <param name="m11">The value in the first row and the first column.</param>
        /// <param name="m12">The value in the first row and the second column.</param>
        /// <param name="m21">The value in the second row and the first column.</param>
        /// <param name="m22">The value in the second row and the second column.</param>
        /// <param name="m31">The value in the third row and the first column.</param>
        /// <param name="m32">The value in the third row and the second column.</param>
        public Matrix3x2(float m11, float m12, float m21, float m22, float m31, float m32)
        {
            M11 = m11; M12 = m12;;
            M21 = m21; M22 = m22;;
            M31 = m31; M32 = m32;;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets a value indicating whether the components of Matrix3x2 first specified <see cref="Matrix3x2"/> are the
        /// same as the components of the second specified <see cref="Matrix3x2"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix3x2"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix3x2"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix3x2"/> are the same.</returns>
        public static bool operator ==(Matrix3x2 a, Matrix3x2 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Matrix3x2"/> are not the
        /// same as the components of the second specified<see cref="Matrix3x2"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix3x2"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix3x2"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix3x2"/> are not the same.</returns>
        public static bool operator !=(Matrix3x2 a, Matrix3x2 b)
        {
            return !a.Equals(ref b);
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
                    case 0: return M11;
                    case 1: return M12;

                    case 2: return M21;
                    case 3: return M22;

                    case 4: return M31;
                    case 5: return M32;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(index),
                            $"Index must be between 0 and {ValueCount}.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: M11 = value; break;
                    case 1: M12 = value; break;

                    case 2: M21 = value; break;
                    case 3: M22 = value; break;

                    case 4: M31 = value; break;
                    case 5: M32 = value; break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(index),
                            $"Index must be between 0 and {ValueCount}.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the component at the given <paramref name="row"/> and <paramref name="column"/>.
        /// </summary>
        /// <param name="row">The row of the component.</param>
        /// <param name="column">The column of the component.</param>
        /// <returns>The value of the component.</returns>
        public float this[int row, int column]
        {
            get
            {
                return this[row * Columns + column];
            }
            set
            {
                this[row * Columns + column] = value;
            }
        }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="Matrix3x2"/> are the same as the components
        /// of the second specified <see cref="Matrix3x2"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Matrix3x2"/>.</param>
        /// <returns>true, if the components of both <see cref="Matrix3x2"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix3x2))
            {
                return false;
            }
            Matrix3x2 matrix3x2 = (Matrix3x2)obj;
            return Equals(ref matrix3x2);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 6521;
                hash *= 1493 + M11.GetHashCode();
                hash *= 1493 + M12.GetHashCode();
                hash *= 1493 + M21.GetHashCode();
                hash *= 1493 + M22.GetHashCode();
                hash *= 1493 + M31.GetHashCode();
                hash *= 1493 + M32.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Matrix3x2"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Matrix3x2"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,
                "{{[M11={0},M12={1}]"
                + "[M21={2},M22={3}]"
                + "[M31={4},M32={5}]}}",
                M11, M12,
                M21, M22,
                M31, M32);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x2"/> is equal to another <see cref="Matrix3x2"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x2"/> to compare with this <see cref="Matrix3x2"/>.</param>
        /// <returns>true if the current <see cref="Matrix3x2"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Matrix3x2 other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x2"/> is equal to another <see cref="Matrix3x2"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x2"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix3x2"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Matrix3x2 other)
        {
            return M11 == other.M11 && M12 == other.M12
                && M21 == other.M21 && M22 == other.M22
                && M31 == other.M31 && M32 == other.M32;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x2"/> is nearly equal to another <see cref="Matrix3x2"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x2"/> to compare with this <see cref="Matrix3x2"/>.</param>
        /// <returns>true if the current <see cref="Matrix3x2"/> is nearly equal to the other parameter; otherwise, false.
        /// </returns>
        public bool NearlyEquals(Matrix3x2 other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x2"/> is nearly equal to another <see cref="Matrix3x2"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x2"/> to compare with this <see cref="Matrix3x2"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix3x2"/> is nearly equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool NearlyEquals(ref Matrix3x2 other)
        {
            return M11.NearlyEquals(other.M11)
                && M12.NearlyEquals(other.M12)
                && M21.NearlyEquals(other.M21)
                && M22.NearlyEquals(other.M22)
                && M31.NearlyEquals(other.M31)
                && M32.NearlyEquals(other.M32);
        }
    }
}
