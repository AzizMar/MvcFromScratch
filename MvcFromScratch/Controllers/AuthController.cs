using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using MvcFromScratch.Models;

namespace MvcFromScratch.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }


        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid) //Checks if input fields have the correct format
            {
                return View(model); //Returns the view with the input values so that the user doesn't have to retype again
            }

            if (model.Email == "admin@admin.com" & model.Password == "123456")
            {
                 var identity = new ClaimsIdentity(new[] {
                     new Claim(ClaimTypes.Name, "Xtian"),
                      new Claim(ClaimTypes.Email, "xtian@email.com"),
                      new Claim(ClaimTypes.Country, "Philippines")
                }, 

                "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            return View(model); //Should always be declared on the end of an action method
        }
    }
}