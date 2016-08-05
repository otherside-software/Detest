using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest
{
    [Export(typeof(ITestContainerDiscoverer))]
    public class TestContainerDiscoverer : ITestContainerDiscoverer
    {
        private IServiceProvider serviceProvider;
        // private readonly ConcurrentDictionary<string, ITestContainer> cachedContainers;

        public IEnumerable<ITestContainer> TestContainers { get { return GetTestContainers(); } }

        public Uri ExecutorUri { get { return JsTestExecutor.ExecutorUri; } }

        public event EventHandler TestContainersUpdated;

        [ImportingConstructor]
        public TestContainerDiscoverer([Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider)
        {
            System.Diagnostics.Debug.WriteLine("constructor: " + ToString());
            this.serviceProvider = serviceProvider;
        }

        private IList<ITestContainer> GetTestContainers()
        {
            var solution = (IVsSolution)serviceProvider.GetService(typeof(SVsSolution));
            var loadedProjects = solution.EnumerateLoadedProjects(__VSENUMPROJFLAGS.EPF_LOADEDINSOLUTION).OfType<IVsProject>();

            try
            {
                var containers = loadedProjects
                    .SelectMany(p => p.GetProjectItems())
                    .Where(IsTestFile)
                    .Select(p => new TestContainer(this, p));

                var list = containers.ToList<ITestContainer>();
                foreach (var l in list)
                {
                    System.Diagnostics.Debug.WriteLine(l.Source);
                }
                return list;
            }
            catch(Exception ex)
            {
                return new List<ITestContainer>();
            }

          
        }

        private bool IsTestFile(string file)
        {
            return file.EndsWith(".js") && file.Contains("test");
        }
    }
}
