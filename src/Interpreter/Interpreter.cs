using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Interpreter
{
    public class Interpreter
    {
        public Context Context { get; set; }

        Expression? expressionChain { get; set; }

        public Interpreter()
        {
            Context = new Context();
        }

        //set.CreateTable(Users).Columns(Id: int, Name: string)
        public Interpreter Build(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].ToLower().StartsWith("set"))
                    continue;

                if (input[i].ToLower().StartsWith("createtable"))
                {
                    CreateTableExpression exp = new CreateTableExpression(Context, input[i]);
                    if ((input.Length - 1) > i && input[i + 1].ToLower().StartsWith("columns"))
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
            if (expressionChain == null)
                return false;

            expressionChain.Execute();
            return true;
        }
    }
}