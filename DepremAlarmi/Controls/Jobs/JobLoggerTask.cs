using System;
using DepremAlarmi.Models;
using Shiny;
using Shiny.Infrastructure;
using Shiny.Jobs;

namespace DepremAlarmi.Controls.Jobs
{
    public class JobLoggerTask : IShinyStartupTask
    {
        readonly IJobManager jobManager;
        readonly SqliteConnection conn;
        readonly ISerializer serializer;


        public JobLoggerTask(IJobManager jobManager,
                             ISerializer serializer,
                             SqliteConnection conn)
        {
            this.jobManager = jobManager;
            this.serializer = serializer;
            this.conn = conn;
        }


        public void Start()
        {
            jobManager.JobStarted.SubscribeAsync(args => conn.InsertAsync(new JobLog
            {
                JobIdentifier = args.Identifier,
                JobType = args.Type.FullName,
                Started = true,
                Timestamp = DateTime.Now,
                Parameters = serializer.Serialize(args.Parameters)
            }));
            jobManager.JobFinished.SubscribeAsync(args => conn.InsertAsync(new JobLog
            {
                JobIdentifier = args.Job.Identifier,
                JobType = args.Job.Type.FullName,
                Error = args.Exception?.ToString(),
                Parameters = serializer.Serialize(args.Job.Parameters),
                Timestamp = DateTime.Now
            }));
        }
    }
}
