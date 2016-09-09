using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SecondIINoneMC.Web.Data;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.ViewModels;
using SecondIINoneMC.Web.Helpers;
using SecondIINoneMC.Core;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Web.Services.Abstract;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Data.EntityFramework;

namespace SecondIINoneMC.Web.Helpers
{
    public static class SecondIINoneMCWebHelper
    {
        public static void AddErrorsToModelState(ModelStateDictionary modelState)
        {
            var errorList = new List<string>();

            foreach (var obj in modelState.Values)
            {
                foreach (var error in obj.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                        errorList.Add(error.ErrorMessage);
                }
            }

            foreach (var message in errorList)
                modelState.AddModelError("", message);
        }

        public static void SetSessionInformation(string username, IUserService userService, IUnitOfWork unitOfWork)
        {
            var context = HttpContext.Current;

            var userId = (Guid)WebSecurity.GetUser(username).ProviderUserKey;

            var user = userService.GetUserById(userId, unitOfWork);
            var userProfile = userService.GetUserProfileByUserId(userId, unitOfWork);

            if (user != null)
            {
                context.Session.Add("UserId", user.UserId);
                context.Session.Add("Name", user.FirstName + " " + user.LastName);
                context.Session.Add("IsOfficer", userProfile.OfficerPositionId < 14 ? true : false);
                context.Session.Add("User", user);
            }
        }
    }
}