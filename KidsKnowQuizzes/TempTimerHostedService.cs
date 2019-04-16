using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KidsKnowQuizzes.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace KidsKnowQuizzes
{
    public class TempTimerHostedService : IHostedService, IDisposable
    {
        private readonly IHubContext<QuizHub> _quizHubContext;

        public TempTimerHostedService(IHubContext<QuizHub> quizHubContext)
        {
            _quizHubContext = quizHubContext;
        }

        private Timer _timer;
        private void DoWork(object state)
        {
            _quizHubContext.Clients.All.SendAsync("test", DateTime.Now.ToLongTimeString()).Wait();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
