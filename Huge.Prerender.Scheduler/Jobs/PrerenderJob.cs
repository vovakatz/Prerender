using Quartz;
using System;
using System.IO;

namespace Huge.Prerender.Scheduler.Jobs
{
    public class PrerenderJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //call end point

            string path = @"c:\temp\" + DateTime.Now.Ticks + ".txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(DateTime.Now);
            }
        }
    }
}
