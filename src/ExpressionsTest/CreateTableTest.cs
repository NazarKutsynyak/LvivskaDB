using Interpreter;
using Xunit;

namespace ExpressionsTest
{
    public class CreateTableTest
    {
        
        [Fact]
        public void Test1()
        {
            Context context = new Context();
            CreateTableExpression expression = new CreateTableExpression(context, "CreateTable(Users)");

            Assert.Equal("Users", expression.TableName);
        }
    }
}