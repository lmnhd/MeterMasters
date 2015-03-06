using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WePaySDK;
using MMUserContext.Entities;
using MMUserContext.Concrete;

namespace MeterMasters.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
       // private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly MMDataWorkUnit _unit;

        public PaymentController()
        {
            _unit = new MMDataWorkUnit();
          
            
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
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReviewMix(int mixId)
        {
            var user = UserManager.FindById(this.User.Identity.GetUserId());
            var mix = _unit.GetMix(mixId);

            if (mix.ClientUserId.Equals(user.Id))
            {
                return View(mix);
            }
            return View();

        }
        public ActionResult CheckoutCreate(decimal amt)
        {

            var req = new CheckoutCreateRequest
            {
                account_id = WePayConfig.accountId,
                mode = "regular",
                accessToken = WePayConfig.accessToken,
                type = "SERVICE",
                amount = amt,
                short_description = "purchase mix masters",
                redirect_uri = Request.Url.Authority + @"/Home/DownloadMix"
            };

            var resp = new Checkout().Post(req);
            if (resp.Error != null)
            {
                ViewBag.Error = resp.Error.error + " - " + resp.Error.error_description;
                return View("Status");
            }

            return Redirect(resp.checkout_uri);
        }

    }


}