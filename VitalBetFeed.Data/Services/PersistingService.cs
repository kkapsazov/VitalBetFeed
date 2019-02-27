using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitalBetFeed.Core.Models;

namespace VitalBetFeed.Data.Services
{
    public static class PersistingService
    {
        public static void Persist(XmlSports xmlSports)
        {
            using (VitalBetDbContext context = new VitalBetDbContext("VitalBet"))
            {
                try
                {
                    context.Configuration.AutoDetectChangesEnabled = false;

                    BulkInsert<Sport>(context, context.SportRepo, xmlSports.Sports);
                    BulkInsert<Event>(context, context.EventsRepo, xmlSports.Events);
                    BulkInsert<Match>(context, context.MatchesRepo, xmlSports.Matches);
                    BulkInsert<Bet>(context, context.BetsRepo, xmlSports.Bets);
                    BulkInsert<Odd>(context, context.OddsRepo, xmlSports.Odds);

                    context.SaveChanges();
                }
                finally
                {
                    context.Configuration.AutoDetectChangesEnabled = true;
                }
            }
        }

        private static void BulkInsert<T>(VitalBetDbContext context, GenericRepository<T> repo, List<T> newItems) where T : class, IVitalBetObject
        {
            Dictionary<long, T> existingItems = repo.Get().ToDictionary(x => x.ID_, x => x);

            if (existingItems.Any())
            {
                foreach (var item in newItems)
                {
                    AddOrUpdate<T>(repo, item, existingItems);
                }
            }
            else
            {
                repo.BulkInsert(newItems);
            }
        }

        private static void AddOrUpdate<T>(GenericRepository<T> repo, T item, Dictionary<long, T> existingItems) where T : class, IVitalBetObject
        {
            if (existingItems.ContainsKey(item.ID_))
            {
                T existingItem = existingItems[item.ID_];
                if (IsChangesDetected<T>(item, existingItem))
                {
                    TransferChanges<T>(item, existingItem);
                    repo.Update(existingItem);
                }
            }
            else
            {
                repo.Insert(item);
            }
        }

        private static void TransferChanges<T>(T source, T destination)
        {
            if (source is Sport)
            {
                Sport s = source as Sport;
                Sport d = destination as Sport;

                d.Name = s.Name;
            }

            if (source is Event)
            {
                Event s = source as Event;
                Event d = destination as Event;

                d.Name = s.Name;
                d.CategoryID = s.CategoryID;
                d.IsLive = s.IsLive;
            }

            if (source is Match)
            {
                Match s = source as Match;
                Match d = destination as Match;

                d.Name = s.Name;
                d.MatchType = s.MatchType;
                d.StartDate = s.StartDate;
            }

            if (source is Bet)
            {
                Bet s = source as Bet;
                Bet d = destination as Bet;

                d.Name = s.Name;
                d.IsLive = s.IsLive;
            }

            if (source is Odd)
            {
                Odd s = source as Odd;
                Odd d = destination as Odd;

                d.Name = s.Name;
                d.Value = s.Value;
                d.SpecialBetValue = d.SpecialBetValue;
            }
        }

        private static bool IsChangesDetected<T>(T source, T destination)
        {
            if (source is Sport)
            {
                Sport s = source as Sport;
                Sport d = destination as Sport;

                if (d.Name != s.Name)
                    return true;

                return false;
            }

            if (source is Event)
            {
                Event s = source as Event;
                Event d = destination as Event;

                if (d.Name != s.Name ||
                    d.CategoryID != s.CategoryID ||
                    d.IsLive != s.IsLive)
                    return true;

                return false;
            }

            if (source is Match)
            {
                Match s = source as Match;
                Match d = destination as Match;

                if (d.Name != s.Name ||
                    d.MatchType != s.MatchType ||
                    d.StartDate != s.StartDate)
                    return true;

                return false;
            }

            if (source is Bet)
            {
                Bet s = source as Bet;
                Bet d = destination as Bet;

                if (d.Name != s.Name ||
                    d.IsLive != s.IsLive)
                    return true;

                return false;
            }

            if (source is Odd)
            {
                Odd s = source as Odd;
                Odd d = destination as Odd;

                if (d.Name != s.Name ||
                    d.Value != s.Value ||
                    d.SpecialBetValue != d.SpecialBetValue)
                    return true;

                return false;
            }

            return false;
        }
    }
}
