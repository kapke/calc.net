using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Calc.Operation;

namespace CalcTest {
    [TestClass]
    public class CalcTest {
        private Calc.Calc calc;

        [TestInitialize]
        public void prepareCalc () {
            List<BinaryOperation> operations = new List<BinaryOperation>();
            operations.Add(new Multiplication());
            operations.Add(new Division());
            operations.Add(new Addition());
            operations.Add(new Subtraction());
            this.calc = new Calc.Calc(operations);
        }
        [TestMethod]
        public void AdditionTest () {
            List<Double> results = new List<Double>();
            results.Add(this.calc.calculate("1+2"));
            results.Add(this.calc.calculate("2+1"));
            results.Add(this.calc.calculate("(1+2)"));
            results.Add(this.calc.calculate("(2+1)"));
            results.Add(this.calc.calculate("3+0"));
            results.Add(this.calc.calculate("0+3"));
            results.Add(this.calc.calculate("(3+0)"));
            results.Add(this.calc.calculate("(0+3)"));
            results.Add(this.calc.calculate("1.0+2"));
            results.Add(this.calc.calculate("2.0+1"));
            results.Add(this.calc.calculate("(1.0+2)"));
            results.Add(this.calc.calculate("(2.0+1)"));
            results.Add(this.calc.calculate("1.5+1.5"));
            results.Add(this.calc.calculate("3.0+0"));
            foreach (Double result in results) {
                Assert.AreEqual(3, result);
            }
        }

        [TestMethod]
        public void SubtractionTest () {
            Assert.AreEqual(-1, this.calc.calculate("1-2"));
            Assert.AreEqual(1, this.calc.calculate("2-1"));
            Assert.AreEqual(-1, this.calc.calculate("(1-2)"));
            Assert.AreEqual(1, this.calc.calculate("(2-1)"));
        }

        [TestMethod]
        public void MultiplicationTest () {
            List<Double> results = new List<Double>();
            results.Add(this.calc.calculate("1*2"));
            results.Add(this.calc.calculate("2*1"));
            results.Add(this.calc.calculate("(1*2)"));
            results.Add(this.calc.calculate("(2*1)"));
            foreach (Double result in results) {
                Assert.AreEqual(2, result);
            }
        }

        [TestMethod]
        public void DivisionTest () {
            Assert.AreEqual(0.5, this.calc.calculate("1/2"));
            Assert.AreEqual(2, this.calc.calculate("2/1"));
            Assert.AreEqual(0.5, this.calc.calculate("(1/2)"));
            Assert.AreEqual(2, this.calc.calculate("(2/1)"));
        }

        [TestMethod]
        public void OperationPrecedenceTest () {
            Assert.AreEqual(6, this.calc.calculate("2+2*2"));
            Assert.AreEqual(8, this.calc.calculate("(2+2)*2"));
        }
    }
}
