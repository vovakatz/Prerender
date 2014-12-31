using Quartz;
using System.Collections.Specialized;

namespace Huge.Prerender.Scheduler
{
    public sealed class QuartzScheduler
    {
        /// <summary>
        /// The scheduler
        /// </summary>
        private static IScheduler scheduler;

        /// <summary>
        /// the lock
        /// </summary>
        private static readonly object padlock = new object();


        /// <summary>
        /// The scheduler singleton
        /// </summary>
        private QuartzScheduler()
        {

        }

        /// <summary>
        /// The scheduler
        /// </summary>
        public static IScheduler Instance
        {
            get
            {
                lock (padlock)
                {
                    if (scheduler == null)
                    {
                        NameValueCollection properties = new NameValueCollection();
                        properties["quartz.scheduler.instanceName"] = "PrerenderScheduler"; // needed if you plan to use the same database for many schedulers
                        properties["quartz.jobStore.type"] = "Quartz.Simpl.RAMJobStore";
                        properties["quartz.threadPool.threadCount"] = "1";
 
                        scheduler = new Quartz.Impl.StdSchedulerFactory(properties).GetScheduler();
                    }

                    return scheduler;
                }
            }
        }
    }
}
