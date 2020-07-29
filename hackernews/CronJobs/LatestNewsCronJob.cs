using hackernews.Extensions;
using hackernews.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace hackernews.CronJobs
{
    public class LatestNewsCronJob : NewsCronJob<LatestNewsCronJob>
    {
        public LatestNewsCronJob(IScheduleConfig<LatestNewsCronJob> config, ILogger<LatestNewsCronJob> logger, IStoryRepository repository)
               : base(config, logger, repository)
        {
            base._name = "LatestNews";
        }
    }
}
