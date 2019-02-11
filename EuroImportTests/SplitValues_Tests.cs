using Microsoft.VisualStudio.TestTools.UnitTesting;
using EuroImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EuroImport.Exceptions;

namespace EuroImport.Tests
{
    [TestClass()]
    public class SplitValues_Tests
    {
        protected string delimeter = "|";
        [TestMethod()]
        public void SplitValuesTest_StringEmpty_ReturnEmpty()
        {
            SplitValues split = new SplitValues(delimeter);
            var values = split.Split(string.Empty);
            Assert.AreEqual(0, values.Count());
        }
        [TestMethod()]
        public void SplitValuesTest_OneValue_ReturnValue()
        {
            SplitValues split = new SplitValues(delimeter);
            var values = split.Split("shoes");
            Assert.AreEqual(1, values.Count());
            Assert.AreEqual("shoes", values.ElementAt(0));
        }
        [TestMethod()]
        public void SplitValuesTest_TwoValues_ReturnValues()
        {
            SplitValues split = new SplitValues(delimeter);
            var values = split.Split("shoes|sandals");
            Assert.AreEqual(2, values.Count());
            Assert.AreEqual("shoes", values.ElementAt(0));
            Assert.AreEqual("sandals", values.ElementAt(1));
        }
        [TestMethod()]
        public void SplitValuesTest_ThreeValues_ReturnValues()
        {
            SplitValues split = new SplitValues(delimeter);
            var values = split.Split("shoes|sandals|ball");
            Assert.AreEqual(3, values.Count());
            Assert.AreEqual("shoes", values.ElementAt(0));
            Assert.AreEqual("sandals", values.ElementAt(1));
            Assert.AreEqual("ball", values.ElementAt(2));
        }

        [ExpectedException(typeof(WrongDelimeterException))]
        [TestMethod()]
        public void SplitValuesTest_TwoValuesWrongDelimeterComma_ThrowException()
        {
            SplitValues split = new SplitValues(delimeter);
            var values = split.Split("shoes,sandals");
        }
        [ExpectedException(typeof(WrongDelimeterException))]
        [TestMethod()]
        public void SplitValuesTest_TwoValuesWrongDelimeterDiv_ThrowException()
        {
            SplitValues split = new SplitValues(delimeter);
            var values = split.Split("shoes,sandals");
        }
    }
}