namespace Syroot.Maths
{
    /// <summary>
    /// Defines a generalized method that a value type or class implements to create a type-specific method for
    /// determining nearly equality of instances with float values, ignoring rounding precision errors.
    /// Structures are passed by reference to avoid stack structure copying.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public interface INearlyEquatableByRef<T>
    {
        // ---- METHODS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// Indicates whether the current object is nearly equal to another object of the same type.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is nearly equal to the other parameter; otherwise, false.</returns>
        bool NearlyEquals(ref T other);
    }
}