using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PieCode.Shell.Commands
{
    public class BuiltIn
    {
        public static void HostnameCTL()
        {
            if (Global.INPUT.StartsWith("hostnamectl --help"))
            {
                var help = new StringBuilder()
                    .Append(" \n")
                    .Append("HostnameCTL Help!")
                    .Append(" \n")
                    .Append("You will be asked for each option!")
                    .Append(" \n")
                    .Append("set   - Set your shell hostname to something.")
                    .Append("reset - Reset your shell's hostname.")
                    .Append(" \n");

                Console.WriteLine(help);
            }
            
            Console.Write("Please state what you'd like to do(use 'hostnamectl --help' for help): ");
            var kek = Console.ReadLine();
            
            if (string.IsNullOrEmpty(kek) || string.IsNullOrWhiteSpace(kek))
            { Console.WriteLine("Not a valid option!"); }
            
            if (kek.StartsWith("reset"))
            {
                Console.WriteLine("Resetting your hostname!");
                Global.HOSTNAME = Environment.MachineName;
            }
            
            if (kek.StartsWith("set"))
            {
                Console.Write("What would you like to set your Hostname to? ");
                var hostname = Console.ReadLine();
                if (string.IsNullOrEmpty(hostname) || string.IsNullOrWhiteSpace(hostname))
                { Console.WriteLine("Not a valid hostname!"); }
                else
                {
                    Global.HOSTNAME = hostname;
                    Console.WriteLine("Set your hostname to {0}", Global.HOSTNAME);
                }
            }
            else
            { Console.WriteLine("Not a valid option!"); }
        }

        public static void LS()
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo[] dirs = di.GetDirectories("*", SearchOption.TopDirectoryOnly);
            FileInfo[] files = di.GetFiles("*", SearchOption.TopDirectoryOnly);
            
            foreach (DirectoryInfo dir in dirs)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("{0,-25} {1,25}", dir.LastWriteTime , dir.FullName);
            }
            
            foreach (FileInfo file in files)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("{0,-25} {1,25}", file.LastWriteTime, file.Name);
            }
        }
        
        private static void FileNotFound(string path)
        {
            Console.WriteLine();
            Console.WriteLine("The file or directory can't be found!");
            Console.WriteLine("File/Directory: {0}", path);
            Console.WriteLine();
        }

        public static void CD()
        {
            var cd = Global.INPUT.Replace("cd", "");
            
            if (cd.StartsWith(" "))
            { cd = cd.Replace(" ", ""); }
            
            if (string.IsNullOrWhiteSpace(cd))
            {
                if (Global.CDHOME) 
                { Directory.SetCurrentDirectory(Global.HOME); }
                else 
                { Console.WriteLine("Please specify somewhere to cd into!"); }
            }
            
            else
            {
                try
                {
                    // TODO: Making this easier to cd into subdirs from home dir
                    if (cd.StartsWith("~"))
                    { Directory.SetCurrentDirectory(Global.HOME); }
                    else
                    { Directory.SetCurrentDirectory(cd); }
                }
                catch (FileNotFoundException)
                { FileNotFound(cd); }
                catch (DirectoryNotFoundException)
                { FileNotFound(cd); }
                catch (Exception e)
                {
                    Console.WriteLine("An error has occured!");
                    Console.WriteLine("Error: {0}", e);
                }
            }
        }

        /// <summary>
        /// Add an alias and write information to console.
        /// </summary>
        /// <param name="alias1">Your alias.</param>
        /// <param name="alias2">What the alias is running.</param>
        private static void add_alias(string alias1, string alias2)
        {
            if (Global.ALIASES.ContainsKey(alias1))
            {
                Console.WriteLine("Your alias has already been defined! It will be rewritten!");
                Console.WriteLine("Alias: {0}, Aliased to: {1}", alias1, alias2);
                Global.ALIASES[alias1] = alias2;
                Console.WriteLine("New alias '{0}' is aliased to '{1}'!", alias1, alias2); 
                // Global.ALIASES.Remove(alias1);
            }
            else
            {
                Global.ALIASES.Add(alias1, alias2);   
                Console.WriteLine("New alias '{0}' is aliased to '{1}'!", alias1, alias2); 
            }
        }

        public static void Alias()
        {
            var alias = Global.INPUT.Replace("alias", "");

            if (alias.StartsWith(" "))
            {
                var regex = new Regex(Regex.Escape(" "));
                alias = regex.Replace(alias, "", 1);
            }
            
            if (string.IsNullOrWhiteSpace(alias))
            {
                Console.Write("What would you like to alias: ");
                string alias1 = Console.ReadLine();
                if (string.IsNullOrEmpty(alias1) || string.IsNullOrWhiteSpace(alias1))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Not a valid alias name!");
                }
                else
                {
                    Console.Write("What would you like to alias '{0}' to: ", alias1);
                    string alias2 = Console.ReadLine();
                    if (string.IsNullOrEmpty(alias2) || string.IsNullOrWhiteSpace(alias2))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Not a valid alias!");
                    }
                    else
                    {
                        add_alias(alias1, alias2);
                    }
                }
            }
            else
            {
                string[] aliasv2 = alias.Split(" ");
                if (aliasv2.Length > 2)
                {
                    if (aliasv2[1].EndsWith(" "))
                    {
                        aliasv2[1] = aliasv2[1].Replace(" ", "");
                    }

                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("Too many arguments!");
                        Console.WriteLine();   
                    }
                }
                else
                {
                    if (aliasv2[0] == aliasv2[1])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("You can't alias a command to itself!");
                        Console.WriteLine();
                    }
                    else
                    {
                        // TODO: Use add_alias(aliasv2[0], aliasv2[1])
                        // Console.WriteLine("New alias '{0}' is aliased to '{1}'!", aliasv2[0], aliasv2[1]);  
                        // Global.ALIASES.Add(aliasv2[0], aliasv2[1]);
                        add_alias(aliasv2[0], aliasv2[1]);
                    }
                }
            }
        }
    }
}