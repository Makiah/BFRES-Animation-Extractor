namespace Syroot.Maths
{
    /// <summary>
    /// Defines a generalized method that a value type or class implements to create a type-specific method for
    /// determining equality of instances.
    /// Structures are passed by reference to avoid stack structure copying.
    /// </summary>
    /// <typeparam name="T">The type of structures to compare.</typeparam>
    public interface IEquatableByRef<T> where T : struct
    {
        // ---- METHODS ------------------------------------------------------------------------------------------------

        /// <summary>
        /// Indicates whether the current structure is equal to another structure of the same type.
        /// Structures are passed by reference to avoid stack structure copying.
        /// </summary>
        /// <param name="other">A structure to compare with this structure.</param>
        /// <returns>true if the current structure is equal to the other parameter; otherwise, false.</returns>
        bool Equals(ref T other);
    }
}
