using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VitalBetFeed.Core.Models;

namespace VitalBetFeed.Core.Services
{
    public static class JobSchedulerService
    {
        public static void Start(Action action)
        {
            IObservable<long> observable = Observable.Interval(TimeSpan.FromSeconds(60)).StartWith(-1L);

            CancellationTokenSource source = new CancellationTokenSource();

            observable.Subscribe(x =>
            {
                Task task = new Task(action);
                task.Start();
            }, source.Token);
        }
    }
}
