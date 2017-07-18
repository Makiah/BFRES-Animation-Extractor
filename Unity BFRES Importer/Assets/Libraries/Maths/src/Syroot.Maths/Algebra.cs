using System;

namespace Syroot.Maths
{
    /// <summary>
    /// Represents a set of mathematical (extension) methods.
    /// </summary>
    public static class Algebra
    {
        // ---- CONSTANTS ----------------------------------------------------------------------------------------------

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        public const float Pi = (float)Math.PI;

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π, divided
        /// two times.
        /// </summary>
        public const float PiOver2 = (float)(Math.PI / 2.0);

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π, divided
        /// four times.
        /// </summary>
        public const float PiOver4 = (float)(Math.PI / 4.0);

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π, divided
        /// eight times.
        /// </summary>
        public const float PiOver8 = (float)(Math.PI / 8.0);

        private const float _degreesToRadiansFactor = (float)Math.PI / 180f;
        private const float _radiansToDegreesFactor = 180f / (float)Math.PI;
        private const float _floatEqualityEpsilon = 0.00001f;

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Limits the given <paramref name="value"/> to lie between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The smallest value to return.</param>
        /// <param name="max">The biggest value to return.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        /// <summary>
        /// Limits the given <paramref name="value"/> to lie between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The smallest value to return.</param>
        /// <param name="max">The biggest value to return.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        
        /// <summary>
        /// Returns the sine of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        /// <returns>The sine of a. If a is equal to <see cref="Single.NaN"/>, <see cref="Single.NegativeInfinity"/> or
        /// <see cref="Single.PositiveInfinity"/>, this method returns <see cref="Single.NaN"/>.</returns>
        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }

        /// <summary>
        /// Returns the cosine of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        /// <returns>The cosine of a. If a is equal to <see cref="Single.NaN"/>, <see cref="Single.NegativeInfinity"/>
        /// or <see cref="Single.PositiveInfinity"/>, this method returns <see cref="Single.NaN"/>.</returns>
        public static float Cos(float value)
        {
            return (float)Math.Cos(value);
        }

        /// <summary>
        /// Returns the tangent of the specified angle.
        /// </summary>
        /// <param name="value">An angle, measured in radians.</param>
        /// <returns>The tangent of a. If a is equal to <see cref="Single.NaN"/>, <see cref="Single.NegativeInfinity"/>
        /// or <see cref="Single.PositiveInfinity"/>, this method returns <see cref="Single.NaN"/>.</returns>
        public static float Tan(float value)
        {
            return (float)Math.Tan(value);
        }

        /// <summary>
        /// Interpolates between the given values by the given factor. If the factor is 0f, a is returned, otherwise b,
        /// and for anything between, an interpolation by that factor is returned.
        /// </summary>
        /// <param name="a">The start value returned if the factor would be 0f.</param>
        /// <param name="b">The end value returned if the factor would be 1f.</param>
        /// <param name="factor">The factor by which the two values are interpolated.</param>
        /// <returns>An interpolation between the two values by the given factor.</returns>
        public static float Lerp(float a, float b, float factor)
        {
            return a + ((b - a) * factor);
        }

        /// <summary>
        /// Converts the given angle in degrees into radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees to convert.</param>
        /// <returns>The converted angle in radians.</returns>
        public static float DegreesToRadians(float degrees)
        {
            return degrees * _degreesToRadiansFactor;
        }

        /// <summary>
        /// Converts the given angle in radians into degrees.
        /// </summary>
        /// <param name="radians">The angle in radians to convert.</param>
        /// <returns>The converted angle in degrees .</returns>
        public static float RadiansToDegrees(float radians)
        {
            return radians * _radiansToDegreesFactor;
        }

        /// <summary>
        /// Indicates whether the current <see cref="Single"/> is nearly equal to another <see cref="Single"/>.
        /// </summary>
        /// <param name="current">The extended <see cref="Single"/>.</param>
        /// <param name="other">A <see cref="Single"/> to compare with this <see cref="Single"/>.</param>
        /// <returns>true if the current <see cref="Single"/> is nearly equal to the other parameter; otherwise <c>false</c>.
        /// </returns>
        /// <seealso href="http://floating-point-gui.de/errors/comparison/"/>
        public static bool NearlyEquals(this float current, float other)
        {
            float absCurrent = Math.Abs(current);
            float absOther = Math.Abs(other);
            float difference = Math.Abs(current - other);

            // Shortcut, handles infinities
            if (current == other)
            {
                return true;
            }

            // Current or other is zero or both are extremely close to it relative error is less meaningful here
            if (current == 0 || other == 0 || difference < Double.MinValue)
            {
                return difference < (_floatEqualityEpsilon * Double.MinValue);
            }

            // Use relative error
            return difference / (absCurrent + absOther) < _floatEqualityEpsilon;
        }
    }
}
