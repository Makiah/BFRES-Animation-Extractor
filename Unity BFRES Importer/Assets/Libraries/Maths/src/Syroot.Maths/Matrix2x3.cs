using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a matrix with 2 rows and 3 columns in row-major notation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix2x3 : IEquatable<Matrix2x3>, IEquatableByRef<Matrix2x3>, INearlyEquatable<Matrix2x3>,
        INearlyEquatableByRef<Matrix2x3>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Matrix2x3"/> with all components being 0f.
        /// </summary>
        public static readonly Matrix2x3 Zero = new Matrix2x3();

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public const int Rows = 2;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public const int Columns = 3;

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
        /// The value in the first row and the third column.
        /// </summary>
        public float M13;
        
        /// <summary>
        /// The value in the second row and the first column.
        /// </summary>
        public float M21;
        /// <summary>
        /// The value in the second row and the second column.
        /// </summary>
        public float M22;
        /// <summary>
        /// The value in the second row and the third column.
        /// </summary>
        public float M23;

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix2x3"/> struct with the given values.
        /// </summary>
        /// <param name="m11">The value in the first row and the first column.</param>
        /// <param name="m12">The value in the first row and the second column.</param>
        /// <param name="m13">The value in the first row and the third column.</param>
        /// <param name="m21">The value in the second row and the first column.</param>
        /// <param name="m22">The value in the second row and the second column.</param>
        /// <param name="m23">The value in the second row and the third column.</param>
        public Matrix2x3(
            float m11, float m12, float m13,
            float m21, float m22, float m23)
        {
            M11 = m11; M12 = m12; M13 = m13;
            M21 = m21; M22 = m22; M23 = m23;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets a value indicating whether the components of Matrix4 first specified <see cref="Matrix2x3"/> are the
        /// same as the components of the second specified <see cref="Matrix2x3"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix2x3"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix2x3"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix2x3"/> are the same.</returns>
        public static bool operator ==(Matrix2x3 a, Matrix2x3 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Matrix2x3"/> are not the
        /// same as the components of the second specified<see cref="Matrix2x3"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix2x3"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix2x3"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix2x3"/> are not the same.</returns>
        public static bool operator !=(Matrix2x3 a, Matrix2x3 b)
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
                    case 2: return M13;

                    case 3: return M21;
                    case 4: return M22;
                    case 5: return M23;

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
                    case 2: M13 = value; break;

                    case 3: M21 = value; break;
                    case 4: M22 = value; break;
                    case 5: M23 = value; break;

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
        /// Gets a value indicating whether the components of this <see cref="Matrix2x3"/> are the same as the
        /// components of the second specified <see cref="Matrix2x3"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Matrix2x3"/>.</param>
        /// <returns>true, if the components of both <see cref="Matrix2x3"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix2x3))
            {
                return false;
            }
            Matrix2x3 matrix2x3 = (Matrix2x3)obj;
            return Equals(ref matrix2x3);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 167;
                hash *= 53 + M11.GetHashCode();
                hash *= 53 + M12.GetHashCode();
                hash *= 53 + M13.GetHashCode();
                hash *= 53 + M21.GetHashCode();
                hash *= 53 + M22.GetHashCode();
                hash *= 53 + M23.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Matrix2x3"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Matrix2x3"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,
                "{{[M11={0},M12={1},M13={2}]"
                + "[M21={3},M22={4},M23={5}]}}",
                M11, M12, M13,
                M21, M22, M23);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix2x3"/> is equal to another <see cref="Matrix2x3"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix2x3"/> to compare with this <see cref="Matrix2x3"/>.</param>
        /// <returns>true if the current <see cref="Matrix2x3"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Matrix2x3 other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix2x3"/> is equal to another <see cref="Matrix2x3"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix2x3"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix2x3"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Matrix2x3 other)
        {
            return M11 == other.M11 && M12 == other.M12 && M13 == other.M13
                && M21 == other.M21 && M22 == other.M22 && M23 == other.M23;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix2x3"/> is nearly equal to another <see cref="Matrix2x3"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix2x3"/> to compare with this <see cref="Matrix2x3"/>.</param>
        /// <returns>true if the current <see cref="Matrix2x3"/> is nearly equal to the other parameter; otherwise,
        /// false.</returns>
        public bool NearlyEquals(Matrix2x3 other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix2x3"/> is nearly equal to another <see cref="Matrix2x3"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix2x3"/> to compare with this <see cref="Matrix2x3"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix2x3"/> is nearly equal to the other parameter;
        /// otherwise, <c>false</c>.</returns>
        public bool NearlyEquals(ref Matrix2x3 other)
        {
            return M11.NearlyEquals(other.M11) && M12.NearlyEquals(other.M12) && M13.NearlyEquals(other.M13)
                && M21.NearlyEquals(other.M21) && M22.NearlyEquals(other.M22) && M23.NearlyEquals(other.M23);
        }
    }
}
