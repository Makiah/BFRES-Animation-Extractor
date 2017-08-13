namespace Syroot.NintenTools.Bfres.Script
{
    /// <summary>
    /// Represents the variables which can be accessed from anywhere in a script.
    /// </summary>
    public class Globals
    {
        // ---- PROPERTIES ---------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Gets the file name of the executed script.
        /// </summary>
        public string ScriptFileName { get; internal set; }

        /// <summary>
        /// Gets the file name of an optionally loaded <see cref="ResFile"/>.
        /// </summary>
        public string ResFileName { get; internal set; }

        /// <summary>
        /// Gets an optionally loaded <see cref="Bfres.ResFile"/> instance.
        /// </summary>
        public ResFile ResFile { get; internal set; }

        /// <summary>
        /// Gets the additional parameters passed to the script upon tool invocation.
        /// </summary>
        public string[] Args { get; internal set; }
    }
}
