using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Operation {
    public class Multiplication : Operation, BinaryOperation {
        public String Symbol {
            get {
                return "*";
            }
        }
        public Double calculate (Double arg1, Double arg2) {
            return arg1 * arg2;
        }
    }
}
