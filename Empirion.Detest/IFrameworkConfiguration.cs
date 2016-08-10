using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest
{
    public interface IFrameworkConfiguration
    {
        string Analyzer { get; }

        string Harness { get; }
    }
}
