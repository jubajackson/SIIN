using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Transactions;

using SecondIINoneMC.Web.Data;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.ViewModels;
using SecondIINoneMC.Web.Helpers;
using SecondIINoneMC.Core;
using SecondIINoneMC.Web.Data;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Helpers;
using SecondIINoneMC.Attributes;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Services.Abstract;
using SecondIINoneMC.Web.Data.EntityFramework;

namespace SecondIINoneMC.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly ICharterService _charterService;
        private readonly IOfficerPositionService _officerPositionService;

        public AccountController(IUserService userService,
            IAddressService addressService,
            ICharterService charterService,
            IOfficerPositionService officerPositionService)
        {
            _userService = userService;
            _addressService = addressService;
            _charterService = charterService;
            _officerPositionService = officerPositionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAddresses()
        {
            return Json(_addressService.GetAddressesByUserId((Guid)System.Web.Security.Membership.GetUser().ProviderUserKey, UnitOfWork), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChangePassword(string oldPassword, string newPassword)
        {
            var saved = false;

            var membershipProvider = new CodeFirstMembershipProvider();

            saved = membershipProvider.ChangePassword(System.Web.Security.Membership.GetUser().UserName, oldPassword, newPassword);

            return Json(saved, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteShippingLocation(int id)
        {
            var address = _addressService.GetAddressById(id, UnitOfWork);

            _addressService.DeleteAddress(address, UnitOfWork);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSelectedAddressInformation(int selectedAddressID)
        {
            var address = _addressService.GetAddressById(selectedAddressID, UnitOfWork);

            return Json(address, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAddressFormat(string countryCode)
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SaveShippingLocation(int addressID,
                string country,
                string name,
                string address1,
                string address2,
                string address3,
                string city,
                string state,
                string province,
                string postalCode,
                string phoneNumber)
        {
            var saved = false;

            var userId = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;

            var address = new Address();

            if (addressID > 0)
                address = _addressService.GetAddressById(addressID, UnitOfWork);

            address.Name = name;
            address.Address1 = address1;
            address.Address2 = address2;
            address.Address3 = address3;
            address.City = city;
            address.State = (state == "None" ? null : state);
            address.Province = (province == string.Empty ? null : province);
            address.PostalCode = (postalCode == string.Empty ? null : postalCode);
            address.Country = country;
            address.Phone = phoneNumber;
            address.UserId = userId;
            address.CreatedBy = userId;
            address.CreatedDate = DateTime.Now;
            address.ModifiedBy = userId;
            address.ModifiedDate = DateTime.Now;


            if (addressID > 0)
            {
                _addressService.UpdateAddress(address, UnitOfWork);

                saved = true;
            }
            else
            {
                _addressService.AddAddress(address, UnitOfWork);

                saved = true;
            }

            return Json(saved, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var membershipProvider = new CodeFirstMembershipProvider();

            var user = WebSecurity.GetUser(model.UserName);

            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                SecondIINoneMCWebHelper.SetSessionInformation(model.UserName, _userService, UnitOfWork);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ViewBag.Error = "The user name or password provided is incorrect.";
                model.UserName = "Error";
                return View(model);
            }
        }


        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Charters = new SelectList(_charterService.GetAllActiveCharters(UnitOfWork), "Id", "Name");
            ViewBag.Positions = new SelectList(_officerPositionService.GetAllOfficerPositions(UnitOfWork), "Id", "Name");

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            ViewBag.Charters = new SelectList(_charterService.GetAllActiveCharters(UnitOfWork), "Id", "Name");
            ViewBag.Positions = new SelectList(_officerPositionService.GetAllOfficerPositions(UnitOfWork), "Id", "Name");

            try
            {
                var administratorUserId = new Guid(ConfigurationManager.AppSettings["AdministratorUserId"]);
                
                var userAccountString = WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                var isLoggedIn = WebSecurity.Login(model.UserName, model.Password);

                var newUser = _userService.GetUserByUsername(model.UserName, UnitOfWork);
                newUser.Email = model.UserName;

                _userService.UpdateUser(newUser, UnitOfWork);

                var userProfile = new UserProfile();
                userProfile.UserId = newUser.UserId;
                userProfile.CharterId = model.CharterId;
                userProfile.OfficerPositionId = model.OfficerPositionId;
                userProfile.RidingName = model.RidingName;
                userProfile.Birthday = model.Birthday;
                userProfile.CreatedBy = administratorUserId;
                userProfile.CreatedDate = DateTime.Now;
                userProfile.ModifiedBy = administratorUserId;
                userProfile.ModifiedDate = DateTime.Now;
                newUser.UserProfiles.Add(userProfile);

                _userService.UpdateUser(newUser, UnitOfWork);

                Roles.AddUserToRole(model.UserName, "User");

                if (isLoggedIn)
                    SecondIINoneMCWebHelper.SetSessionInformation(model.UserName, _userService, UnitOfWork);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult ForgotPassword(string username)
        {
            var membershipProvider = new CodeFirstMembershipProvider();

            string newPassword = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);

            var wasPasswordChangeSuccessful = membershipProvider.ChangePassword(username, newPassword);

            if (wasPasswordChangeSuccessful)
                EmailHelper.SendResetPasswordEmail(username, newPassword);

            return Json(wasPasswordChangeSuccessful, JsonRequestBehavior.AllowGet);
        }

        #region Helpers
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

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
