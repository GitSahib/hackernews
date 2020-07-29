using System;

namespace hackernews.Extensions
{
    public interface IScheduleConfig<T>
    {
        string name { get; set; }
        string CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }

    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string name { get; set; }
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
