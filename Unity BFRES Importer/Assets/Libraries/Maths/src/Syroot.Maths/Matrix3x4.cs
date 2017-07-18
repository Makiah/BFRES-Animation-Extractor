using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a matrix with 3 rows and 4 columns in row-major notation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix3x4 : IEquatable<Matrix3x4>, IEquatableByRef<Matrix3x4>, INearlyEquatable<Matrix3x4>,
        INearlyEquatableByRef<Matrix3x4>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Matrix4"/> with all components being 0f.
        /// </summary>
        public static readonly Matrix3x4 Zero = new Matrix3x4();

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public const int Rows = 3;

        /// <summary>
        /// Gets the number of columns.
        /// </summary>
        public const int Columns = 4;

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
        /// The value in the first row and the fourth column.
        /// </summary>
        public float M14;

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
        /// <summary>
        /// The value in the second row and the fourth column.
        /// </summary>
        public float M24;

        /// <summary>
        /// The value in the third row and the first column.
        /// </summary>
        public float M31;
        /// <summary>
        /// The value in the third row and the second column.
        /// </summary>
        public float M32;
        /// <summary>
        /// The value in the third row and the third column.
        /// </summary>
        public float M33;
        /// <summary>
        /// The value in the third row and the fourth column.
        /// </summary>
        public float M34;
        
        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix4"/> struct with the given values.
        /// </summary>
        /// <param name="m11">The value in the first row and the first column.</param>
        /// <param name="m12">The value in the first row and the second column.</param>
        /// <param name="m13">The value in the first row and the third column.</param>
        /// <param name="m14">The value in the first row and the fourth column.</param>
        /// <param name="m21">The value in the second row and the first column.</param>
        /// <param name="m22">The value in the second row and the second column.</param>
        /// <param name="m23">The value in the second row and the third column.</param>
        /// <param name="m24">The value in the second row and the fourth column.</param>
        /// <param name="m31">The value in the third row and the first column.</param>
        /// <param name="m32">The value in the third row and the second column.</param>
        /// <param name="m33">The value in the third row and the third column.</param>
        /// <param name="m34">The value in the third row and the fourth column.</param>
        public Matrix3x4(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34)
        {
            M11 = m11; M12 = m12; M13 = m13; M14 = m14;
            M21 = m21; M22 = m22; M23 = m23; M24 = m24;
            M31 = m31; M32 = m32; M33 = m33; M34 = m34;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets a value indicating whether the components of Matrix4 first specified <see cref="Matrix3x4"/> are the same
        /// as the components of the second specified <see cref="Matrix3x4"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix3x4"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix3x4"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix3x4"/> are the same.</returns>
        public static bool operator ==(Matrix3x4 a, Matrix3x4 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Matrix3x4"/> are not the same
        /// as the components of the second specified<see cref="Matrix3x4"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix3x4"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix3x4"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix3x4"/> are not the same.</returns>
        public static bool operator !=(Matrix3x4 a, Matrix3x4 b)
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
                    case 3: return M14;

                    case 4: return M21;
                    case 5: return M22;
                    case 6: return M23;
                    case 7: return M24;

                    case 8: return M31;
                    case 9: return M32;
                    case 10: return M33;
                    case 11: return M34;

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
                    case 3: M14 = value; break;

                    case 4: M21 = value; break;
                    case 5: M22 = value; break;
                    case 6: M23 = value; break;
                    case 7: M24 = value; break;

                    case 8: M31 = value; break;
                    case 9: M32 = value; break;
                    case 10: M33 = value; break;
                    case 11: M34 = value; break;

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
        /// Gets a value indicating whether the components of this <see cref="Matrix3x4"/> are the same as the components
        /// of the second specified <see cref="Matrix3x4"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Matrix3x4"/>.</param>
        /// <returns>true, if the components of both <see cref="Matrix3x4"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix3x4))
            {
                return false;
            }
            Matrix3x4 matrix3x4 = (Matrix3x4)obj;
            return Equals(ref matrix3x4);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 1531;
                hash *= 4519 + M11.GetHashCode();
                hash *= 4519 + M12.GetHashCode();
                hash *= 4519 + M13.GetHashCode();
                hash *= 4519 + M14.GetHashCode();
                hash *= 4519 + M21.GetHashCode();
                hash *= 4519 + M22.GetHashCode();
                hash *= 4519 + M23.GetHashCode();
                hash *= 4519 + M24.GetHashCode();
                hash *= 4519 + M31.GetHashCode();
                hash *= 4519 + M32.GetHashCode();
                hash *= 4519 + M33.GetHashCode();
                hash *= 4519 + M34.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Matrix3x4"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Matrix3x4"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,
                "{{[M11={0},M12={1},M13={2},M14={3}]"
                + "[M21={4},M22={5},M23={6},M24={7}]"
                + "[M31={8},M32={9},M33={10},M34={11}]}}",
                M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x4"/> is equal to another <see cref="Matrix3x4"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x4"/> to compare with this <see cref="Matrix3x4"/>.</param>
        /// <returns>true if the current <see cref="Matrix3x4"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Matrix3x4 other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x4"/> is equal to another <see cref="Matrix3x4"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x4"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix3x4"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Matrix3x4 other)
        {
            return M11 == other.M11 && M12 == other.M12 && M13 == other.M13 && M14 == other.M14
                && M21 == other.M21 && M22 == other.M22 && M23 == other.M23 && M24 == other.M24
                && M31 == other.M31 && M32 == other.M32 && M33 == other.M33 && M34 == other.M34;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x4"/> is nearly equal to another <see cref="Matrix3x4"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x4"/> to compare with this <see cref="Matrix3x4"/>.</param>
        /// <returns>true if the current <see cref="Matrix3x4"/> is nearly equal to the other parameter; otherwise, false.
        /// </returns>
        public bool NearlyEquals(Matrix3x4 other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3x4"/> is nearly equal to another <see cref="Matrix3x4"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3x4"/> to compare with this <see cref="Matrix3x4"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix3x4"/> is nearly equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool NearlyEquals(ref Matrix3x4 other)
        {
            return M11.NearlyEquals(other.M11)
                && M12.NearlyEquals(other.M12)
                && M13.NearlyEquals(other.M13)
                && M14.NearlyEquals(other.M14)
                && M21.NearlyEquals(other.M21)
                && M22.NearlyEquals(other.M22)
                && M23.NearlyEquals(other.M23)
                && M24.NearlyEquals(other.M24)
                && M31.NearlyEquals(other.M31)
                && M32.NearlyEquals(other.M32)
                && M33.NearlyEquals(other.M33)
                && M34.NearlyEquals(other.M34);
        }
    }
}
