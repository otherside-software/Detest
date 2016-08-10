using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest.WebHost
{
    public class FixedRootPathProvider : IRootPathProvider
    {
        private string rootPath;

        public FixedRootPathProvider(string rootPath)
        {
            this.rootPath = rootPath;
        }

        public string GetRootPath()
        {
            return rootPath;
        }
    }
}
