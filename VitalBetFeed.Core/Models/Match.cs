using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalBetFeed.Core.Models
{
    public class Match : IVitalBetObject
    {
        public long ID { get; set; }
        public long ID_ { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string MatchType { get; set; }
        public long EventID { get; set; }
    }
}
