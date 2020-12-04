using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyBlog.Web.Core
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class TraceLogger : ILogger
    {
        public void Log(string message)
        {
            Trace.WriteLine(message);
        }
    }
}
