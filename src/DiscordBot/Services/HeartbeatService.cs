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
            string webjobName = "FeedbackLoopBot";
            string userName = "$FeedbackLooper";
            string userPWD = Environment.GetEnvironmentVariable("publishingPassword");
            string webjobUrl = string.Format("https://{0}.scm.azurewebsites.net/api/continuouswebjobs/{1}", websiteName, webjobName);

            while (true)
            {
                HttpClient client = new HttpClient();
                Console.WriteLine("Getting Ready To Heartbeat");
                try
                {
                    string auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ':' + userPWD));
                    client.DefaultRequestHeaders.Add("authorization", auth);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var data = client.GetStringAsync(webjobUrl).Result;
                    var result = JsonConvert.DeserializeObject(data) as JObject;
                    Console.WriteLine("Heartbeat " + result.ToString());
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
