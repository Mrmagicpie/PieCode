/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using PieCodeV2.Errors;

namespace PieCodeV2.Interpreter
{
    // internal class FileNotFound : Exception {}
    
    public class InterpreterFile
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, string> ALIASES = new Dictionary<string, string>()
        {
            {"ded", "exit"}
        };

        public string file;
        public string[] contents;

        public void SetFile(string fileName)
        {
            try
            {
                if (!fileName.EndsWith(".pie"))
                { throw new Errors.FileNotValid(); }
                
                var tempFile = File.Open(fileName, FileMode.Open);
                tempFile.Close();
                file = Global.PWD + "/" + fileName;
            }
            catch (FileNotFoundException)
            { Console.ForegroundColor = ConsoleColor.Red; throw new Errors.FileNotFound(); }
            catch (FileNotValid)
            { throw new Errors.FileNotValid(); }
        }
        
        /// <summary>
        /// Dictionary of file based Variables.
        /// </summary>
        public static Dictionary<string, dynamic> VARIABLES = new Dictionary<string, dynamic>()
        {
            {"HOME", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}, 
            {"PWD", Directory.GetCurrentDirectory()},
            {"CDHOME", true},
            {"ALIASES", ALIASES},
            
            {"USER", "Mrmagicpie"},
            // {"USER", Environment.UserName},
            
            {"HOSTNAME", "Mrmagicpie"}
            // {"HOSTNAME", Environment.MachineName}
        };
    }
    
    /// <summary>
    /// Class to hold the main interpreter.
    /// </summary>
    public class Interpreter
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
                        if (!lines.EndsWith(" ")) {temp_lines = lines + " ";}
                        else { temp_lines = lines;}
                    
                        temp_line = temp_lines.Split(" ", 2);
                        object[] parameters = {temp_line[1]};
                        typeof(Commands).GetMethod(temp_line[0]).Invoke(null, parameters);
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