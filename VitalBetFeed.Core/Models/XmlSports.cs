using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalBetFeed.Core.Models
{
    public class XmlSports
    {
        public DateTime CreateDate { get; set; }
        public List<Sport> Sports { get; set; }
        public List<Event> Events { get; set; }
        public List<Match> Matches { get; set; }
        public List<Bet> Bets { get; set; }
        public List<Odd> Odds { get; set; }
    }
}
