using System;

namespace Syroot.NintenTools.Bfres.Script
{
    /// <summary>
    /// Represents an exception which is catched in <see cref="Program"/>.
    /// </summary>
    internal class ProgramException : Exception
    {
        public ProgramException()
        {
        }

        // ---- CONSTRUCTORS & DESTRUCTOR ------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramException"/> class with the given
        /// <paramref name="message"/>.
        /// </summary>
        /// <param name="message">The textual message to display.</param>
        internal ProgramException(string message) : base(message)
        {
        }
    }
}
