using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VitalBetFeed.Core.Models;

namespace VitalBetFeed.Core.Services
{
    public static class XmlParseService
    {
        public static XmlSports Parse(string content)
        {
            List<Sport> sports = new List<Sport>();
            List<Event> events = new List<Event>();
            List<Match> matches = new List<Match>();
            List<Bet> bets = new List<Bet>();
            List<Odd> odds = new List<Odd>();

            using (XmlReader reader = XmlReader.Create(new StringReader(content)))
            {
                long currentSportId = 0;
                long currentEventId = 0;
                long currentMatchId = 0;
                long currentBetId = 0;

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "Sport")
                        {
                            string id = reader["ID"];
                            string name = reader["Name"];
                            Sport s = new Sport()
                            {
                                ID_ = long.Parse(id),
                                Name = name
                            };

                            sports.Add(s);
                            currentSportId = s.ID_;
                        }
                        else if (reader.Name == "Event")
                        {
                            string id = reader["ID"];
                            string name = reader["Name"];
                            string isLive = reader["IsLive"];
                            string categoryID = reader["CategoryID"];

                            Event e = new Event()
                            {
                                ID_ = long.Parse(id),
                                Name = name,
                                IsLive = bool.Parse(isLive),
                                CategoryID = long.Parse(categoryID),
                                SportID = currentSportId
                            };

                            currentEventId = e.ID_;
                            events.Add(e);
                        }
                        else if (reader.Name == "Match")
                        {
                            string id = reader["ID"];
                            string name = reader["Name"];
                            string startDate = reader["StartDate"];
                            string matchType = reader["MatchType"];

                            Match m = new Match()
                            {
                                ID_ = long.Parse(id),
                                Name = name,
                                StartDate = DateTime.Parse(startDate),
                                MatchType = matchType,
                                EventID = currentEventId
                            };

                            currentMatchId = m.ID_;
                            matches.Add(m);
                        }
                        else if (reader.Name == "Bet")
                        {
                            string id = reader["ID"];
                            string name = reader["Name"];
                            string isLive = reader["IsLive"];

                            Bet b = new Bet()
                            {
                                ID_ = long.Parse(id),
                                Name = name,
                                IsLive = bool.Parse(isLive),
                                MatchID = currentMatchId
                            };

                            currentBetId = b.ID_;
                            bets.Add(b);
                        }
                        else if (reader.Name == "Odd")
                        {
                            string id = reader["ID"];
                            string name = reader["Name"];
                            string value = reader["Value"];
                            string specialBetValue = reader["SpecialBetValue"];

                            Odd o = new Odd()
                            {
                                ID_ = long.Parse(id),
                                Name = name,
                                Value = double.Parse(value, CultureInfo.InvariantCulture),
                                SpecialBetValue = specialBetValue,
                                BetID = currentBetId
                            };

                            odds.Add(o);
                        }
                    }
                }
            }

            return new XmlSports()
            {
                Sports = sports,
                Events = events,
                Matches = matches,
                Bets = bets,
                Odds = odds
            };
        }
    }
}
