using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BrockAllen.MembershipReboot;
using BrockAllen.MembershipReboot.Ef;
using BrockAllen.MembershipReboot.WebHost;
using System.ComponentModel.DataAnnotations;
using EF.Core.Data;

namespace EF.Web.Controllers
{
    public class UserAccountController : Controller
    {
        UserAccountService _userAccountService;
        AuthenticationService _authService;

        public UserAccountController(AuthenticationService authService)
        {
            _userAccountService = authService.UserAccountService;
            _authService = authService;
        }
        //
        // GET: /UserAccount/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(/*new LoginInputModel ()/ new Users()*/);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(/*LoginInputModel model / Users model*/ AspNetUsers model)
        {/*
            if (ModelState.IsValid)
            {
                BrockAllen.MembershipReboot.UserAccount account;
                if (_userAccountService.AuthenticateWithUsernameOrEmail(model.Username, model.Password, out account))
                {
                    _authService.SignIn(account, model.RememberMe);

                    if (account.RequiresTwoFactorAuthCodeToSignIn())
                    {
                        return RedirectToAction("TwoFactorAuthCodeLogin");
                    }
                    if (account.RequiresTwoFactorCertificateToSignIn())
                    {
                        return RedirectToAction("CertificateLogin");
                    }

                    if (_userAccountService.IsPasswordExpired(account))
                    {
                        return RedirectToAction("Index", "ChangePassword");
                    }

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }

            return View(model);
            */
            //пустая заглушка
            return View();
        }

        public ActionResult Register()
        {
            return View( /*new RegisterInputModel() / new Users ()*/);//заглушка
            //RegisterInputModel
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(/*RegisterInputModel model/ Users model*/AspNetUsers model)
        {/*
            if (ModelState.IsValid)
            {
                try
                {
                    var account = _userAccountService.CreateAccount(model.Username, model.Password, model.Email);
                    ViewData["RequireAccountVerification"] = _userAccountService.Configuration.RequireAccountVerification;
                    return View("Success", model);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
            */
            //пустая заглушка
            return View();
        }
    }
}
