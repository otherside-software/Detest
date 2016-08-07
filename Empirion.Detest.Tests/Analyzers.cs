using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest.Tests
{
    [TestClass]
    public class Analyzers
    {
        [TestMethod]
        public void MochaBddTestDiscovery()
        {
            var analyzer = PathFinder.GetApplicationPath("mocha_bdd_analyzer.js");
            var testfile = PathFinder.GetApplicationPath("TestAssets/mocha_bdd_math.js");
            var result = ProcessRunner.RunAnalyzer(analyzer, testfile);
            Assert.AreEqual(3, result.Output.Count());
        }

        [TestMethod]
        public void TestDiscovery()
        {
            var tests = JsTestDiscoverer.GetTests(new List<string> { "TestAssets/mocha_bdd_math.js" });
            Assert.AreEqual(3, tests.Count());
            
        }
    }
}
