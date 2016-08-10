using Antlr4.StringTemplate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest
{
    public static class HarnessBuilder
    {
        public static string Construct(string template, IEnumerable<string> files)
        {
            var tmpl = new Template(template, '$', '$');

            //TODO: fetch from detest.json
            tmpl.Add("dependencies", new List<string>()
            {
                "Scripts/lib/require.js",
                "Scripts/require.config.js",
                "Scripts/require.test.config.js"
            });

            tmpl.Add("files", files);
            return tmpl.Render();
        }

        public static void CreateHarnessFile(IFrameworkConfiguration configuration, IEnumerable<string> files, string outputPath, string outputFile)
        {
            var harness = PathFinder.GetApplicationPath(configuration.Harness);
            var template = File.ReadAllText(harness);

            //TODO: unhardcode (fetch from detest.json)
            //TODO: for some reason this isn't necessary, script files fetched from harness are not relative to baseUrl.
           // var scriptsRoot = Path.Combine(outputPath, "Scripts");

            var relativePathFiles = files.Select(f => PathFinder.GetRelativePathUri(f, outputPath));
            var generated = Construct(template, relativePathFiles);
            var targetFile = Path.Combine(outputPath, outputFile);
            File.WriteAllText(targetFile, generated);
        }

        
    }
}
