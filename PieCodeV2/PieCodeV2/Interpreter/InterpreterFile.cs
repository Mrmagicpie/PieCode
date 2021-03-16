/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 *
 * 
 *  Basic file to hold the InterpreterFile class.
 *
 *   This class holds information about the current
 *  file being interpreted. Please refer to the methods
 *  inside the class for more information about what it
 *  does.
 *
 * 
 *  Namespace: PieCodeV2.Interpreter
 * 
 */


using System;
using System.Collections.Generic;
using System.IO;    
using PieCodeV2.Errors;


namespace PieCodeV2.Interpreter
{
    /// <summary>
    /// Public class to represent a file being interpreted.
    /// </summary>
    public class InterpreterFile
    {
        /// <summary>
        /// A private dictionary for internal aliases.
        /// </summary>
        private static Dictionary<string, string> ALIASES = new Dictionary<string, string>()
        {
            {"ded", "exit"}
        };

        /// <summary>
        /// The file path to the current file.
        /// </summary>
        public string file;
        
        /// <summary>
        /// The contents of the file.
        /// </summary>
        public string[] contents;

        /// <summary>
        /// Set the file to the file you want to use.
        /// </summary>
        /// <param name="fileName">The filename of the file they want to interpret.</param>
        /// <exception cref="FileNotValid">The file is not a .pie file.</exception>
        /// <exception cref="FileNotFound">The file is not found in the current dir.</exception>
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
}