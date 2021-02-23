/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;    
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
}