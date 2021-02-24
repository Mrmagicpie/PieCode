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


using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using PieCodeV2.Errors;


namespace PieCodeV2.Interpreter
{
    /// <summary>
    /// Class to hold the main interpreter.
    /// </summary>
    public partial class Interpreter
    {
        /// <summary>
        /// Begin interpreting the file.
        /// </summary>
        /// <param name="interpreterFile">An InterpreterFile object.</param>
        /// <returns>Returns your code interpreted.</returns>
        private static async Task Interprete(InterpreterFile interpreterFile, string file)
        {
            try
            { interpreterFile.SetFile(file); }
            catch (FileNotValid)
            {
                Console.WriteLine("\nProgramming Error:");
                Console.WriteLine("You can only use a .pie file!\n");
                return;
            }
            
            try
            { interpreterFile.contents = File.ReadAllLines(interpreterFile.file); }
            catch (SecurityException)
            {
                Console.WriteLine("\nSystem Error:");
                Console.WriteLine("You do not have permission to execute this file!\n");
                return;
            }

            int line = 0;
            foreach (var lines in interpreterFile.contents)
            {
                // Console.WriteLine(lines);
                string[] temp_line;
                string temp_lines;
                try
                {
                    if (!lines.StartsWith(";"))
                    {
                        if (!string.IsNullOrWhiteSpace(lines) || !string.IsNullOrWhiteSpace(lines))
                        {
                            if (!lines.EndsWith(" ")) {temp_lines = lines + " ";}
                            else { temp_lines = lines;}
                    
                            temp_line = temp_lines.Split(" ", 2);
                            object[] parameters = {temp_line[1]};
                            typeof(Commands.Commands).GetMethod(temp_line[0]).Invoke(null, parameters);   
                        }
                    }
                    line++;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("\nSyntax error at line {0}: function not found", line + 1);
                    Console.WriteLine(lines);
                    Console.WriteLine("^\n");
                    break;
                }
            }
        }
    }
}