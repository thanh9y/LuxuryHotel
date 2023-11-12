using LuxuryHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxuryHotel.Areas.Reception.Controllers
{
    public class StatisticController : Controller
    {
        private dbDataContext db = new dbDataContext("Data Source=MSI;Initial Catalog=LuxuryHotel;Integrated Security=True");

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}