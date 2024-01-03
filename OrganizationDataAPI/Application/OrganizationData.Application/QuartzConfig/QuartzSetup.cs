using Microsoft.Extensions.Options;
using OrganizationData.Application.FileData;
using Quartz;

namespace OrganizationData.Application.QuartzConfig
{
    internal class QuartzSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var jobKey = JobKey.Create(nameof(FileDataJob));

            options
                .AddJob<FileDataJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(trigger =>
                {
                    trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule.WithIntervalInHours(6).RepeatForever());
                });
        }
    }
}
