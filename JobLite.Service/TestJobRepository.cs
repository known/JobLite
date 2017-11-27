using System.Collections.Generic;

namespace JobLite.Service
{
    class TestJobRepository : IJobRepository
    {
        public List<JobInfo> GetServerJobs(string server)
        {
            return new List<JobInfo>();
        }

        public Dictionary<string, object> GetJobConfig(string id)
        {
            return new Dictionary<string, object>();
        }

        public void UpdateJob(JobInfo job)
        {
        }

        public void AddRecord(JobRecord record)
        {
        }
    }
}
