using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using SecondIINoneMC.Web.Data;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.ViewModels;
using SecondIINoneMC.Web.Helpers;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Core;
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Core.Enums;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Helpers;
using SecondIINoneMC.Attributes;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Services.Abstract;
using SecondIINoneMC.Web.Data.EntityFramework;

namespace SecondIINoneMC.Web.Controllers
{
    [SecondIINoneMCAuthorize]
    public class AdminController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ICharterService _charterService;
        private readonly IOfficerPositionService _officerPositionService;

        public AdminController(IUserService userService, 
            IRoleService roleService,
            ICharterService charterService,
            IOfficerPositionService officerPositionService)
        {
            _userService = userService;
            _roleService = roleService;
            _charterService = charterService;
            _officerPositionService = officerPositionService;
        }

        public ActionResult Index()
        {
            ViewBag.Charters = new SelectList(_charterService.GetAllActiveCharters(UnitOfWork), "Id", "Name");
            ViewBag.Positions = new SelectList(_officerPositionService.GetAllOfficerPositions(UnitOfWork), "Id", "Name");

            return View();
        }

        [HttpGet]
        public JsonResult SearchUsers(string userSearchCriteria)
        {
            return Json(_userService.SearchUsers(userSearchCriteria, UnitOfWork), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllRoles()
        {
            return Json(_roleService.GetAllRoles(UnitOfWork), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUserByID(string userId)
        {
            var membershipProvider = new CodeFirstMembershipProvider();

            var user = _userService.GetUserById(new Guid(userId), UnitOfWork);
            var userProfile = _userService.GetUserProfileByUserId(new Guid(userId), UnitOfWork);

            var membershipUser = WebSecurity.GetUser(user.Username);

            if (user.IsLockedOut)
            {
                user.IsLockedOut = true;
            }

            var userViewModel = new UserViewModel();
            userViewModel.User = user;
            userViewModel.UserProfile = userProfile;

            return Json(userViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveUser(Guid userId,
                            int charterId,
                            string ridingName,
                            int officerPositionId,
                            string firstName,
                            string lastName,
                            string username,
                            string password,
                            string email,
                            DateTime birthday,
                            bool isLockedOut,
                            bool isApproved,
                            string comment,
                            string facility,
                            string[] roles)
        {
            var user = ((userId.ToString() != string.Empty) ? _userService.GetUserById(userId, UnitOfWork) : null);

            var updaterUserId = (Guid)System.Web.Security.Membership.GetUser().ProviderUserKey;

            var userViewModel = new UserViewModel();
            
            if (user != null)
            {
                var membershipProvider = new CodeFirstMembershipProvider();

                var membershipUser = WebSecurity.GetUser(user.Username);

                if (user.IsLockedOut && !isLockedOut)
                {
                    membershipProvider.UnlockUser(user.Username);
                }

                if (isLockedOut)
                {
                    user.IsLockedOut = true;
                    user.LastLockoutDate = DateTime.Now;
                }

                user.FirstName = firstName;
                user.LastName = lastName;
                user.Username = username;
                user.Email = (email != string.Empty) ? email : user.Username;
                user.IsApproved = isApproved;
                user.Comment = comment;

                var userProfile = user.UserProfiles.FirstOrDefault();
                userProfile.CharterId = charterId;
                userProfile.RidingName = ridingName;
                userProfile.OfficerPositionId = officerPositionId;
                userProfile.Birthday = birthday;
                userProfile.ModifiedBy = updaterUserId;
                userProfile.ModifiedDate = DateTime.Now;

                _userService.UpdateUser(user, UnitOfWork);

                //delete all roles for this user.
                var existingRoles = Roles.GetRolesForUser(user.Username);
                foreach (String roleName in existingRoles)
                {
                    Roles.RemoveUserFromRole(user.Username, roleName);
                }

                userViewModel.UserProfile = userProfile;
            }
            else
            {
                WebSecurity.CreateUserAndAccount(username, password);

                user = _userService.GetUserByUsername(username, UnitOfWork);

                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = (email != string.Empty) ? email : user.Username;
                user.Comment = comment;

                var userProfile = new UserProfile();
                userProfile.UserId = user.UserId;
                userProfile.CharterId = charterId;
                userProfile.RidingName = ridingName;
                userProfile.OfficerPositionId = officerPositionId;
                userProfile.Birthday = birthday;
                userProfile.CreatedBy = updaterUserId;
                userProfile.CreatedDate = DateTime.Now;
                userProfile.ModifiedBy = updaterUserId;
                userProfile.ModifiedDate = DateTime.Now;
                user.UserProfiles.Add(userProfile);

                _userService.UpdateUser(user, UnitOfWork);

                userViewModel.UserProfile = userProfile;
            }


            //create roles for this user.
            Roles.AddUserToRoles(username, roles);

            userViewModel.User = user;
            
            return Json(userViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    var imageFilePath = ConfigurationManager.AppSettings["ImageDirectory"];

                    if (!Directory.Exists(Server.MapPath(imageFilePath)))
                        Directory.CreateDirectory(Server.MapPath(imageFilePath));

                    string savedFileName = string.Empty;
                    var selectedUserId = Request.Form["selectedUserId"];

                    HttpPostedFileBase uploadedImage = Request.Files["imageUpload"] as HttpPostedFileBase;
                    if (uploadedImage.ContentLength > 0)
                    {
                        savedFileName = Path.Combine(Server.MapPath(imageFilePath), selectedUserId + ".jpg");

                        uploadedImage.SaveAs(savedFileName);
                    }

                    ViewBag.Message = "The image was saved.";
                }
                catch (MembershipCreateUserException e)
                {
                    ViewBag.Error = "The image was not saved. " + e.Message;
                }
            }
            else
            {
                ViewBag.Error = "The image did not post for upload.";
            }

            return View();
        }

    }
}
