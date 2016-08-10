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
        private const string ConfigFileName = "detest.json";

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

        /// <summary>
        /// Returns the parent folder where a detest.json resides.
        /// </summary>
        /// <param name="file">The file for which to find the root folder.</param>
        public static string FindRootDirectoryForFile(string file)
        {
            var path = Path.GetDirectoryName(file);
            return FindRootDirectoryForPath(path);
        }

        public static string GetRelativePathUri(string path, string relativeTo)
        {
            if (!relativeTo.EndsWith("\\"))
                relativeTo += "\\";

            var relative = new Uri(relativeTo).MakeRelativeUri(new Uri(path));
            return relative.OriginalString;
        }

        private static string FindRootDirectoryForPath(string path)
        {
            var configPath = Path.Combine(path, PathFinder.ConfigFileName);
            if (File.Exists(configPath))
            {
                return path;
            }

            return FindRootDirectoryForPath(Directory.GetParent(path).FullName);
        }
    }
}
