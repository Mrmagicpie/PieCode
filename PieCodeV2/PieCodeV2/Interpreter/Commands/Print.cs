/*
 *  PieCode Copyright (c) 2021 Mrmagicpie
 *
 *  File to hold the print command.
 *
 *  Namespace: PieCodeV2.Interpreter.Commands
 *  
 */


using System;


namespace PieCodeV2.Interpreter.Commands
{
    public partial class Commands
    {
        public static void print(string content)
        {
            if (content.Contains("\\n"))
            { content = content.Replace("\\n", Environment.NewLine); }
            
            Console.WriteLine(content);
            
        }
    }
}