/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 *
 *  File to hold the "annoying" command.
 *
 *  Namespace: PieCodeV2.Interpreter.Commands
 *  
 */


using System;


namespace PieCodeV2.Interpreter.Commands
{
    public partial class Commands
    {
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