using Calc.Operation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calc
{
    public class Calc
    {
        /// <summary>
        /// Standard operations:
        ///   - Multiplication
        ///   - Division
        ///   - Addition
        ///   - Subtraction
        /// </summary>
        public static List<BinaryOperation> standardOperations = new List<BinaryOperation>(new BinaryOperation[] {
            new Multiplication(),
            new Division(),
            new Addition(),
            new Subtraction()
        });

        /// <summary>
        /// Operations used by calculator instance to parse and evaluate expressions
        /// </summary>
        private List<BinaryOperation> binaryOperations;

        /// <summary>
        /// Initializes calculator instance
        /// </summary>
        /// <param name="binaryOperations">List of operations used by calculator. Operator precedence is strongly connected with operations order on given list.</param>
        public Calc (List<BinaryOperation> binaryOperations) {
            this.binaryOperations = binaryOperations;
        }

        /// <summary>
        /// Initializes calculator instance using `Calc.standardOperations` operations list
        /// </summary>
        public Calc () {
            this.binaryOperations = standardOperations;
        }

        /// <summary>
        /// Evaluates given string.
        /// </summary>
        /// <param name="expression">Expression to evaluate</param>
        /// <returns>Result of given expression</returns>
        public Double calculate (String expression) {
            Boolean changed = true;
            while (changed) {
                changed = false;
                foreach (BinaryOperation operation in this.binaryOperations) {
                    String reduced = this.reduce(expression, operation);
                    if (expression != reduced) {
                        changed = true;
                        expression = reduced;
                    }
                }
            }
            Double output;
            if (Double.TryParse(expression, out output)) {
                return output;
            } else {
                throw new EvaluationExcpetion();
            }
        }

        /// <summary>
        /// Reduces simple operations in given string to evaluated numbers. 
        /// </summary>
        /// <example>
        /// String "2+2*2" with given Addition operation will be reduced to "4*2"
        /// </example>
        /// <param name="full">Full expression</param>
        /// <param name="operation">Binary operation used to evaluate</param>
        /// <returns>Expression with reduced operations</returns>
        private String reduce (String full, BinaryOperation operation) {
            Regex operationRegex = this.buildRegex(operation);
            String newFull = full;
            while(true) {
                Match match = operationRegex.Match(newFull);
                if(match.Success) {
                    Tuple<Double, Double> arguments = parseMatch(match, operation);
                    Double value = operation.calculate(arguments.Item1, arguments.Item2);
                    newFull = full.Remove(match.Index, match.Length).Insert(match.Index, value.ToString());
                } else {
                    break;
                }
            }
            return newFull;
        }

        /// <summary>
        /// Parses given Match to tuple of doubles representing first and second operation argument
        /// </summary>
        /// <param name="match">Match to parse</param>
        /// <param name="operation">Operation used to find and parse given match</param>
        /// <returns>tuple of doubles conatining operation arguments</returns>
        private Tuple<Double, Double> parseMatch (Match match, BinaryOperation operation) {
            String[] arguments = match.Value.Split(new String[] { operation.Symbol }, StringSplitOptions.None);
            for (int i = 0; i < 2; i++) {
                arguments[i] = arguments[i].Replace("(", String.Empty);
                arguments[i] = arguments[i].Replace(")", String.Empty);
            }
            return new Tuple<Double, Double>(
                Double.Parse(arguments[0], CultureInfo.InvariantCulture),
                Double.Parse(arguments[1], CultureInfo.InvariantCulture)
                );
        }

        /// <summary>
        /// Builds regexp used to find simple operations in expression
        /// </summary>
        /// <param name="operation">Operation used to create regexp and then parse and evaluate expression</param>
        /// <returns>Regexp</returns>
        private Regex buildRegex (BinaryOperation operation) {
            String number = @"-?\d+(\.\d+)?";
            String simpleOperation = String.Format(@"{0}\{1}{0}", number, operation.Symbol);
            String fullOperation = String.Format(@"({0})|(\({0}\))", simpleOperation);
            return new Regex(fullOperation);
        }
    }

    public class EvaluationExcpetion : Exception {}
}
