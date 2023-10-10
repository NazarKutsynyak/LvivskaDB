using Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionsTest
{
    public class ColumnsExpressionTest
    {
        [Fact]
        public void Test1()
        {
            Context context = new Context();
            CreateTableExpression exp = new CreateTableExpression(context, "CreateTable(Users)");
            ColumnsExpression clmExp = new ColumnsExpression(context, exp.TableName, new string[] { "Columns", "Id: int, Name: string" });

            Assert.Equal(2, clmExp.columnList.Length);
        }
    }
}
