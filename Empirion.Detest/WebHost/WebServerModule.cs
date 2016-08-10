using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace Empirion.Detest.WebHost
{
    public class WebServerModule : NancyModule
    {
        public WebServerModule()
        {
            Get["/{uri*}"] = parameters =>
            {
                string path = parameters["uri"].Value;
                return Response.AsFile(path);
            };

            Get["/"] = parameters =>
            {
                return "Detest Web Server";
            };
        }
    }
}
