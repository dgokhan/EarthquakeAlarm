using System;
using System.Threading;
using System.Threading.Tasks;
using DepremAlarmi.Controls.Services;
using Shiny;
using Shiny.Jobs;

namespace DepremAlarmi.Controls.Jobs
{
    public class GetDataJob : IJob
    {
        readonly CoreDelegateServices services;
        public GetDataJob(CoreDelegateServices services) => this.services = services;

        public async Task<bool> Run(JobInfo jobInfo, CancellationToken cancelToken)
        {
            await services.SendNotification(
                "Job Started ",
                $"{jobInfo.Identifier} Started bro! " 
                                                 
            );

            jobInfo.Repeat = true;
            // you really shouldn't lie about this on iOS as it is watching :)
            return true;
        }
    }
}
