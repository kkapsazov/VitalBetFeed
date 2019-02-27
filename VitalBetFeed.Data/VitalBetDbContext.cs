using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VitalBetFeed.Core.Models;

namespace VitalBetFeed.Data
{
    public class VitalBetDbContext : DbContext
    {
        public VitalBetDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            SqlProviderServices ensureDLLIsCopied = SqlProviderServices.Instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>().ToTable("Sport").Property(e => e.ID_)
                                                         .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                                              new IndexAnnotation(new IndexAttribute()));
            modelBuilder.Entity<Event>().ToTable("Event").Property(e => e.ID_)
                                                         .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                                              new IndexAnnotation(new IndexAttribute()));
            modelBuilder.Entity<Match>().ToTable("Match").Property(e => e.ID_)
                                                         .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                                              new IndexAnnotation(new IndexAttribute()));
            modelBuilder.Entity<Bet>().ToTable("Bet").Property(e => e.ID_)
                                                     .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                                          new IndexAnnotation(new IndexAttribute()));
            modelBuilder.Entity<Odd>().ToTable("Odd").Property(e => e.ID_)
                                                     .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                                          new IndexAnnotation(new IndexAttribute()));
        }

        private GenericRepository<Sport> sportRepo;
        public GenericRepository<Sport> SportRepo
        {
            get
            {
                if (sportRepo == null)
                {
                    sportRepo = new GenericRepository<Sport>(this);
                }
                return sportRepo;
            }
        }

        private GenericRepository<Match> matchesRepo;
        public GenericRepository<Match> MatchesRepo
        {
            get
            {
                if (matchesRepo == null)
                {
                    matchesRepo = new GenericRepository<Match>(this);
                }
                return matchesRepo;
            }
        }

        private GenericRepository<Event> eventsRepo;
        public GenericRepository<Event> EventsRepo
        {
            get
            {
                if (eventsRepo == null)
                {
                    eventsRepo = new GenericRepository<Event>(this);
                }
                return eventsRepo;
            }
        }

        private GenericRepository<Bet> betsRepo;
        public GenericRepository<Bet> BetsRepo
        {
            get
            {
                if (betsRepo == null)
                {
                    betsRepo = new GenericRepository<Bet>(this);
                }
                return betsRepo;
            }
        }

        private GenericRepository<Odd> oddsRepo;
        public GenericRepository<Odd> OddsRepo
        {
            get
            {
                if (oddsRepo == null)
                {
                    oddsRepo = new GenericRepository<Odd>(this);
                }
                return oddsRepo;
            }
        }
    }
}
