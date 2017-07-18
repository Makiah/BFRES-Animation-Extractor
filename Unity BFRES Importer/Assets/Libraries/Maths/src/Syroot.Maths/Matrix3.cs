using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a matrix with 3 rows and 3 columns in row-major notation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix3 : IEquatable<Matrix3>, IEquatableByRef<Matrix3>, INearlyEquatable<Matrix3>,
        INearlyEquatableByRef<Matrix3>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Matrix3"/> with all components being 0f.
        /// </summary>
        public static readonly Matrix3 Zero = new Matrix3();

        /// <summary>
        /// The identity <see cref="Matrix3"/> which causes no transformations to happen.
        /// </summary>
        public static readonly Matrix3 Identity = new Matrix3() { M11 = 1f, M22 = 1f, M33 = 1f };

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public const int Rows = 3;

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

        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix3"/> struct with the given values.
        /// </summary>
        /// <param name="m11">The value in the first row and the first column.</param>
        /// <param name="m12">The value in the first row and the second column.</param>
        /// <param name="m13">The value in the first row and the third column.</param>
        /// <param name="m21">The value in the second row and the first column.</param>
        /// <param name="m22">The value in the second row and the second column.</param>
        /// <param name="m23">The value in the second row and the third column.</param>
        /// <param name="m31">The value in the third row and the first column.</param>
        /// <param name="m32">The value in the third row and the second column.</param>
        /// <param name="m33">The value in the third row and the third column.</param>
        public Matrix3(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32,
            float m33)
        {
            M11 = m11; M12 = m12; M13 = m13;
            M21 = m21; M22 = m22; M23 = m23;
            M31 = m31; M32 = m32; M33 = m33;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Multiplicates the first <see cref="Matrix3"/> with the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix3"/> to multiplicate with.</param>
        /// <param name="b">The second <see cref="Matrix3"/> to multiplicate with.</param>
        /// <returns>The multiplication result.</returns>
        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            Matrix3 result = new Matrix3()
            {
                M11 = (a.M11 * b.M11) + (a.M12 * b.M21) + (a.M13 * b.M31),
                M12 = (a.M11 * b.M12) + (a.M12 * b.M22) + (a.M13 * b.M32),
                M13 = (a.M11 * b.M13) + (a.M12 * b.M23) + (a.M13 * b.M33),
                M21 = (a.M21 * b.M11) + (a.M22 * b.M21) + (a.M23 * b.M31),
                M22 = (a.M21 * b.M12) + (a.M22 * b.M22) + (a.M23 * b.M32),
                M23 = (a.M21 * b.M13) + (a.M22 * b.M23) + (a.M23 * b.M33),
                M31 = (a.M31 * b.M11) + (a.M32 * b.M21) + (a.M33 * b.M31),
                M32 = (a.M31 * b.M12) + (a.M32 * b.M22) + (a.M33 * b.M32),
                M33 = (a.M31 * b.M13) + (a.M32 * b.M23) + (a.M33 * b.M33)
            };
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether the components of Matrix3 first specified <see cref="Matrix3"/> are the same
        /// as the components of the second specified <see cref="Matrix3"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix3"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix3"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix3"/> are the same.</returns>
        public static bool operator ==(Matrix3 a, Matrix3 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Matrix3"/> are not the same
        /// as the components of the second specified<see cref="Matrix3"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix3"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix3"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix3"/> are not the same.</returns>
        public static bool operator !=(Matrix3 a, Matrix3 b)
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

                    case 6: return M31;
                    case 7: return M32;
                    case 8: return M33;

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

                    case 6: M31 = value; break;
                    case 7: M32 = value; break;
                    case 8: M33 = value; break;

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
        /// Gets a value indicating whether the components of this <see cref="Matrix3"/> are the same as the components
        /// of the second specified <see cref="Matrix3"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Matrix3"/>.</param>
        /// <returns>true, if the components of both <see cref="Matrix3"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix3))
            {
                return false;
            }
            Matrix3 matrix3x3 = (Matrix3)obj;
            return Equals(ref matrix3x3);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 239;
                hash *= 433 + M11.GetHashCode();
                hash *= 433 + M12.GetHashCode();
                hash *= 433 + M13.GetHashCode();
                hash *= 433 + M21.GetHashCode();
                hash *= 433 + M22.GetHashCode();
                hash *= 433 + M23.GetHashCode();
                hash *= 433 + M31.GetHashCode();
                hash *= 433 + M32.GetHashCode();
                hash *= 433 + M33.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Matrix3"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Matrix3"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,
                "{{[M11={0},M12={1},M13={2}]"
                + "[M21={3},M22={4},M23={5}]"
                + "[M31={6},M32={7},M33={8}]}}",
                M11, M12, M13,
                M21, M22, M23,
                M31, M32, M33);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3"/> is equal to another <see cref="Matrix3"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3"/> to compare with this <see cref="Matrix3"/>.</param>
        /// <returns>true if the current <see cref="Matrix3"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Matrix3 other)
        {
            return Equals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3"/> is equal to another <see cref="Matrix3"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix3"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Matrix3 other)
        {
            return M11 == other.M11 && M12 == other.M12 && M13 == other.M13
                && M21 == other.M21 && M22 == other.M22 && M23 == other.M23
                && M31 == other.M31 && M32 == other.M32 && M33 == other.M33;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3"/> is nearly equal to another <see cref="Matrix3"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3"/> to compare with this <see cref="Matrix3"/>.</param>
        /// <returns>true if the current <see cref="Matrix3"/> is nearly equal to the other parameter; otherwise, false.
        /// </returns>
        public bool NearlyEquals(Matrix3 other)
        {
            return NearlyEquals(ref other);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix3"/> is nearly equal to another <see cref="Matrix3"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix3"/> to compare with this <see cref="Matrix3"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix3"/> is nearly equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool NearlyEquals(ref Matrix3 other)
        {
            return M11.NearlyEquals(other.M11)
                && M12.NearlyEquals(other.M12)
                && M13.NearlyEquals(other.M13)
                && M21.NearlyEquals(other.M21)
                && M22.NearlyEquals(other.M22)
                && M23.NearlyEquals(other.M23)
                && M31.NearlyEquals(other.M31)
                && M32.NearlyEquals(other.M32)
                && M33.NearlyEquals(other.M33);
        }
    }
}
