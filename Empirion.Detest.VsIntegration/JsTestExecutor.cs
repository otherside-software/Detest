using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Threading;
using System.ComponentModel.Composition;
using System.IO;
using Empirion.Detest.Interop;
using Empirion.Detest.WebHost;

namespace Empirion.Detest
{
    [ExtensionUri(JsTestExecutor.ExecutorUriString)]
    public class JsTestExecutor : ITestExecutor
    {
        public const string ExecutorUriString = "executor://detest/v1";
        public static readonly Uri ExecutorUri = new Uri(ExecutorUriString);

        public void Cancel()
        {
            //TODO: implement cancellation of running process
            // it appears threads are terminated as expected, but likely the external processes will still finish running.
            // this could become problematic if test runs are costly in terms of system performance.
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var configuration = new Mocha.BddConfiguration(); //TODO: unhardcode
            var tests = JsTestDiscoverer.GetTests(configuration, sources);

            RunTests(tests, runContext, frameworkHandle);
        }

        public void RunTests(IEnumerable<TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var configuration = new Mocha.BddConfiguration(); //TODO: unhardcode
            var harnessFile = "harness.html";

            var sources = tests
                .Select(t => t.Source)
                .Distinct();

            //TODO: allow multiple roots / multiple configs / multiple projects
            var rootPath = PathFinder.FindRootDirectoryForFile(sources.First());
            HarnessBuilder.CreateHarnessFile(configuration, sources, rootPath, harnessFile);

            var hostConfig = new Nancy.Hosting.Self.HostConfiguration()
            {
                RewriteLocalhost = true,
                UrlReservations = new Nancy.Hosting.Self.UrlReservations() { CreateAutomatically = true }
            };
            var port = "8081"; //TODO: unhardcode
            var uri = String.Format("http://localhost:{0}", port);

            var host = new Nancy.Hosting.Self.NancyHost(
                new Uri(uri),
                new NancySettingsBootstrapper(rootPath),
                hostConfig
            );

            host.Start();

            //Run phantom per harness
            var harnessUri = uri + "/" + harnessFile;
            var phantomrunner = new PhantomRunner(frameworkHandle, tests);
            var phantomResult = phantomrunner.Run(harnessUri);
            phantomResult.Wait();

            host.Stop();
        }
    }
}
