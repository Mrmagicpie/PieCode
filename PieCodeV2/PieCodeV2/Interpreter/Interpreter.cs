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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
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
            {
                interpreterFile.SetFile(file);
            }
            catch (FileNotValid)
            {
                Console.WriteLine("\nProgramming Error:");
                Console.WriteLine("You can only use a .pie file!\n");
                return;
            }

            try
            {
                interpreterFile.contents = File.ReadAllLines(interpreterFile.file);
            }
            catch (SecurityException)
            {
                Console.WriteLine("\nSystem Error:");
                Console.WriteLine("You do not have permission to execute this file!\n");
                return;
            }

            int line = 0;
            // char[] split   = { ' ', '<', '>' };
            // char[] split   = {' '};
            // string last_func;

            foreach (var lines in interpreterFile.contents)
            {
                // Console.WriteLine(lines);

                // Set things to ignore while running the program.
                if (
                    string.IsNullOrWhiteSpace(lines)      ||
                    string.IsNullOrWhiteSpace(lines)      ||
                    lines.StartsWith(";")                 ||
                    (line == 0 && lines.StartsWith("#!"))
                )
                { line++; continue; }

                try
                {
                    string temp_lines;

                    if (!lines.EndsWith(" "))
                    { temp_lines = lines + " "; }
                    else
                    { temp_lines = lines; }

                    string[] temp_line;

                    temp_line = temp_lines.Split("<", 2);
                    // temp_line                 =  temp_lines.Split(split, 2);
                    // object[] parameters       =  { temp_line[1] };

                    // I forgot why I did this but leaving it commented in case.
                    // int param_count = command.GetParameters().Length;
                    // foreach (var par in command_par)
                    // {
                    //     if (par.IsOptional)
                    //     {
                    //         param_count--;
                    //     }
                    // }

                    // TODO: Make this actually be able to process not only the first thing
                    MethodInfo command       =  typeof(Commands.Commands).GetMethod(temp_line[0]);
                    List<object> parameters  =  new List<object>();
                    int para_int             =  0;

                    foreach (var parameter in command.GetParameters())
                    {
                        try
                        {
                            parameters.Add(temp_line[para_int + 1]);
                            para_int++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Syntax error:\n Mismatch arguments!");
                            return;
                            // throw new Exception();
                        }
                    }

                    object[] new_para = parameters.ToArray();
                    command.Invoke(null, new_para);

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