namespace Syroot.Maths
{
    /// <summary>
    /// Defines a generalized method that a value type or class implements to create a type-specific method for
    /// determining nearly equality of instances with float values, ignoring rounding precision errors.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public interface INearlyEquatable<T>
    {
        // ---- METHODS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// Indicates whether the current object is nearly equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is nearly equal to the other parameter; otherwise <c>false</c>.</returns>
        bool NearlyEquals(T other);
    }
}
