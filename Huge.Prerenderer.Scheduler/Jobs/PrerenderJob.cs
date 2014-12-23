using Quartz;
using System;
using System.IO;

namespace Huge.Prerenderer.Scheduler.Jobs
{
    public class PrerenderJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            string path = @"c:\temp\" + DateTime.Now.Ticks + ".txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(DateTime.Now);
            }
        }
    }
}
