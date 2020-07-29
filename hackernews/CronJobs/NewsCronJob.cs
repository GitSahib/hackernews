using hackernews.Extensions;
using hackernews.Repositories.Interfaces;
using hackernews.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hackernews.CronJobs
{
    public class NewsCronJob<T> : AbstractCronJobService
    {
        private readonly ILogger<T> _logger;
        private readonly IStoryRepository _repository;

        public NewsCronJob(IScheduleConfig<T> config, ILogger<T> logger, IStoryRepository repository)
            : base(config.CronExpression, config.TimeZoneInfo, config.name)
        {
            _logger = logger;
            _repository = repository;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"NewsCronJob {_name} starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            Task task = _repository.GetAllByTypeAsync(new ViewModels.Request.StoryClientRequest { search = "Test", type = _name });
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} NewsCronJob {_name} is working.");
            return task;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"NewsCronJob {_name}  is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
