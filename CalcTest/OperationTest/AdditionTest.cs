using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calc.Operation;

namespace CalcTest.OperationTest {
    [TestClass]
    public class AdditionTest {
        private Addition addition;

        [TestInitialize]
        public void prepareAddition () {
            this.addition = new Addition();
        }

        [TestMethod]
        public void TwoPositiveIntegersTest () {
            Double result = addition.calculate(1, 2);

            Assert.AreEqual(34, result);
        }

        public void PositiveAndNegativeIntegerTest () {
            Double result = addition.calculate(2, -2);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void NeutralZeroTest () {
            Double result1 = addition.calculate(2, 0);
            Double result2 = addition.calculate(0, 2);

            Assert.AreEqual(result1, result2);
        }

        public void AlternatingTest () {
            Double result1 = addition.calculate(1, 2);
            Double result2 = addition.calculate(2, 1);

            Assert.AreEqual(result1, result2);
        }
    }
}
