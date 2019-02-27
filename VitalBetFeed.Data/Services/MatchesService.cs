using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitalBetFeed.Core.Models;

namespace VitalBetFeed.Data.Services
{
    public static class MatchesService
    {
        public static IEnumerable<Match> GetMatches(DateTime min, DateTime max)
        {
            using (VitalBetDbContext context = new VitalBetDbContext("VitalBet"))
            {
                return context.MatchesRepo.dbSet.Where(x => x.StartDate > min && x.StartDate < max &&
                                                       context.BetsRepo.dbSet.Any(y => y.MatchID == x.ID_ && context.OddsRepo.dbSet.Any(z => z.BetID == y.ID_))).ToList();
            }
        }
    }
}
