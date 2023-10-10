using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public abstract class Expression
    {
        public Context? Context { get; set; }
        public Expression? NextExpression { get; set; }
        public abstract void Execute();
    }
}
