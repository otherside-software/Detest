using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest
{
    public class PathFinder
    {
        public static string GetApplicationPath(string file = null)
        {
            if (String.IsNullOrWhiteSpace(file))
            {
                return Path.GetDirectoryName(typeof(PathFinder).Assembly.Location);
            }

            return Path.Combine(
                Path.GetDirectoryName(typeof(PathFinder).Assembly.Location),
                file
            );
        }
    }
}
