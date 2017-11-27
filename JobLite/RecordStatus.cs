using System.ComponentModel;

namespace JobLite
{
    public enum RecordStatus
    {
        [Description("执行成功")]
        Success = 1,
        [Description("执行失败")]
        Failure = 2
    }
}
