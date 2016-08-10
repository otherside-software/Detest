using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest.Mocha
{
    public class BddConfiguration : IFrameworkConfiguration
    {
        public string Analyzer
        { 
            get
            {
                return "Mocha/bdd_analyzer.js";
            }
        }

        public string Harness
        {
            get
            {
                return "Mocha/harness.html";
            }
        }

    }
}
