using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Empirion.Detest;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace Empirion.Detest.Tests
{
    [TestClass]
    public class ExternalTools
    {
        [TestMethod]
        public async Task PhantomStreaming2()
        {
            var progress = new Progress<string>((data) =>
            {
                Debug.WriteLine(data);
            });

            var foo = await ProcessRunner.RunProcessAsync("phantomjs.exe", "detest_phantom.js", progress);
            Debug.WriteLine(foo.Output.Count());
        }
    }
}