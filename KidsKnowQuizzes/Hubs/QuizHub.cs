using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace KidsKnowQuizzes.Hubs
{
    public class QuizHub : Hub
    {
        public async Task RegisterTest(string userName)
        {
            await Clients.All.SendAsync("test", $"{userName} has joined.");
        }
    }
}
