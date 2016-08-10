using Antlr4.StringTemplate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest.Tests
{
    [TestClass]
    public class Harnesses
    {

        [TestMethod]
        public void HarnessCreation()
        {
            var harness = PathFinder.GetApplicationPath("Mocha/harness.html");
            var templateString = File.ReadAllText(harness);

            var template = HarnessBuilder.Construct(templateString, new List<string>()
            {
                "TestAssets/mocha_bdd_math"
            });

            var resultPath = PathFinder.GetApplicationPath("harness.html");

            File.WriteAllText(resultPath, template);
        }
    }
}
