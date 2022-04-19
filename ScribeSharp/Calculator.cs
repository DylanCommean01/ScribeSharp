using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScribeSharp
{
    public enum Symbols
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    class Calculator
    {
        private List<Double> _numbers;
        private List<Symbols> _symbols;

        public Calculator(List<Double> numbers, List<Symbols> symbols) 
        {
            _numbers = numbers;
            _symbols = symbols;
        }

        public List<Double> Numbers { get; set; }
        public Double Calculate() 
        {
            // Will implement this later.
            return 0;    
        }
    }
}
