using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using VitalBetFeed.Core.Models;
using VitalBetFeed.Core.Services;
using System.Net;
using VitalBetFeed.Data.Services;

namespace VitalBetFeed.Hubs
{
    public class MatchesHub : Hub
    {
        public void Start()
        {
            JobSchedulerService.Start(GetMatches);
        }

        private void GetMatches()
        {
            WebClient client = new WebClient();
            string response = client.DownloadString("http://vitalbet.net/sportxml");
            XmlSports xmlSports = XmlParseService.Parse(response);

            PersistingService.Persist(xmlSports);

            DateTime minDate = DateTime.Now;
            DateTime maxDate = minDate.AddDays(1);

            IEnumerable<Match> matches = MatchesService.GetMatches(minDate, maxDate);

            Clients.All.GetMatches(matches);
        }
    }
}