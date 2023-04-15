using Hangfire;
using HC.TmdbBackgroundJob.Managers.RecurringJobs;

namespace HC.TmdbBackgroundJob.Schedules
{
    /// <summary>
    /// Çok kez tekrarlı işler ve belirtilen CRON süresince çalışır.
    /// </summary>

    //[AutomaticRetry(Attempts=1)]
    public static  class RecurringJobs
    {
        public static void Start()
        {
            RecurringJob.AddOrUpdate<TmdbFilmsJobManager>(nameof(TmdbFilmsJobManager),
               job => job.Process(), Cron.Minutely()
          );
        }
    }
}
