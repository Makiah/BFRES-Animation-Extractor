using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a matrix with 4 rows and 4 columns in row-major notation.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix4 : IEquatable<Matrix4>, IEquatableByRef<Matrix4>, INearlyEquatable<Matrix4>,
        INearlyEquatableByRef<Matrix4>
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// A <see cref="Matrix4"/> with all components being 0f.
        /// </summary>
        public static readonly Matrix4 Zero = new Matrix4();

        /// <summary>
        /// The identity <see cref="Matrix4"/> which causes no transformations to happen.
        /// </summary>
        public static readonly Matrix4 Identity = new Matrix4() { M11 = 1f, M22 = 1f, M33 = 1f, M44 = 1f };

        /// <summary>
        /// Gets the number of rows.
        /// </summary>
        public const int Rows = 4;

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

        /// <summary>
        /// The value in the fourth row and the first column.
        /// </summary>
        public float M41;
        /// <summary>
        /// The value in the fourth row and the second column.
        /// </summary>
        public float M42;
        /// <summary>
        /// The value in the fourth row and the third column.
        /// </summary>
        public float M43;
        /// <summary>
        /// The value in the fourth row and the fourth column.
        /// </summary>
        public float M44;

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
        /// <param name="m41">The value in the fourth row and the first column.</param>
        /// <param name="m42">The value in the fourth row and the second column.</param>
        /// <param name="m43">The value in the fourth row and the third column.</param>
        /// <param name="m44">The value in the fourth row and the fourth column.</param>
        public Matrix4(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            M11 = m11; M12 = m12; M13 = m13; M14 = m14;
            M21 = m21; M22 = m22; M23 = m23; M24 = m24;
            M31 = m31; M32 = m32; M33 = m33; M34 = m34;
            M41 = m41; M42 = m42; M43 = m43; M44 = m44;
        }

        // ---- OPERATORS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Multiplicates the first <see cref="Matrix4"/> with the second one.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix4"/> to multiplicate with.</param>
        /// <param name="b">The second <see cref="Matrix4"/> to multiplicate with.</param>
        /// <returns>The multiplication result.</returns>
        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Matrix4 result = new Matrix4()
            {
                M11 = (a.M11 * b.M11) + (a.M12 * b.M21) + (a.M13 * b.M31) + (a.M14 * b.M41),
                M12 = (a.M11 * b.M12) + (a.M12 * b.M22) + (a.M13 * b.M32) + (a.M14 * b.M42),
                M13 = (a.M11 * b.M13) + (a.M12 * b.M23) + (a.M13 * b.M33) + (a.M14 * b.M43),
                M14 = (a.M11 * b.M14) + (a.M12 * b.M24) + (a.M13 * b.M34) + (a.M14 * b.M44),
                M21 = (a.M21 * b.M11) + (a.M22 * b.M21) + (a.M23 * b.M31) + (a.M24 * b.M41),
                M22 = (a.M21 * b.M12) + (a.M22 * b.M22) + (a.M23 * b.M32) + (a.M24 * b.M42),
                M23 = (a.M21 * b.M13) + (a.M22 * b.M23) + (a.M23 * b.M33) + (a.M24 * b.M43),
                M24 = (a.M21 * b.M14) + (a.M22 * b.M24) + (a.M23 * b.M34) + (a.M24 * b.M44),
                M31 = (a.M31 * b.M11) + (a.M32 * b.M21) + (a.M33 * b.M31) + (a.M34 * b.M41),
                M32 = (a.M31 * b.M12) + (a.M32 * b.M22) + (a.M33 * b.M32) + (a.M34 * b.M42),
                M33 = (a.M31 * b.M13) + (a.M32 * b.M23) + (a.M33 * b.M33) + (a.M34 * b.M43),
                M34 = (a.M31 * b.M14) + (a.M32 * b.M24) + (a.M33 * b.M34) + (a.M34 * b.M44),
                M41 = (a.M41 * b.M11) + (a.M42 * b.M21) + (a.M43 * b.M31) + (a.M44 * b.M41),
                M42 = (a.M41 * b.M12) + (a.M42 * b.M22) + (a.M43 * b.M32) + (a.M44 * b.M42),
                M43 = (a.M41 * b.M13) + (a.M42 * b.M23) + (a.M43 * b.M33) + (a.M44 * b.M43),
                M44 = (a.M41 * b.M14) + (a.M42 * b.M24) + (a.M43 * b.M34) + (a.M44 * b.M44)
            };
            return result;
        }

        /// <summary>
        /// Gets a value indicating whether the components of Matrix4 first specified <see cref="Matrix4"/> are the same
        /// as the components of the second specified <see cref="Matrix4"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix4"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix4"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix4"/> are the same.</returns>
        public static bool operator ==(Matrix4 a, Matrix4 b)
        {
            return a.Equals(ref b);
        }

        /// <summary>
        /// Gets a value indicating whether the components of the first specified <see cref="Matrix4"/> are not the same
        /// as the components of the second specified<see cref="Matrix4"/>.
        /// </summary>
        /// <param name="a">The first <see cref="Matrix4"/> to compare.</param>
        /// <param name="b">The second <see cref="Matrix4"/> to compare.</param>
        /// <returns>true, if the components of both <see cref="Matrix4"/> are not the same.</returns>
        public static bool operator !=(Matrix4 a, Matrix4 b)
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

                    case 12: return M41;
                    case 13: return M42;
                    case 14: return M43;
                    case 15: return M44;

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

                    case 12: M41 = value; break;
                    case 13: M42 = value; break;
                    case 14: M43 = value; break;
                    case 15: M44 = value; break;

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
        /// Gets a <see cref="Matrix4"/> for an orthographic camera to transform vertices to normalized device
        /// coordinates.
        /// </summary>
        /// <param name="top">The top clipping distance before which vertices will disappear.</param>
        /// <param name="right">The right clipping distance before which vertices will disappear.</param>
        /// <param name="bottom">The bottom clipping distance before which vertices will disappear.</param>
        /// <param name="left">The left clipping distance before which vertices will disappear.</param>
        /// <param name="zNear">The near clipping distance before which vertices will disappear.</param>
        /// <param name="zFar">The far clipping distance after which vertices will disappear.</param>
        /// <returns>An orthographic camera <see cref="Matrix4"/>.</returns>
        public static Matrix4 GetOrthographicMatrix(float top, float right, float bottom, float left, float zNear,
            float zFar)
        {
            float rightLeftInvert = 1f / (right - left);
            float topBottomInvert = 1f / (top - bottom);
            float farNearInvert = 1f / (zFar - zNear);

            Matrix4 result = Matrix4.Identity;
            result.M11 = 2f * rightLeftInvert;
            result.M22 = 2f * topBottomInvert;
            result.M33 = -2f * farNearInvert;
            result.M41 = -(right + left) * rightLeftInvert;
            result.M42 = -(top + bottom) * topBottomInvert;
            result.M43 = -(zFar + zNear) * farNearInvert;
            return result;
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> for a rotation around the X axis in 3-dimensional space.
        /// </summary>
        /// <param name="degrees">The degrees of rotation.</param>
        /// <returns>A <see cref="Matrix4"/> representing the given rotation.</returns>
        public static Matrix4 GetRotationXMatrix(float degrees)
        {
            float radians = Algebra.DegreesToRadians(degrees);
            float sinRotation = (float)System.Math.Sin(radians);
            float cosRotation = (float)System.Math.Cos(radians);
            Matrix4 matrix = Identity;
            matrix.M22 = cosRotation;
            matrix.M23 = sinRotation;
            matrix.M32 = -sinRotation;
            matrix.M33 = cosRotation;
            return matrix;
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> for a rotation around the Y axis in 3-dimensional space.
        /// </summary>
        /// <param name="degrees">The degrees of rotation.</param>
        /// <returns>A <see cref="Matrix4"/> representing the given rotation.</returns>
        public static Matrix4 GetRotationYMatrix(float degrees)
        {
            float radians = Algebra.DegreesToRadians(degrees);
            float sinRotation = (float)System.Math.Sin(radians);
            float cosRotation = (float)System.Math.Cos(radians);
            Matrix4 matrix = Identity;
            matrix.M11 = cosRotation;
            matrix.M13 = -sinRotation;
            matrix.M31 = sinRotation;
            matrix.M33 = cosRotation;
            return matrix;
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> for a rotation around the Z axis in 3-dimensional space.
        /// </summary>
        /// <param name="degrees">The degrees of rotation.</param>
        /// <returns>A <see cref="Matrix4"/> representing the given rotation.</returns>
        public static Matrix4 GetRotationZMatrix(float degrees)
        {
            float radians = Algebra.DegreesToRadians(degrees);
            float sinRotation = Algebra.Sin(radians);
            float cosRotation = Algebra.Cos(radians);
            Matrix4 matrix = Identity;
            matrix.M11 = cosRotation;
            matrix.M12 = sinRotation;
            matrix.M21 = -sinRotation;
            matrix.M22 = cosRotation;
            return matrix;
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> for a scaling of vectors in 3-dimensional space.
        /// </summary>
        /// <param name="scaling">The <see cref="Vector3F"/> containing the scaling amount on all axes.</param>
        /// <returns>A <see cref="Matrix4"/> representing the given scaling.</returns>
        public static Matrix4 GetScalingMatrix(Vector3F scaling)
        {
            return GetScalingMatrix(scaling.X, scaling.Y, scaling.Z);
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> for a scaling of vectors in 3-dimensional space.
        /// </summary>
        /// <param name="xScale">The scaling amount on the X axis.</param>
        /// <param name="yScale">The scaling amount on the Y axis.</param>
        /// <param name="zScale">The scaling amount on the Z axis.</param>
        /// <returns>A <see cref="Matrix4"/> representing the given scaling.</returns>
        public static Matrix4 GetScalingMatrix(float xScale, float yScale, float zScale)
        {
            Matrix4 matrix = Identity;
            matrix.M11 = xScale;
            matrix.M22 = yScale;
            matrix.M33 = zScale;
            return matrix;
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> for a translation of vectors in 3-dimensional space.
        /// </summary>
        /// <param name="translation">The <see cref="Vector3F"/> containing the translation amount on all axes.</param>
        /// <returns>A <see cref="Matrix4"/> representing the given translation.</returns>
        public static Matrix4 GetTranslationMatrix(Vector3F translation)
        {
            return GetTranslationMatrix(translation.X, translation.Y, translation.Z);
        }

        /// <summary>
        /// Gets a <see cref="Matrix4"/> for a translation of vectors in 3-dimensional space.
        /// </summary>
        /// <param name="x">The translation amount on the X axis.</param>
        /// <param name="y">The translation amount on the Y axis.</param>
        /// <param name="z">The translation amount on the Z axis.</param>
        /// <returns>A <see cref="Matrix4"/> representing the given translation.</returns>
        public static Matrix4 GetTranslationMatrix(float x, float y, float z)
        {
            Matrix4 matrix = Identity;
            matrix.M41 = x;
            matrix.M42 = y;
            matrix.M43 = z;
            return matrix;
        }

        /// <summary>
        /// Gets a value indicating whether the components of this <see cref="Matrix4"/> are the same as the components
        /// of the second specified <see cref="Matrix4"/>.
        /// </summary>
        /// <param name="obj">The object to compare, if it is a <see cref="Matrix4"/>.</param>
        /// <returns>true, if the components of both <see cref="Matrix4"/> are the same.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix4))
            {
                return false;
            }
            Matrix4 matrix4x4 = (Matrix4)obj;
            return Equals(ref matrix4x4);
        }

        /// <summary>
        /// Gets a hash code as an indication for object equality.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 421;
                hash *= 479 + M11.GetHashCode();
                hash *= 479 + M12.GetHashCode();
                hash *= 479 + M13.GetHashCode();
                hash *= 479 + M14.GetHashCode();
                hash *= 479 + M21.GetHashCode();
                hash *= 479 + M22.GetHashCode();
                hash *= 479 + M23.GetHashCode();
                hash *= 479 + M24.GetHashCode();
                hash *= 479 + M31.GetHashCode();
                hash *= 479 + M32.GetHashCode();
                hash *= 479 + M33.GetHashCode();
                hash *= 479 + M34.GetHashCode();
                hash *= 479 + M41.GetHashCode();
                hash *= 479 + M42.GetHashCode();
                hash *= 479 + M43.GetHashCode();
                hash *= 479 + M44.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Gets a string describing the components of this <see cref="Matrix4"/>.
        /// </summary>
        /// <returns>A string describing this <see cref="Matrix4"/>.</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,
                "{{[M11={0},M12={1},M13={2},M14={3}]"
                + "[M21={4},M22={5},M23={6},M24={7}]"
                + "[M31={8},M32={9},M33={10},M34={11}]"
                + "[M41={12},M42={13},M43={14},M44={15}]}}",
                M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34,
                M41, M42, M43, M44);
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix4"/> is equal to another <see cref="Matrix4"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix4"/> to compare with this <see cref="Matrix4"/>.</param>
        /// <returns>true if the current <see cref="Matrix4"/> is equal to the other parameter; otherwise, false.
        /// </returns>
        public bool Equals(Matrix4 other)
        {
            return Equals(ref other);
        }
        
        /// <summary>
        /// Indicates whether the current <see cref="Matrix4"/> is equal to another <see cref="Matrix4"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix4"/> to compare with this structure.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix4"/> is equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool Equals(ref Matrix4 other)
        {
            return M11 == other.M11 && M12 == other.M12 && M13 == other.M13 && M14 == other.M14
                && M21 == other.M21 && M22 == other.M22 && M23 == other.M23 && M24 == other.M24
                && M31 == other.M31 && M32 == other.M32 && M33 == other.M33 && M34 == other.M34
                && M41 == other.M41 && M42 == other.M42 && M43 == other.M43 && M44 == other.M44;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Matrix4"/> is nearly equal to another <see cref="Matrix4"/>.
        /// </summary>
        /// <param name="other">A <see cref="Matrix4"/> to compare with this <see cref="Matrix4"/>.</param>
        /// <returns>true if the current <see cref="Matrix4"/> is nearly equal to the other parameter; otherwise, false.
        /// </returns>
        public bool NearlyEquals(Matrix4 other)
        {
            return NearlyEquals(ref other);
        }
        
        /// <summary>
        /// Indicates whether the current <see cref="Matrix4"/> is nearly equal to another <see cref="Matrix4"/>.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A <see cref="Matrix4"/> to compare with this <see cref="Matrix4"/>.</param>
        /// <returns><c>true</c> if the current <see cref="Matrix4"/> is nearly equal to the other parameter; otherwise,
        /// <c>false</c>.</returns>
        public bool NearlyEquals(ref Matrix4 other)
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
                && M34.NearlyEquals(other.M34)
                && M41.NearlyEquals(other.M41)
                && M42.NearlyEquals(other.M42)
                && M43.NearlyEquals(other.M43)
                && M44.NearlyEquals(other.M44);
        }
    }
}
