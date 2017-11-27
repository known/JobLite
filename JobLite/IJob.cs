using JobLite.Logs;
using System.Collections.Generic;

namespace JobLite
{
    public interface IJob
    {
        ExecuteResult Execute(ILogger log, Dictionary<string, object> config);
    }
}
