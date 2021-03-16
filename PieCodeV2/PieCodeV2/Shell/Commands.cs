/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 *
 * 
 *  Essential file holding the ShellCommands class. 
 *
 *    This file holds the ShellCommands class,
 *  which is an essential class in the shell.
 *  This holds the code for the commands that
 *  are usable at runtime.
 *
 *
 *  Namespace: PieCodeV2.Shell
 * 
 */


using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace PieCodeV2.Shell
{
    internal class ShellCommands
    {
        /// <summary>
        /// Run certain things for a proper Shell exit.
        /// </summary>
        public static void exit()
        {
            var exit = new StringBuilder()
                .Append(" \nExiting the shell! \n")
                .Append("Thank you for using PieCode! \n");
            Console.WriteLine(exit);
            Thread.Sleep(Global.DELAY);
        }
        
        /// <summary>
        /// Display the main help command.
        /// </summary>
        public static void help()
        {
            if (!string.IsNullOrWhiteSpace(Global.INPUT[1]))
            {
                try
                {
                    object method      = typeof(ShellCommands).GetMethod(Global.INPUT[1]);
                    object method_desc = typeof(ShellCommandsDocs).GetMethod(Global.INPUT[1]).Invoke(null, null);
                    Console.WriteLine();
                    Console.WriteLine("Help for:    {0}", Global.INPUT[1]);
                    Console.WriteLine("Description: {0}", method_desc);
                    var help = new StringBuilder()
                        .Append(" \nPieCode Help! \n")
                        .Append("Check out our Documentation for more information! \n")
                        .Append("https://docs." + Global.URL + " \n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(help);
                }
                catch (NullReferenceException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("No help available for: {0}", Global.INPUT[1]);
                    var help = new StringBuilder()
                        .Append(" \nPieCode Help! \n")
                        .Append("Check out our Documentation for more information! \n")
                        .Append("https://docs." + Global.URL + " \n");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(help);
                }
            }
            else
            {
                string[] excludes = {"porn"};
                Console.WriteLine();
                foreach (var method in typeof(ShellCommands).GetMethods())
                {
                    if (method.ReturnType == typeof(void) && !excludes.Contains(method.Name) 
                        || method.ReturnType == typeof(Task) && !excludes.Contains(method.Name))
                    {
                        object desc;
                        try
                        { desc = typeof(ShellCommandsDocs).GetMethod(method.Name).Invoke(null, null); }
                        catch (NullReferenceException)
                        { desc = "No description available!"; }

                        string name = method.Name;
                        if (method.Name.Length != 10)
                        {
                            int add = 10 - method.Name.Length;
                            string addto = "";
                            for (int i = 0; add >= i; i++)
                            { addto = addto + " "; }
                            name = method.Name + addto;
                        }
                        Console.WriteLine(name + "-  " + desc);
                    }
                }
                var help = new StringBuilder()
                    .Append(" \nPieCode Help! \n")
                    .Append("Check out our Documentation for more information! \n")
                    .Append("https://docs." + Global.URL + " \n");
                // Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(help);
            }
        }
    }
}