using hackernews.Extensions;
using hackernews.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace hackernews.CronJobs
{
    public class TopNewsCronJob : NewsCronJob<TopNewsCronJob>
    {
        public TopNewsCronJob(IScheduleConfig<TopNewsCronJob> config, ILogger<TopNewsCronJob> logger, IStoryRepository repository)
               : base(config, logger, repository)
        {
            base._name = "TopNews";
        }
    }
}
