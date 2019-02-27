﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalBetFeed.Core.Models
{
    public class Event : IVitalBetObject
    {
        public long ID { get; set; }
        public long ID_ { get; set; }
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public long CategoryID { get; set; }
        public long SportID { get; set; }
    }
}
