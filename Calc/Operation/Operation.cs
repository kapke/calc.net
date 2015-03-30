using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Operation {
    public interface Operation {
        String Symbol {get; }
    }
    public interface BinaryOperation : Operation {
        Double calculate (Double arg1, Double arg2);
    }
}
