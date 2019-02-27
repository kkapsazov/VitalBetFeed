using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VitalBetFeed.Core.Models;
using VitalBetFeed.Core.Services;
using VitalBetFeed.Data;
using VitalBetFeed.Data.Services;

namespace VitalBetFeed.Tests
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod]
        public void TestStaticParsing()
        {
            var xmlSports = XmlParseService.Parse(File.ReadAllText("../../bet.xml"));

            DateTime min = DateTime.Parse("2017-02-28T09:37:13.352Z");
            DateTime max = min.AddDays(1);
            IEnumerable<Match> matches = xmlSports.Matches.Where(x => x.StartDate > min && x.StartDate < max
                                            && xmlSports.Bets.Where(y => y.MatchID == x.ID_ && xmlSports.Odds.Any(z => z.BetID == y.ID_)).Any());

            Assert.IsTrue(matches.Count() == 5);
        }

        [TestMethod]
        public void TestSimplePersitingAndRetrieving()
        {
            XmlSports xml = new XmlSports()
            {
                Sports = new List<Sport>(),
                Events = new List<Event>(),
                Matches = new List<Match>() { new Match() { ID = 1, ID_ = 1, Name = "asd", StartDate = DateTime.Parse("3017-02-28T10:37:13.352Z") } },
                Bets = new List<Bet>() { new Bet() { ID = 1, ID_ = 1, MatchID = 1, Name = "sad" } },
                Odds = new List<Odd>() { new Odd() { ID = 1, ID_ = 1, BetID = 1, Name = "sad" } }
            };

            PersistingService.Persist(xml);

            DateTime min = DateTime.Parse("3017-02-28T09:37:13.352Z");
            DateTime max = min.AddDays(1);
            IEnumerable<Match> matches = MatchesService.GetMatches(min, max);

            Assert.IsTrue(matches.Count() == 1);
        }

        [TestMethod]
        public void TestPersistance()
        {
            WebClient client = new WebClient();
            string response = client.DownloadString("http://vitalbet.net/sportxml");
            XmlSports xmlSports = XmlParseService.Parse(response);

            PersistingService.Persist(xmlSports);
        }

        [TestMethod]
        public void TestRetrievingData()
        {
            DateTime min = DateTime.Now;
            DateTime max = min.AddDays(1);
            IEnumerable<Match> matches = MatchesService.GetMatches(min, max);

            Assert.IsTrue(matches.Any());
        }
    }
}
