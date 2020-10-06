using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeedbackLoop.Services
{
    public class HeartbeatService 
    {
        public async Task ExecuteAsync()
        {
            while (true)
            {
                Console.WriteLine("Heartbeat");
                await Task.Delay(30000);
            }
        }
    }
}
