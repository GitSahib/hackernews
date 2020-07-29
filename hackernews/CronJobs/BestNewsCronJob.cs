using hackernews.Extensions;
using hackernews.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace hackernews.CronJobs
{
    public class BestNewsCronJob : NewsCronJob<BestNewsCronJob>
    {
        public BestNewsCronJob(IScheduleConfig<BestNewsCronJob> config, ILogger<BestNewsCronJob> logger, IStoryRepository repository)
               : base(config, logger, repository)
        {
            base._name = "BestNews";
        }
    }
}
