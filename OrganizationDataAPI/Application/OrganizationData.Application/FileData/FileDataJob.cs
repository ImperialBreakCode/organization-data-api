using OrganizationData.Application.Abstractions.FileData;
using Quartz;
using System.Diagnostics;

namespace OrganizationData.Application.FileData
{
    [DisallowConcurrentExecution]
    internal class FileDataJob : IJob
    {
        private readonly IFileDataManager _dataManager;

        public FileDataJob(IFileDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _dataManager.SaveDataFromFiles();

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return Task.CompletedTask;
        }
    }
}
