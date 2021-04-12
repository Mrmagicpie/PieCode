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

// System imports
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security;
using System.Threading.Tasks;

// Extra Misc imports
using PieCodeV2.Errors;


namespace PieCodeV2.Interpreter
{
    /// <summary>
    /// Class to hold the main interpreter.
    /// </summary>
    public partial class Interpreter
    {
        /// <summary>
        /// System error handling for reading a file.
        /// </summary>
        /// <param name="error">Exception from a catch.</param>
        /// <returns>Return print statements in the proper format.</returns>
        protected static async Task FileException(object error)
        {
            Console.WriteLine("\nSystem error:");
            Console.WriteLine("An error happened while trying to read your file:\n{0}", error);
        }
        
        /// <summary>
        /// Begin interpreting the file.
        /// </summary>
        /// <param name="interpreterFile">An InterpreterFile object.</param>
        /// <returns>Returns your code interpreted.</returns>
        private static async Task Interprete(InterpreterFile interpreterFile, string file)
        {
            // Try to set the current file to the one provided via command line
            try
            { interpreterFile.SetFile(file); }
            catch (FileNotValid)
            {
                Console.WriteLine("\nProgramming Error:");
                Console.WriteLine("You can only use a .pie file!\n");
                return;
            }

            // Try to open the file provided then save in the contents list
            try
            { interpreterFile.contents = File.ReadAllLines(interpreterFile.file); }
            // So many catch's lmao
            catch (SecurityException)
            {
                Console.WriteLine("\nSystem Error:");
                Console.WriteLine("You do not have permission to execute this file!\n");
                return;
            }
            catch (UnauthorizedAccessException e)
            { await FileException(e); return; }
            catch (ArgumentException e)
            { await FileException(e); return; }
            catch (PathTooLongException e)
            { await FileException(e); return; }
            catch (DirectoryNotFoundException e)
            { await FileException(e); return; }

            // Keep track of the current line in the line variable
            int line = 0;
            
            // Old split stuff just in case
            // char[] split   = { ' ', '<', '>' };
            // char[] split   = {' '};
            // string last_func;

            // Line number + char number
            Dictionary<string, string> STRINGS = new Dictionary<string, string>() 
            { {"0-100", "ded"} };
            Dictionary<string, bool>   CONTEXT = new Dictionary<string, bool>()
            { {"string", false}, {"func", false} };
            
            string LAST_STRING = "";

            // Loop over each line in the contents list
            foreach (var lines in interpreterFile.contents)
            {
                // Console.WriteLine(lines);

                // Set things to ignore while running the program.
                if (
                    string.IsNullOrWhiteSpace(lines)      ||
                    string.IsNullOrWhiteSpace(lines)      ||
                    lines.StartsWith(";")                 ||
                    (line == 0 && lines.StartsWith("#!"))
                )  // Add one to the line int variable(for lines) and then go to the next loop
                { line++; continue; }

                var char_num = 0;
                foreach (var character in lines)
                {
                    var letter = character.ToString();
                    // if (string.IsNullOrEmpty(letter) || string.IsNullOrWhiteSpace(letter))
                    // {
                    //     if (CONTEXT["string"]) { STRINGS[LAST_STRING] += letter; }
                    // }

                    if (letter == "\"")
                    {
                        // TODO: Add support for \" and '
                        string str_line   = line + "-" + char_num;
                        
                        // If we aren't in a string, create a new PieCode string in the dictionary,
                        // and set the last string var to the proper var. If we are, then unset the 
                        // last string var.
                        if (!CONTEXT["string"])
                        {
                            STRINGS[str_line] = "";
                            LAST_STRING       = str_line;
                        }
                        else
                        { LAST_STRING = ""; }
                        
                        // And then flip the string var to tell the interpreter if we're in a 
                        // string or not. 
                        CONTEXT["string"] = !CONTEXT["string"];
                    }
                    else if (CONTEXT["string"])
                    {
                        STRINGS[LAST_STRING] += letter; 
                    }
                    // Pass if it's blank
                    else if (string.IsNullOrWhiteSpace(letter) || string.IsNullOrEmpty(letter)) { }
                    
                    // TODO: Add the letter to a current operation var and then when we get to a blank space attempt to process that current operation. 
                    // or when we get to a < start function calling until we get to a >
                    else
                    {
                        Console.WriteLine("You are trying to do more than a string initialization!" + 
                                          "Yell at Pie to finish this!");
                    }

                    char_num++;
                }
                
                // TODO: Recode this to process after the line has been processed char-by-char 
                // try
                // {
                //     string temp_lines;
                //
                //     // Check if the line doesn't end with a space(for now this is necessary for the interpreter not to error)
                //     if (!lines.EndsWith(" "))
                //     { temp_lines = lines + " "; }
                //     else
                //     { temp_lines = lines; }
                //
                //     string[] temp_line;
                //
                //     // Split at the first call symbol, this will need to be redone for more advanced situations in the future
                //     temp_line = temp_lines.Split("<", 2);
                //     // temp_line                 =  temp_lines.Split(split, 2);
                //     // object[] parameters       =  { temp_line[1] };
                //
                //     // I forgot why I did this but leaving it commented in case.
                //     // int param_count = command.GetParameters().Length;
                //     // foreach (var par in command_par)
                //     // {
                //     //     if (par.IsOptional)
                //     //     {
                //     //         param_count--;
                //     //     }
                //     // }
                //
                //     // TODO: Make this actually be able to process not only the first thing
                //     
                //     // Find the first method in the line
                //     MethodInfo command       =  typeof(Commands.Commands).GetMethod(temp_line[0]);
                //     // Create a new list for the parameters later
                //     List<object> parameters  =  new List<object>();
                //     int para_int             =  0;
                //
                //     // Loop over all the methods parameters, then if they're not optional add them to the parameters list
                //     foreach (var parameter in command.GetParameters())
                //     {
                //         try
                //         {
                //             if (!parameter.IsOptional)
                //             {
                //                 parameters.Add(temp_line[para_int + 1]);
                //                 para_int++;
                //             }
                //         }
                //         catch (IndexOutOfRangeException)
                //         {
                //             Console.WriteLine("Syntax error:\n Mismatch arguments!");
                //             return;
                //             // throw new Exception();
                //         }
                //     }
                //
                //     // Change the parameter list to an array, then use that array to invoke the methods
                //     object[] new_para = parameters.ToArray();
                //     command.Invoke(null, new_para);
                //
                //     // Tell the interpreter we're on a new line
                //     line++;
                // }
                //
                // // Catch nullreference in case there isn't a function by that name
                // catch (NullReferenceException)
                // {
                //     Console.WriteLine("\nSyntax error at line {0}: function not found", line + 1);
                //     Console.WriteLine(lines);
                //     Console.WriteLine("^\n");
                //     break;
                // }
            }
        }
    }
}