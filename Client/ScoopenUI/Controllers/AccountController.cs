using RegistrationAndLogin.Api;
using ScoopenAPIModals.Notifications;
using ScoopenModals.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace RegistrationAndLogin.Controllers
{
    public class AccountController : Controller
    {
        RegisterApiController registerApiController = new RegisterApiController();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Exclude = "Id, UserId")] UserInfo userInfo)
        {
            var response = registerApiController.RegisterUser(userInfo);

            if (response != null && response.IsSuccessStatusCode)
            {
                Session["RegisteredUserDetails"] = userInfo;
                return RedirectToAction("ConfirmRegister");
            }

            ModelState.AddModelError("FirstName", "Error in registration");
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmRegister(string otp)
        {
            var userDetails = (UserInfo)Session["RegisteredUserDetails"];

            if (userDetails != null)
            {
                OtpRequest otpRequest = new OtpRequest()
                {
                    Email = userDetails.Email,
                    Mobile = userDetails.Mobile,
                    Otp = otp,
                    SessionId = string.Empty // this is required only to verify otp sent over sms, if we have used otp service
                };

                var response = registerApiController.ActivateRegisteredUser(otpRequest);

                if (response != null && response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "We have sent you login details on your email and mobile.";
                }
                else
                {
                    ViewBag.SendOtpAgain = true;
                }
            }

            ModelState.AddModelError("otp", "Error in otp confirmation");
            return View();
        }

        [HttpGet]
        public ActionResult ResendOtp()
        {
            // Logic to send otp details one more time
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginRequest login)
        {
            // use to hash password
            //string pwd = Crypto.Hash(login.Password);

            var response = registerApiController.Authenticate(login);

            if (response != null && response.IsSuccessStatusCode)
            {
                int timeout = 20;
                var ticket = new FormsAuthenticationTicket(login.Username, true, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                Session["UserName"] = login.Username;

                // call change passwor method on first time login
                //return RedirectToAction("ChangePassword");

                return RedirectToAction("Welcome", "Home");
            }
            else
            {
                ViewBag.LoginError = "Error in login";
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string confirmNewPassword)
        {
            if (newPassword == confirmNewPassword)
            {
                string userName = Session["UserName"] != null ? Session["UserName"].ToString() : string.Empty;

                // use Crypto.Hash(newPassword)  to hash password
                LoginRequest request = new LoginRequest() { Username = userName, Password = newPassword };

                var response = registerApiController.ChangePasswordOnFirstLogin(request);

                if (response != null && response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Welcome", "Home");
                }
            }
            return View();
        }
    }
}