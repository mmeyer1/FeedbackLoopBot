using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeedbackLoop.Services
{
    public class HeartbeatService 
    {
        public void Heartbeat()
        {
            string websiteName = "feedbacklooper";
            string userName = "$FeedbackLooper";
            string userPWD = Environment.GetEnvironmentVariable("publishingPassword");
            string webjobUrl = string.Format("https://{0}.azurewebsites.net", websiteName);

            while (true)
            {
                HttpClient client = new HttpClient();
                Console.WriteLine("Getting Ready To Heartbeat");
                try
                {
                    string auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ':' + userPWD));
                    client.DefaultRequestHeaders.Add("authorization", auth);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    var data = client.GetStringAsync(webjobUrl).Result;
                    Console.WriteLine("Heartbeat success");
                }
                catch (Exception E)
                {
                    Console.WriteLine("Heartbeat error ! " + E.Message);
                } finally
                {
                    Thread.Sleep(30000);
                }
            }
        }
    }
}
