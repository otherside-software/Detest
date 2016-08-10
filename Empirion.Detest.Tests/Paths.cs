using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest.Tests
{
    [TestClass]
    public class Paths
    {
        [TestMethod]
        public void Find_Correct_Root_For_Path()
        {
            var file = PathFinder.GetApplicationPath("lib/require.js");
            var path = PathFinder.FindRootDirectoryForFile(file);

            Assert.AreEqual(PathFinder.GetApplicationPath(), path);
        }

        [TestMethod]
        public void Relative_Path_To_Directory_Should_Be_Correct()
        {
            var rootPath = @"C:\git\Detest\Empirion.Detest.Tests";
            var path = @"C:\git\Detest\Empirion.Detest.Tests\Bar\baz";

            var relativePath = PathFinder.GetRelativePathUri(path, rootPath);
            Assert.AreEqual("Bar/baz", relativePath);
        }
    }
}
