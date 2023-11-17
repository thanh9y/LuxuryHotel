using LuxuryHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LuxuryHotel.Areas.Reception.Controllers
{
    public class CustomerController : Controller
    {
        private dbDataContext db = new dbDataContext();
        // GET: Reception/Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}