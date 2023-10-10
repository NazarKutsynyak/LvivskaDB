using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter
{
    public class Context
    {
        public DataSet ds { get; set; }

        public Context()
        {
            ds = new DataSet();
        }
    }
}
