using System;
using System.Collections.Generic;
using System.Linq;

namespace Empirion.Detest.Interop
{
    public class TestDescription
    {
        public List<string> Suite { get; set; }
        public string Test { get; set; }

        public string FullyQualifiedName
        {
            get
            {
                return String.Join(" ", Suite.Concat(new[] { Test }));
            }
        }

        public bool Passed { get; set; }
        public int Duration { get; set; }

        public int Line { get; set; }
      
    }
}