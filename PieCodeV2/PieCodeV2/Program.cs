/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PieCodeV2
{
    /// <summary>
    /// Class for all Shell wide variables.
    /// </summary>
    public static class Global
    {
        
        /// <summary>
        /// Set a Shell variable for the users home dir. Readonly because, yea.
        /// </summary>
        public static readonly string HOME = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// Readonly variable similar to the home dir because you only have one user per shell.
        /// </summary>
        public static readonly string USER = "Mrmagicpie";
        // public static readonly string USER = Environment.UserName;

        /// <summary>
        /// Current directory.
        /// </summary>
        public static string PWD = Directory.GetCurrentDirectory();

        /// <summary>
        /// Shell variable to change the way cd works.
        /// </summary>
        public static bool CDHOME = true;

        /// <summary>
        /// Raw command input to be used for collecting the raw input.
        /// </summary>
        public static string RAWINPUT;

        /// <summary>
        /// Split command input to be used for command handling.
        /// </summary>
        public static string[] INPUT;

        /// <summary>
        /// List of Shell wide aliases. 
        /// </summary>
        public static Dictionary<string, string> ALIASES = new Dictionary<string, string>()
        {
            {"l", "ls"}, {"ded", "exit"}
        };

        /// <summary>
        /// Shell variable for the hostname. Not readonly so you can change it per session.
        /// </summary>
        public static string HOSTNAME  = Environment.MachineName;
        
        /// <summary>
        /// Shell delay for closing the Shell.
        /// </summary>
        public static readonly int DELAY = 5000;

        /// <summary>
        /// URL for things in the Shell.
        /// </summary>
        public static readonly string URL = "mrmagicpie.xyz/";
    }

    /// <summary>
    /// Main PieCode Class.
    /// </summary>
    class Program
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
            { await Shell.Shell.StartShell(); }
            else
            { await Interpreter.Interpreter.Process(args); }
        }
    }
}