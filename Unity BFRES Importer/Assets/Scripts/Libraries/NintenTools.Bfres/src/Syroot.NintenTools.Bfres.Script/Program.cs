using System;
using System.IO;
using System.Reflection;
//using Microsoft.CodeAnalysis.CSharp.Scripting; //Removed for OS X 
//using Microsoft.CodeAnalysis.Scripting;
using Syroot.Maths;

namespace Syroot.NintenTools.Bfres.Script
{
    /// <summary>
    /// Represents the main class of the application containing the program entry point.
    /// </summary>
    internal class Program
    {
        // ---- FIELDS -------------------------------------------------------------------------------------------------

        private static string _scriptFileName;
        private static string _resFileName;
        private static string[] _scriptArgs;

        // ---- METHODS (PRIVATE) --------------------------------------------------------------------------------------

        private static int Main(string[] args)
        {
            try
            {
                ParseArguments(args);
                RunScript();
                return 0;
            }
            catch (ProgramException ex)
            {
                LogLine(ex.Message, ConsoleColor.Red);
                return 1;
            }
        }

        private static void ParseArguments(string[] args)
        {
            // Check if the minimum required number of arguments have been passed - if not, show help.
            if (args.Length < 2)
            {
                Console.WriteLine("Runs a C# script compiled at runtime with access to the NintenTools.Bfres API.");
                Console.WriteLine();
                Console.WriteLine("RESSCRIPT SCRIPT BFRES [ARGS]");
                Console.WriteLine();
                Console.WriteLine("        SCRIPT   	The text file to parse as a script.");
                Console.WriteLine("        BFRES    	A BFRES file which can be accessed in the script.");
                Console.WriteLine("        ARGS     	Optional arguments passed to the script.");
                throw new ProgramException(String.Empty);
            }

            // Get the script file name.
            _scriptFileName = args[0];
            if (!File.Exists(_scriptFileName))
            {
                throw new ProgramException($"Script file \"{_scriptFileName}\" not found. Ensure the file exists.");
            }

            // Get the BFRES file name.
            _resFileName = args[1];
            if (!File.Exists(_resFileName))
            {
                throw new ProgramException($"BFRES file \"{_resFileName}\" not found. Ensure the file exists.");
            }

            // Get any additional parameters.
            if (args.Length > 2)
            {
                _scriptArgs = new string[args.Length - 2];
                args.CopyTo(_scriptArgs, 2);
            }
            else
            {
                _scriptArgs = new string[0];
            }
        }

        private static void RunScript()
        {
            // Load the script code.
            string scriptSource = File.ReadAllText(_scriptFileName);

            // Load the BFRES file.
            LogLine($"Loading BFRES file {Path.GetFileName(_resFileName)}...");
            ResFile resFile = new ResFile(_resFileName);
            
            // Create global variables which are accessible in the script.
            Globals globals = new Globals()
            {
                ScriptFileName = _scriptFileName,
                ResFileName = _resFileName,
                ResFile = resFile,
                Args = _scriptArgs
            };

            // Compile the script and run it.
            LogLine($"Running script \"{Path.GetFileName(_scriptFileName)}\"...");
            LogLine();
            Console.ForegroundColor = ConsoleColor.White;
//            try
//            {
//                Script<object> script = CSharpScript.Create(scriptSource, ScriptOptions.Default
//                    .WithReferences(
//                        typeof(ResFile).GetTypeInfo().Assembly,
//                        typeof(Algebra).GetTypeInfo().Assembly)
//                    .WithImports(
//                        "System",
//                        "System.Console",
//                        "Syroot.Maths",
//                        "Syroot.NintenTools.Bfres",
//                        "Syroot.NintenTools.Bfres.GX2",
//                        "Syroot.NintenTools.Bfres.Helpers"),
//                    globalsType: typeof(Globals));
//                script.Compile();
//                (script.RunAsync(globals)).Wait();
//            }
//            catch (CompilationErrorException ex)
//            {
//                throw new ProgramException(ex.Message);
//            }
            LogLine();
            Console.WriteLine("Script executed successfully.", ConsoleColor.Green);
        }

        private static void LogLine(string message = null, ConsoleColor? color = null)
        {
            if (color.HasValue)
            {
                Console.ForegroundColor = color.Value;
            }
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}