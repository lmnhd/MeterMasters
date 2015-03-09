using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MeterMasters.Models;
using MMUserContext.Concrete;

namespace MeterMasters
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager which is used in this application.
    public class ApplicationUserManager : UserManager<ApplicationUser,string>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, string> store)
            : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser,ApplicationRole,string,ApplicationUserLogin,ApplicationUserRole,ApplicationUserClaim>(context.Get<ApplicationDbContext>()));
           
           
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.  
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager,DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

        public class ApplicationRoleManager : RoleManager<ApplicationRole>

    {

        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)

            : base(roleStore)

        {

        }

 

        public static ApplicationRoleManager Create(

            IdentityFactoryOptions<ApplicationRoleManager> options, 

            IOwinContext context)

        {

            return new ApplicationRoleManager(

                new ApplicationRoleStore(context.Get<ApplicationDbContext>()));

        }

    }





    public class ApplicationDbInitializer

        : DropCreateDatabaseIfModelChanges<ApplicationDbContext>

    {
        

        protected override void Seed(ApplicationDbContext context)

        {

            InitializeIdentityForEF(context);

            base.Seed(context);

        }



        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        

        public static void InitializeIdentityForEF(ApplicationDbContext db)

        {
            var _unit = new MMDataWorkUnit();
            
            var userManager = HttpContext.Current

                .GetOwinContext().GetUserManager<ApplicationUserManager>();



            var roleManager = HttpContext.Current

                .GetOwinContext().Get<ApplicationRoleManager>();



            const string name = "admin@metermastersonline.co";

            const string password = "Rollpop1!";

            const string roleName = "Admin";



            //Create Role Admin if it does not exist

            var role = roleManager.FindByName(roleName);

            if (role == null)

            {

                role = new ApplicationRole(roleName);

                var roleresult = roleManager.Create(role);

            }



            var user = userManager.FindByName(name);

            if (user == null)

            {

                user = new ApplicationUser {UserName = name, Email = name};

                var result = userManager.Create(user, password);

                result = userManager.SetLockoutEnabled(user.Id, false);

                _unit.StoreClient(user.Id, user.UserName, user.Hometown);
                



            }



            // Add user admin to Role Admin if not already added

            var rolesForUser2 = userManager.GetRoles(user.Id);

            if (!rolesForUser2.Contains(role.Name))

            {

                var result = userManager.AddToRole(user.Id, role.Name);

            }

                var rolesForUser = userManager.GetRoles(user.Id);

    if (!rolesForUser.Contains(role.Name))

    {

        userManager.AddToRole(user.Id, role.Name);

    }

 

    // Initial Vanilla User:

    const string vanillaUserName = "vanillaUser@example.com";

    const string vanillaUserPassword = "Rollpop1!";

 

    // Add a plain vannilla Users Role:

    const string usersRoleName = "Users";

    const string usersRoleDescription = "Plain vanilla User";

 

    //Create Role Users if it does not exist

    var usersRole = roleManager.FindByName(usersRoleName);

    if (usersRole == null)

    {

        usersRole = new ApplicationRole(usersRoleName);

 

        // Set the new custom property:

        usersRole.Description = usersRoleDescription;

        var userRoleresult = roleManager.Create(usersRole);

    }

 

    // Create Vanilla User:

    var vanillaUser = userManager.FindByName(vanillaUserName);

    if (vanillaUser == null)

    {

        vanillaUser = new ApplicationUser 

        { 

            UserName = vanillaUserName, 

            Email = vanillaUserName 

        };

 

        // Set the new custom properties:

        //vanillaUser.Address = address;

        //vanillaUser.City = city;

        //vanillaUser.State = state;

        //vanillaUser.PostalCode = postalCode;

 

        var result = userManager.Create(vanillaUser, vanillaUserPassword);

        result = userManager.SetLockoutEnabled(vanillaUser.Id, false);

    }

 

    // Add vanilla user to Role Users if not already added

    var rolesForVanillaUser = userManager.GetRoles(vanillaUser.Id);

    if (!rolesForVanillaUser.Contains(usersRole.Name))

    {

        userManager.AddToRole(vanillaUser.Id, usersRole.Name);

        _unit.StoreClient(user.Id, user.Email, user.Hometown);

    }




        }

    }



}
