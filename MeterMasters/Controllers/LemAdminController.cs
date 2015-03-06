using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MMUserContext.Concrete;

namespace MeterMasters.Controllers
{
    public class LemAdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private MMDataWorkUnit _unit;
        // GET: LemAdmin
        public ActionResult Index()
        {
            var requests = _unit.GetPendingRequests();
            return View(requests);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }

}