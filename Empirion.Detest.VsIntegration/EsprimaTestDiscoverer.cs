using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System.Collections.Generic;

namespace Empirion.Detest
{
    [DefaultExecutorUri(JsTestExecutor.ExecutorUriString)]
    [FileExtension(".js")]
    public class JsTestDiscoverer : ITestDiscoverer
    {
        public void DiscoverTests(IEnumerable<string> sources, IDiscoveryContext discoveryContext, IMessageLogger logger, ITestCaseDiscoverySink discoverySink)
        {
            var tests = GetTests(sources);

            foreach (var test in tests)
            {
                discoverySink.SendTestCase(test);
            }

        }

        public static IEnumerable<TestCase> GetTests(IEnumerable<string> sources)
        {
            //TODO: make this configurable
            var analyzer = "mocha_bdd_analyzer.js";

            foreach(var source in sources)
            {
                var result = ProcessRunner.RunAnalyzer(analyzer, source);
                foreach(var line in result.Output)
                {
                    yield return new TestCase(line, JsTestExecutor.ExecutorUri, source);
                }
            }
        }
    }
}
