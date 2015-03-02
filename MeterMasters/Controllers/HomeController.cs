using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MMUserContext.Concrete;

namespace MeterMasters.Controllers
{
   
    public class HomeController : Controller
    {
        private MMDataWorkUnit _unit;

        public HomeController()
        {
            _unit = new MMDataWorkUnit();
            
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
