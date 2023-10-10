using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class ColumnsExpression : Expression
    {
        private readonly string tableName;
        public string[] columnList { get; set; }

        public ColumnsExpression(Context context, string tableName, string[] input)
        {
            Context = context;
            this.tableName = tableName;
            columnList = input[1].Split(',');
        }

        //Can't correctly parse column type
        public override void Execute()
        {
            if(Context == null)
                return;

            var dt = Context.ds.Tables[tableName];
            if (dt == null)
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
