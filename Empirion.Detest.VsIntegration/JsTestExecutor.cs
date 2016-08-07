using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace Empirion.Detest
{
	[ExtensionUri(JsTestExecutor.ExecutorUriString)]
	public class JsTestExecutor : ITestExecutor
	{
        public const string ExecutorUriString = "executor://detest/v1";
        public static readonly Uri ExecutorUri = new Uri(ExecutorUriString);

        private bool cancelling = false;

		public void Cancel()
		{
			cancelling = true;
		}

		public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
            var tests = JsTestDiscoverer.GetTests(sources);
            RunTests(tests, runContext, frameworkHandle);
		}

		public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
		{
            foreach (var test in tests)
			{
				if (cancelling)
					break;

				var result = new TestResult(test);
				result.Outcome = test.FullyQualifiedName == "foo" ? TestOutcome.Passed : TestOutcome.Failed;
				frameworkHandle.RecordResult(result);
			}
		}
	}
}
