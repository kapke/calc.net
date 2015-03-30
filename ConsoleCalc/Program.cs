using Calc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalc {
    class Program {
        private static Calc.Calc calc = new Calc.Calc();

        static void Main (string[] args) {
            Console.WriteLine(R.Calc);
            Console.WriteLine(R.PressEsc);
            Console.WriteLine(R.EnterExpression);
            StringBuilder bufferBuilder = new StringBuilder();
            do {
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key) {
                    case ConsoleKey.Enter:
                        String buffer = bufferBuilder.ToString();
                        try {
                            Double output = calc.calculate(buffer);
                            Console.WriteLine(R.Result);
                            Console.WriteLine(output);
                            Console.WriteLine(R.EnterExpression);
                        } catch (EvaluationExcpetion) {
                            Console.WriteLine(R.EvaluationError);
                            Console.WriteLine(R.EnterExpression);
                        }
                        bufferBuilder = new StringBuilder();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        bufferBuilder.Append(key.KeyChar);
                        break;
                }
            } while (true);
        }
    }
}
