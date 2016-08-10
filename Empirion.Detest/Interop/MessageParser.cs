using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empirion.Detest.Interop
{
    public class MessageParser
    {
        public static TestDescription ParseTestMessage(string message)
        {
            try
            {
                var description = JsonConvert.DeserializeObject<TestDescription>(message);
                if (description.Suite == null || description.Test == null)
                    return null;

                return description;
            }
            catch
            {
                return null;
            }
        }
    }
}
