using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MMUserContext.Concrete;
using MMUserContext.Entities;
using Owin;
using MeterMasters.Models;

namespace MeterMasters.Controllers
{
    //[Authorize]
    public class MeController : ApiController
    {
        private ApplicationUserManager _userManager;
        private MMDataWorkUnit _unit;

        public MeController()
        {
            _unit = new MMDataWorkUnit();
            
        }

        public MeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET api/Me
        public GetViewModel Get()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            return new GetViewModel()
            {
                Hometown = user != null ? user.Hometown : "Unknown",
                Message = user != null ? "Welcome " + user.UserName : "Welcome!",
                UserName = user != null ? user.UserName : "Unknown"
            };
        }
        [HttpGet]
        [Route("api/Me/Profile")]
        public GetViewModel Profile()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user == null)
            {
                return null;
            }
            var client = _unit.GetClient(user.Id,true);
            var requests = _unit.GetMixRequests(client.UserId);
            
            var result = new GetViewModel
            {
                UserId = client.UserId,
                UserName = client.Name,
                Hometown = client.Hometown,
                Email = client.Email,
                Message = "Welcome" + client.Name,
                ClientId = client.Id,
                Requests = requests
            };

            return result;
        }

        [HttpPost]
        [Route("api/Me/Update")]
        public int Update(MixClient client)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var update = _unit.GetClient(client.UserId);
            update.Name = client.Name;
            update.Email = client.Email;
            update.Hometown = client.Hometown;
            _unit.UpdateClient(update);
            var userChanged = false;

            if (user != null)
            {
                if (!Equals(user.Hometown, update.Hometown))
                {
                    user.Hometown = update.Hometown;
                    userChanged = true;
                }
                if (!Equals(user.UserName, update.Name))
                {
                    user.UserName = update.Name;
                    userChanged = true;
                }
                //if (!Equals(user.Email, update.Email))
                //{
                //    user.Email = update.Email;
                //    userChanged = true;
                //}
                if (userChanged)
                {
                    UserManager.Update(user);
                }
            }
            



            return update.Id;
        }

        [HttpPost]
        [Route("api/Me/CancelRequest")]
        public bool CancelRequest(MixRequest request)
        {
           
             return _unit.CancelRequest(request);
            
        }

       

        [HttpPost]
        [Route("api/Me/SubmitMix")]
        public MixRequest SubmitMix(MixRequest request)
        {
            request.EntryTime = DateTime.Now;
            request.AcceptanceTime = DateTime.Now;
            request.MixCancelled = false;
            request.MixComplete = false;
            request.Active = true;
            
           return _unit.StoreMixRequest(request);
        }
    }
}