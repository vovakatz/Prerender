using Huge.Prerender.Models.Scheduler;
using Quartz;
using RestSharp;
using System;
using System.IO;

namespace Huge.Prerender.Scheduler.Jobs
{
    public class PrerenderJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //call end point
            var client = new RestClient("http://localhost/Huge.Prerender.Service/api");
            var request = new RestRequest("Prerender", Method.POST);
            request.RequestFormat = DataFormat.Json;
            JobData jobData = context.MergedJobDataMap["info"] as JobData;
            //System.Diagnostics.Debugger.Launch();
            request.AddBody(jobData);

            client.ExecuteAsync(request, response =>
            {
                //log the completion of the task
                //Console.WriteLine(response.Content);
            });


            string path = @"c:\temp\" + DateTime.Now.Ticks + ".txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(DateTime.Now);
            }
        }
    }
}
