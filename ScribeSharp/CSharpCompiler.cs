using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    class CSharpCompiler
    {
        private string _code;
        private RosylnAPI _connection;

        public CSharpCompiler(string code, RosylnAPI connection)
        {
            _code = code;
            _connection = connection;
        }

        public string Code{ get; set; }

        public string Compile() {
            return _connection.Compile();
        }
    }
}
