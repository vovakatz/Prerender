﻿using Huge.Prerender.Models.Scheduler;
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
            JobData jobData = context.MergedJobDataMap["info"] as JobData;
            //call end point
            var client = new RestClient(jobData.ServiceEndPointUrl);
            var request = new RestRequest("Prerender/Start", Method.POST);
            request.RequestFormat = DataFormat.Json;
            
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
