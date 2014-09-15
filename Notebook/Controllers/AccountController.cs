using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Notebook.Models;
using System.Web.Security;
using Notebook.Helpers;

namespace Notebook.Controllers
{
    [Authorize]
    public partial class AccountController : Controller
    {
        [AllowAnonymous]
        public virtual ActionResult Login(string returnUrl)
        {
            try
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
            catch (Exception e) {
                Logger.Log("Login failed", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid && AccountDM.Login(model.UserName, model.Password, model.RememberMe))
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            catch (Exception e)
            {
                Logger.Log("Login failed", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }

        [AllowAnonymous]
        public virtual ActionResult Register()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                Logger.Log("Cann't open register from", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AccountDM.Register(model.UserName, model.Password);
                    AccountDM.Login(model.UserName, model.Password, false);
                    return RedirectToAction("Index", "Home");
                }

                return View(model);
            }
            catch (Exception e)
            {
                Logger.Log("Register form post failed", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOff()
        {
            try
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                Logger.Log("Log out failed", e);
                return View("~/Views/Shared/Error.cshtml");                
            }
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}