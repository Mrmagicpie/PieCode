using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using PieCode.Shell.Commands;

namespace PieCode.Shell
{
    public class Shell
    {
        public static void StartShell()
        {
            if (Global.CDHOME)
            {
                Directory.SetCurrentDirectory(Global.HOME);
            }

            while (true)
            {
                Global.PWD = Directory.GetCurrentDirectory();

                if (Global.PWD.ToLower() == Global.HOME.ToLower())
                {
                    Global.PWD = "~";
                }
                else
                {
                    Global.PWD = Global.PWD.Replace(Global.HOME, "~");
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("{0}@{1} [ {2} ]: $ ", Global.USER, Global.HOSTNAME, Global.PWD);

                Global.INPUT = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(Global.INPUT))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(Global.INPUT))
                {
                    continue;
                }

                if (Global.INPUT.ToLower() == "exit")
                {
                    Console.WriteLine("\nExiting the shell!\n");
                    break;
                }
                else
                {
                    Command();
                }
            }
        }
        // Yes ik it can be private but if somehow someone wants to use this they can

        /// <summary>
        /// Command handler. Deals with all Shell commands.
        /// </summary>
        /// <returns>Calls a function based on the command given.</returns>
        public static void Command()
        {
            // Bool to stop the command function. I don't really know why its executing the if and else. Probably
            //                            because I'm using a global variable.
            bool running = true;

            foreach (var alias in Global.ALIASES.Keys)
            {
                if (Global.INPUT.StartsWith(alias))
                {
                    // TODO: Check if the alias is in the aliased command.
                    var regex = new Regex(Regex.Escape(alias));
                    Global.INPUT = regex.Replace(Global.INPUT, Global.ALIASES[alias], 1);
                }
            }

            // BuiltIn commands.
            if (Global.INPUT.ToLower().StartsWith("hostnamectl"))
            {
                running = false;
                BuiltIn.HostnameCTL();
            }

            if (Global.INPUT.StartsWith("ls"))
            {
                running = false;
                BuiltIn.LS();
            }

            if (Global.INPUT.StartsWith("cd"))
            {
                running = false;
                BuiltIn.CD();
            }

            if (Global.INPUT.StartsWith("clear"))
            {
                running = false;
                Console.Clear();
            }

            if (Global.INPUT.StartsWith("alias"))
            {
                running = false;
                BuiltIn.Alias();
            }

            if (Global.INPUT.StartsWith("help"))
            {
                running = false;
                var help = new StringBuilder()
                    .Append(" \n")
                    .Append("Mrmagicpie C# Shell Help!")
                    .Append(" \n")
                    .Append("Work in progress!")
                    .Append(" \n");
                Console.WriteLine(help);
            }

            // For everything else
            // TODO: Create a way to add commands in external classes.
            else
            {
                if (running)
                {
                    var none = new StringBuilder()
                        .Append(" \n")
                        .Append("Unknown command! Use \"help\" to get help!")
                        .Append(" \n");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(none);
                }
            }
        }
    }
}