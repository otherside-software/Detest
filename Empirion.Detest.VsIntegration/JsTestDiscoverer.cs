using Empirion.Detest.Interop;
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
            var configuration = new Mocha.BddConfiguration(); //TODO: unhardcode
            var tests = GetTests(configuration, sources);

            foreach (var test in tests)
            {
                discoverySink.SendTestCase(test);
            }

        }

        public static IEnumerable<TestCase> GetTests(IFrameworkConfiguration configuration, IEnumerable<string> sources)
        {
            var analyzer = PathFinder.GetApplicationPath(configuration.Analyzer);

            return GetTests(analyzer, sources).ToList();
        }
        
        private static IEnumerable<TestCase> GetTests(string analyzer, IEnumerable<string> sources)
        {
            foreach (var source in sources)
            {
                var result = ProcessRunner.RunAnalyzer(analyzer, source);
                foreach (var line in result.Output)
                {
                    var testDescription = MessageParser.ParseTestMessage(line);
                    yield return new TestCase(testDescription.FullyQualifiedName, JsTestExecutor.ExecutorUri, source)
                    {
                        CodeFilePath = source,
                        DisplayName = testDescription.Test,
                        LineNumber = testDescription.Line
                    };
                }
            }
        }
    }
}