using System.ComponentModel;

namespace JobLite
{
    public enum JobStatus
    {
        [Description("已停用")]
        Disable = 0,
        [Description("运行正常")]
        Normal = 1,
        [Description("运行异常")]
        Abnormal = 2,
        [Description("运行中")]
        Running = 3
    }
}
