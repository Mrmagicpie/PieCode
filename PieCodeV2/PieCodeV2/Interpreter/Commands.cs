/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 * 
 */

using System;

namespace PieCodeV2.Interpreter
{
    public class Commands
    {
        public static void print(string content)
        // TODO: Make this allow \n
        { Console.Write(content); }
        
        private static void Annoying(string person) 
        {
            Console.WriteLine("{0} is very annoying and should die!", person);
            return;
        }

        public static void annoying(string person)
        {
            person = person.Replace(" ", "");
            switch (person.ToLower())
            {
                case "pie": Annoying(person);
                    break;
                case "mrmagicpie": Annoying(person);
                    break;
                case "magic": Annoying(person);
                    break;
                default: Console.WriteLine("bruh {0} is so not annoying what are you talking about!", person);
                    break;
                
            }
        }
    }
}