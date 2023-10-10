

using System.Data;

namespace LvivskaDB
{
    // See https://aka.ms/new-console-template for more information

    internal class Program
    {
        private const string exitCommand = "exit";

        private static List<User> users = new List<User>();

        private static Interpreter interpreter = new Interpreter();
        
        static void Main()
        {
            int? var = null;

            var tmp = var != null ? null : var;

            var prg = new Program();
            while(true)
            {
                Console.Write("LvivskaDB> ");
                var input = Console.ReadLine();

                if (input == null)
                    continue;

                if(input.StartsWith('.'))
                {
                    prg.ProcessMetaCommand(input.Substring(1, input.Length - 1));
                }
                else if(input.ToLower().StartsWith("set"))
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
                //Console.WriteLine("do select");
                foreach (var user in users) 
                {
                    Console.WriteLine($"{user.Id} {new string(user.Name)} {new string(user.Email)}");
                }
                return;
            }
            if (simpleCommand.StartsWith("insert"))
            {
                //Query query = new Insert(simpleCommand);
                //users.Add(query.Execute());

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

    internal class User
    {
        public Int32 Id;
        public char[] Name;
        public char[] Email;

        internal User()
        {

        }

        internal User(Int32 id, char[] name, char[] email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }

    public class Interpreter
    {
        public Context Context { get; set; }

        Expression expressionChain { get; set; }

        public Interpreter()
        {
            Context = new Context();
        }

        //set.CreateTable(Users).Columns(Id: int, Name: string)
        public Interpreter Build(string[] input)
        {
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i].ToLower().StartsWith("set"))
                    continue;

                if (input[i].ToLower().StartsWith("createtable"))
                {
                    CreateTableExpression exp = new CreateTableExpression(Context, input[i]);
                    if((input.Length - 1) > i && input[i + 1].ToLower().StartsWith("columns"))
                    {
                        exp.NextExpression = new ColumnsExpression(Context, exp.TableName, input[i + 1].Split('(', ')'));
                    }
                    expressionChain = exp;
                }
            }
            return this;
        }

        public bool Execute()
        {
            expressionChain.Execute();
            return true;
        }
    }

    public class Context
    {
        //public string Input { get; set; }
        public DataSet ds { get; set; }

        public Context()
        {
            ds = new DataSet();
        }

    }

    public abstract class Expression
    {
        public Context Context { get; set; }
        public Expression NextExpression { get; set; }
        public abstract void Execute();
    }


    //CreateTable(Users)
    public class CreateTableExpression : Expression
    {
        public string TableName { get; set; }
        public CreateTableExpression(Context context, string command)
        {
            Context = context;
            TableName = command.Split('(', ')')[1];
        }

        public override void Execute()
        {
            Context.ds.Tables.Add(TableName);
            if(NextExpression != null)
                NextExpression.Execute();
        }
    }

    //Columns(Id: int, Name: string)
    public class ColumnsExpression : Expression
    {
        private string tableName;
        private string[] columnList;
        
        public ColumnsExpression(Context context, string tableName, string[] input)
        {
            Context = context;
            this.tableName = tableName;
            columnList = input[1].Split(',');
        }

        //Can't correctly parse column type
        public override void Execute()
        {
            var dt = Context.ds.Tables[tableName];
            if(dt == null)
            {
                throw new Exception($"Table: {tableName} is not present");
            }

            foreach (var col in columnList)
            {
                var args = col.Split(": ");
                var type = Type.GetType(args[1]);
                
                if (type == null)
                    continue;

                dt.Columns.Add(args[0], type);
            }
        }
    }
}