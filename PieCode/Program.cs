﻿/*
 * Copyright (c) 2021 Mrmagicpie
 *
 *          PieCode
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;
using PieCode.Shell.Commands;
using PieCode.Shell;

namespace PieCode
{
    /// <summary>
    /// Class for all Shell wide variables.
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Set a Shell variable for the users home dir. Readonly because, yea.
        /// </summary>
        public static readonly string   HOME      = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        /// <summary>
        /// Shell variable for the hostname. Not readonly so you can change it per session.
        /// </summary>
        public static string            HOSTNAME  = Environment.MachineName;
        
        /// <summary>
        /// Readonly variable similar to the home dir because you only have one user per shell.
        /// </summary>
        public static readonly string   USER      = Environment.UserName;
        
        /// <summary>
        /// Current directory.
        /// </summary>
        public static string            PWD       = Directory.GetCurrentDirectory();

        /// <summary>
        /// Shell variable to change the way cd works.
        /// </summary>
        public static bool              CDHOME    = true;
        
        /// <summary>
        /// Raw command input to be used for collecting the raw input.
        /// </summary>
        public static string            RAWINPUT;
        
        /// <summary>
        /// Split command input to be used for command handling.
        /// </summary>
        public static string[]          INPUT;

        /// <summary>
        /// List of Shell wide aliases. 
        /// </summary>
        public static Dictionary<string, string> ALIASES = new Dictionary<string, string>(){
            {"l", "ls"}
        };

    }

    /// <summary>
    /// Main Shell Class. Contains everything needed to handle basic commands.
    /// </summary>
    public class PieCode
    {
        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                Console.WriteLine("wack");
            }
            else
            {
                Shell.Shell.StartShell();
            }
            
        } 
    }
}