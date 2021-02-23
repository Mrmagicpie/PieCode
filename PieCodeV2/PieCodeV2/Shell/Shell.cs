/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 * 
 */

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieCodeV2.Shell
{
    public class Shell
    {
       /// <summary>
        /// Starts the main shell.
        /// </summary>
        /// <returns>Returns "never" ending shell instance.</returns>
        public static async Task StartShell()
        {
            // _handler += new EventHandler(Handler);
            // SetConsoleCtrlHandler(_handler, true);
            
            if (Global.CDHOME)
            { Directory.SetCurrentDirectory(Global.HOME); }

            while (true)
            {
                Global.PWD = Directory.GetCurrentDirectory();

                if (Global.PWD.ToLower() == Global.HOME.ToLower())
                { Global.PWD = "~"; }
                else
                { Global.PWD = Global.PWD.Replace(Global.HOME, "~"); }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("{0}@{1} [ {2} ]: $ ", Global.USER, Global.HOSTNAME, Global.PWD);

                Global.RAWINPUT = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(Global.RAWINPUT) || !string.IsNullOrEmpty(Global.RAWINPUT))
                {
                    if (!Global.RAWINPUT.EndsWith(" "))
                    { Global.RAWINPUT = Global.RAWINPUT + " "; }
                    
                    Global.INPUT      = Global.RAWINPUT.Split(" ");
                    Global.INPUT[0]   = Global.INPUT[0].ToLower();
                    // Console.WriteLine(Global.INPUT.Length);

                    if (Global.ALIASES.Keys.Contains(Global.INPUT[0]))
                    { Global.INPUT[0] = Global.ALIASES[Global.INPUT[0]]; }

                    if (Global.INPUT[0] == "exit")
                    { ShellCommands.exit(); break; }

                    await Command();

                }
                else
                { continue; }
            }
        }

        /// <summary>
        /// Private Shell command handler.
        /// </summary>
        /// <returns>Returns whatever the command is.</returns>
        private static async Task Command()
        {
            try
            { typeof(ShellCommands).GetMethod(Global.INPUT[0]).Invoke(null, null); }
            catch (NullReferenceException)
            {
                var unknown = new StringBuilder()
                    .Append(" \n")
                    .Append("Unknown command!")
                    .Append(" \n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(unknown);
            }
            catch (Exception e)
            {
                var uhoh = new StringBuilder()
                    .Append(" \n")
                    .Append("An unknown error has occurred!")
                    .Append(" \n" + e)
                    .Append(" \n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(uhoh);
            }
        } 
    }
}