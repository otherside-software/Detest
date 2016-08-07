using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Empirion.Detest
{
    [DefaultExecutorUri(JsTestExecutor.ExecutorUriString)]
    [FileExtension(".js")]
    public class JsTestDiscoverer : ITestDiscoverer
    {
        public JsTestDiscoverer()
        {
            //Thread.Sleep(10000);
        }

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
            var analyzer = PathFinder.GetApplicationPath("mocha_bdd_analyzer.js");

            var result2 = ProcessRunner.RunProcess("node", "-v");
            foreach(var source in sources)
            {
                var result = ProcessRunner.RunAnalyzer(analyzer, source);
                foreach (var line in result.Output)
                {
                    yield return new TestCase(line, JsTestExecutor.ExecutorUri, source);
                }
            }
        }
    }
}