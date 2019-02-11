using Microsoft.VisualStudio.TestTools.UnitTesting;
using EuroImport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EuroImport.Tests
{
    [TestClass()]
    public class CombineValuesTests
    {
        string delimeter = "|";
        [TestMethod()]
        public void CombinesTest_Empty_ReturnEmpty()
        {
            CombineValues combine = new CombineValues(delimeter);
            Assert.AreEqual(string.Empty, combine.Combines(new List<string> { string.Empty }));
        }
        [TestMethod()]
        public void CombinesTest_OneValue_ReturnValue()
        {
            string value = "ball";
            CombineValues combine = new CombineValues(delimeter);
            Assert.AreEqual(value, combine.Combines(new List<string> { value }));
        }
        [TestMethod()]
        public void CombinesTest_TwoValues_ReturnValues()
        {
            List<string> values = new List<string> { "ball" ,"women"};
            CombineValues combine = new CombineValues(delimeter);
            var result = combine.Combines(values.ToArray());
            Assert.AreEqual("ball|women", result);
        }
        [TestMethod()]
        public void CombinesTest_ThreeValues_ReturnValues()
        {
            List<string> values = new List<string> { "ball", "women", "men" };
            CombineValues combine = new CombineValues(delimeter);
            var result = combine.Combines(values.ToArray());
            Assert.AreEqual("ball|women|men", result);
        }
        [TestMethod()]
        public void CombinesTest_ThreeValuesRemoveDuplicates_ReturnValuesNoDuplicates()
        {
            List<string> values = new List<string> { "ball", "women", "women" };
            CombineValues combine = new CombineValues(delimeter);
            var result = combine.Combines(values.ToArray());
            Assert.AreEqual("ball|women", result);
        }
    }
}