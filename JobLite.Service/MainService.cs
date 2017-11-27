using JobLite.Logs;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Timers;

namespace JobLite.Service
{
    public partial class MainService : ServiceBase
    {
        private Timer checkTimer;
        private JobService jobService;
        private JobHelper jobHelper;

        private static Dictionary<string, JobTimer> timers = new Dictionary<string, JobTimer>();
        private ILogger log = new ConsoleLogger();

        public MainService()
        {
            InitializeComponent();

            checkTimer = new Timer(Config.TimerInterval) { Enabled = true };
            checkTimer.Elapsed += new ElapsedEventHandler(CheckTimer_Elapsed);

            jobService = new JobService(new TestJobRepository());
            jobHelper = new JobHelper(jobService);
        }

        protected override void OnStart(string[] args)
        {
            checkTimer.Start();
            log.Info("启动服务");
        }

        protected override void OnStop()
        {
            checkTimer.Stop();
            try
            {
                var jobs = jobService.GetServerJobs(Config.Server);
                foreach (var job in jobs)
                {
                    if (job.Status == JobStatus.Running)
                    {
                        jobService.UpdateJobStatus(job, JobStatus.Normal, "强制停止运行");
                    }
                }
            }
            catch (Exception ex)
            {
                JobHelper.SendExceptionMail("主服务停止异常", ex.ToString());
            }

            log.Info("停止服务");
        }

        private void CheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var jobs = jobService.GetServerJobs(Config.Server);
                foreach (var job in jobs)
                {
                    if (job.Status == JobStatus.Disable && timers.ContainsKey(job.Id))
                    {
                        StopJob(timers[job.Id]);
                    }

                    if (job.Status == JobStatus.Normal && !timers.ContainsKey(job.Id))
                    {
                        var result = jobHelper.CheckJob(log, job);
                        if (!result.Pass)
                        {
                            jobService.UpdateJobStatus(job, JobStatus.Abnormal, result.ErrorMessage);
                            log.Info($"{result.ErrorMessage}{job.ExecuteTarget}");
                            continue;
                        }

                        var timer = new JobTimer(job.Id, result.TimerInterval)
                        {
                            Job = job,
                            CheckResult = result,
                            Config = jobService.GetJobConfig(job.Id),
                            Enabled = true
                        };
                        timer.Elapsed += new ElapsedEventHandler(JobTimer_Elapsed);
                        timers[timer.Id] = timer;
                        timer.Start();
                        log.Info($"启动Job-{timer.Job.Name}");
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                JobHelper.SendExceptionMail("主服务轮询作业异常", ex.ToString());
            }
        }

        private void JobTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var timer = sender as JobTimer;
                var job = timer.Job;
                var result = timer.CheckResult;

                if (job.Status == JobStatus.Running)
                    return;

                if (!jobService.CheckJobTime(DateTime.Now, job, result))
                    return;

                if (!jobHelper.RunJob(log, job, result.Instance, timer.Config))
                    StopJob(timer);
            }
            catch (Exception ex)
            {
                JobHelper.SendExceptionMail("主服务执行作业异常", ex.ToString());
            }
        }

        private void StopJob(JobTimer timer)
        {
            timer.Stop();
            timers.Remove(timer.Id);
            log.Info($"停止Job-{timer.Job.Name}");
            timer.Dispose();
        }
    }
}
