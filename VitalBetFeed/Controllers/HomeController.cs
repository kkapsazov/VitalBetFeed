using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VitalBetFeed.Core.Models;
using VitalBetFeed.Core.Services;

namespace VitalBetFeed.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}