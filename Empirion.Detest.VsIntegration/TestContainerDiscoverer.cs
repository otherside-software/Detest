using EnvDTE;
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
            this.serviceProvider = serviceProvider;
        }

        private IList<ITestContainer> GetTestContainers()
        {
            var envdte = serviceProvider.GetService(typeof(_DTE)) as _DTE;

            return GetAllProjectItemsInSolution(envdte)
                .Where(IsTestFile)
                .Select(p => new TestContainer(this, p.FileNames[0]))
                .ToList<ITestContainer>();
            
        }
        
        private bool IsTestFile(ProjectItem item)
        {
            if (item.FileCount > 0)
            {
                var file = item.FileNames[1];
                return file.EndsWith(".js") && file.Contains("test");
            }
            return false;
        }

        private IEnumerable<ProjectItem> GetAllProjectItemsInSolution(_DTE dte)
        {
            foreach (Project project in dte.Solution.Projects)
            {
                foreach(var item in GetAllSubProjectItems(project.ProjectItems))
                {
                    yield return item;   
                }
            }
        }

        private IEnumerable<ProjectItem> GetAllSubProjectItems(ProjectItems items)
        {
            if (items == null)
                yield break;

            foreach (ProjectItem item in items)
            {
                yield return item;
                foreach (var subItem in GetAllSubProjectItems(item.ProjectItems))
                    yield return subItem;
            }
        }
    }
}
