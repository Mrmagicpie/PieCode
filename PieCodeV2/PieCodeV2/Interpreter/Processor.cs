/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 *
 * 
 *  File to hold the guts of the main interpreter.
 *
 *    The interpreter class holds the main parts
 *  to process the file. It will check if it's
 *  even a file, and then start processing the
 *  contents of the file. This class is split
 *  in two files, Processor.cs and Interpreter.cs.
 *
 *
 *  Namespace: PieCodeV2.Interpreter
 * 
 */


using System.Threading.Tasks;

namespace PieCodeV2.Interpreter
{
    /// <summary>
    /// Class to hold the main interpreter.
    /// </summary>
    public partial class Interpreter
    {
        /// <summary>
        /// Check certain things to begin interpreting the file.
        /// </summary>
        /// <param name="args">Command line args from the exe.</param>
        /// <returns>Returns the Interpret method or another outcome if not a file.</returns>
        public static async Task Process(string[] args)
        {
            await Interprete(new InterpreterFile(), args[0]);
            // try
            // { if (File.Exists(args[0])) { await Interprete(new InterpreterFile(), args[0]); } }
            // catch (FileNotFoundException)
            // {
            //     Console.ForegroundColor = ConsoleColor.Red;
            //     Console.WriteLine();
            //     Console.WriteLine("Your file isn't found!");
            //     Console.WriteLine();
            //     return;
            // }
            // catch (Exception e)
            // {
            //     var uhoh = new StringBuilder()
            //         .Append(" \n")
            //         .Append("An unknown error has occurred!")
            //         .Append(" \n" + e)
            //         .Append(" \n");
            //     Console.ForegroundColor = ConsoleColor.Red;
            //     Console.WriteLine(uhoh);
            //     return;
            // }
            //
            // Console.ForegroundColor = ConsoleColor.Red;
            // Console.WriteLine();
            // Console.WriteLine("This option isn't currently supported!");
            // Console.WriteLine();
        }
    }
}