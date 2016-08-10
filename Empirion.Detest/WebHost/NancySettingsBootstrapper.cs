using Nancy;
using Nancy.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest.WebHost
{
    public class NancySettingsBootstrapper : DefaultNancyBootstrapper
    {
        readonly string rootPath;

        public NancySettingsBootstrapper(string rootPath)
        {
            this.rootPath = rootPath;
        }

        protected override IEnumerable<Type> ViewEngines
        {
            get
            {
                return Enumerable.Empty<Type>();
            }
        }

        protected override IRootPathProvider RootPathProvider
        {
            get
            {
                return new FixedRootPathProvider(rootPath);
            }
        }
    }
}
