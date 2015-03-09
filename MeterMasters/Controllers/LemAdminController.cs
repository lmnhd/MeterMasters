using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using MMUserContext.Concrete;
using MMUserContext.Entities;
using Newtonsoft.Json;

namespace MeterMasters.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LemAdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private MMDataWorkUnit _unit;

        public LemAdminController()
        {
            _unit = new MMDataWorkUnit();
        }


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
        public class RequestComparer : IComparer<MixRequest>
        {

            public int Compare(MixRequest x, MixRequest y)
            {
                if (x.EntryTime > y.EntryTime)
                {
                    return 1;
                }
                else if (y.EntryTime > x.EntryTime)
                {
                    return -1;
                }
                return 0;
            }
        }

        [HttpPost]
        //[Route("GetAllRequests")]
        public ActionResult GetAllRequests()
        {
            //return JsonConvert.SerializeObject(_unit.GetPendingRequests());
            var result = _unit.GetPendingRequests();
            result.Sort(new RequestComparer() );
            return Json(result);
        } 
    }

}