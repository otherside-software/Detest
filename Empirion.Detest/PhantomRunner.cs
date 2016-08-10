using Empirion.Detest.Interop;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest
{
    public class PhantomRunner
    {
        private const string PHANTOM_PATH = "phantomjs";
        private const string PHANTOM_DETEST_PATH = "detest_phantom.js";

        private IFrameworkHandle frameworkHandle;
        private IList<TestCase> testCache;

        public PhantomRunner(IFrameworkHandle frameworkHandle, IEnumerable<TestCase> tests)
        {
            this.frameworkHandle = frameworkHandle;
            this.testCache = tests.ToList();
        }

        public Task<ProcessResult> Run(string uri)
        {
            var arguments = String.Format(
               "\"{0}\" \"{1}\"",
               PHANTOM_DETEST_PATH,
               uri
            );

            var progress = new Progress<string>(HandleMessage);

            return ProcessRunner.RunProcessAsync(PHANTOM_PATH, arguments, progress);
        }

        public void HandleMessage(string data)
        {
            var message = MessageParser.ParseTestMessage(data);
            if (message != null)
            {
                var test = testCache.FirstOrDefault(t => t.FullyQualifiedName == message.FullyQualifiedName);

                if (test != null)
                {
                    var result = new TestResult(test)
                    {
                        DisplayName = test.DisplayName,
                        Duration = TimeSpan.FromMilliseconds(message.Duration),
                        Outcome = message.Passed ? TestOutcome.Passed : TestOutcome.Failed
                    };
                    frameworkHandle.RecordResult(result);
                }
                else
                {

                }
            }

        }
    }
}
