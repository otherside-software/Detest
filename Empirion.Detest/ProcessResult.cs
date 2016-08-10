using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest
{
    public class ProcessResult
    {
        public IEnumerable<string> Output { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        public int ExitCode { get; private set; }

        public ProcessResult(int statusCode, IEnumerable<string> output, IEnumerable<string> errors)
        {
            Output = output;
            Errors = errors;
        }
    }
}
