

using Interpreter;
using System.Data;

namespace LvivskaDB
{
    // See https://aka.ms/new-console-template for more information

    internal class Program
    {
        private const string exitCommand = "exit";

        private static Interpreter.Interpreter interpreter = new Interpreter.Interpreter();


        static void Main()
        {
            var prg = new Program();
            while (true)
            {
                Console.Write("LvivskaDB> ");
                var input = Console.ReadLine();

                if (input == null)
                    continue;

                if (input.StartsWith('.'))
                {
                    prg.ProcessMetaCommand(input.Substring(1, input.Length - 1));
                }
                else if (input.ToLower().StartsWith("set"))
                {
                    interpreter
                        .Build(input.Split('.'))
                        .Execute();
                }
                else
                {
                    prg.ProcessSimpleCommand(input);
                }
            }
        }

        private void ProcessMetaCommand(string metaCommand)
        {
            //Console.WriteLine($"metaCommand: {metaCommand}");
            if (metaCommand == exitCommand)
                Environment.Exit(0);
            else
                Console.WriteLine($"metaCommand: {metaCommand}");
        }

        private void ProcessSetCommand(string input)
        {
            //CreateTable(User).Columns(Id:int32, Name:string)
            //context.SetInput(ref input);
        }

        private void ProcessSimpleCommand(string simpleCommand)
        {
            if (simpleCommand == "select")
            {
                Console.WriteLine("do select");
                return;
            }
            if (simpleCommand.StartsWith("insert"))
            {
                Console.WriteLine("do insert");
                return;
            }
            if (simpleCommand == "update")
            {
                Console.WriteLine("do update");
                return;
            }
            if (simpleCommand == "delete")
            {
                Console.WriteLine("do delete");
                return;
            }

            Console.WriteLine($"simpleCommand: {simpleCommand}");
        }
    }
}