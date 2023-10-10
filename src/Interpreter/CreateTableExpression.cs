using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    //input: CreateTable(Users)
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
            if (NextExpression != null)
                NextExpression.Execute();
        }
    }
}
