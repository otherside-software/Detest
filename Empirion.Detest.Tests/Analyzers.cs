using Empirion.Detest.Interop;
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
            var tests = JsTestDiscoverer.GetTests(new Mocha.BddConfiguration(), new List<string> { "TestAssets/mocha_bdd_math.js" });
            Assert.AreEqual(4, tests.Count());
        }

        [TestMethod]
        public void Test_Message_Parse_Should_Succeed()
        {
            var parsed = MessageParser.ParseTestMessage("{ \"suite\":\"math multiplication\",\"test\":\"6*7 should equal 42\",\"passed\":true,\"duration\":75}");

            Assert.AreEqual("math multiplication", parsed.Suite);
            Assert.AreEqual("6*7 should equal 42", parsed.Test);
            Assert.AreEqual(true, parsed.Passed);
            Assert.AreEqual(75, parsed.Duration);
        }

        [TestMethod]
        public void Non_Test_Message_Parse_Should_Return_Null()
        {
            var parsed = MessageParser.ParseTestMessage("{\"close\":true}");

            Assert.IsNull(parsed);
        }
    }
}
