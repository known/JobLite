using System;

namespace JobLite
{
    public class JobInfo
    {
        public string Id { get; set; }
        public string Server { get; set; }
        public string Name { get; set; }
        public string ExecuteTarget { get; set; }
        public string ExecuteInterval { get; set; }
        public JobStatus Status { get; set; }
        public int? SuccessCount { get; set; }
        public int? FailCount { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Message { get; set; }
        public string Enabled { get; set; }
        public int RunCount { get; set; }
    }
}
