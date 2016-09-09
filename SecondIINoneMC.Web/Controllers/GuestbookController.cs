using System;
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
using SecondIINoneMC.Core;
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Helpers;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Controllers
{
    public class GuestbookController : BaseController
    {
        private readonly IGuestbookService _guestbookService;
        private readonly ISecondIINoneMCService _secondIINoneMCService;
        private readonly ICharterService _charterService;

        public GuestbookController(IGuestbookService guestbookService,
            ISecondIINoneMCService secondIINoneMCService, 
            ICharterService charterService)
        {
            _guestbookService = guestbookService;
            _secondIINoneMCService = secondIINoneMCService;
            _charterService = charterService;
        }

        private const int _pageSize = 4;
        //
        // GET: /Guestbook/Index
        [HttpGet]
        public ActionResult Index(int? pageNumber)
        {
            if (pageNumber == null)
                pageNumber = 1;

            var guestbookPosts = _guestbookService.GetAllActiveGuestbookPosts(UnitOfWork).ToList().ConvertAll(x => SecondIINoneMC.Web.Helpers.TranslateHelper.ToGuestbookViewModel(x));
            var postCount = guestbookPosts.Count();

            foreach (GuestbookViewModel guestbook in guestbookPosts)
            {
                guestbook.PostCount = postCount;
                guestbook.PostHeaderMessage = _guestbookService.GetGuestbookPostMessage(_pageSize, Convert.ToInt32(pageNumber), postCount);
                guestbook.CurrentPageNumber = Convert.ToInt32(pageNumber);
            }

            var guestBookPosts = guestbookPosts.OrderByDescending(g => g.Number)
                                    .Skip(_pageSize * (Convert.ToInt32(pageNumber) - 1))
                                    .Take(_pageSize);

            return View(guestBookPosts);
        }

        //
        // GET: /Guestbook/Create
        public ActionResult Create()
        {
            ViewBag.Charters = new SelectList(_charterService.GetAllActiveCharters(UnitOfWork), "Id", "Name");

            var randomNumber = new Random().Next(1, 5);

            var guestBookViewModel = new GuestbookViewModel();
            guestBookViewModel.CaptchaId = randomNumber;
            guestBookViewModel.CaptchaImage = _secondIINoneMCService.GetCaptchaImageFromXML(randomNumber);

            return View(guestBookViewModel);
        }

        //
        // POST: /Guestbook/Create
        [HttpPost]
        public ActionResult Create(GuestbookViewModel guestbookViewModel)
        {
            try
            {
                ViewBag.Charters = new SelectList(_charterService.GetAllActiveCharters(UnitOfWork), "Id", "Name");

                var captcha = _secondIINoneMCService.GetCaptchaTextFromXML(guestbookViewModel.CaptchaId);
                var enteredCaptcha = guestbookViewModel.CaptchaEntered;

                if (captcha.ToLower() != enteredCaptcha.ToLower())
                {
                    var captchaError = "The code you entered is not valid.";
                    ViewBag.Error = captchaError;
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        guestbookViewModel.CreatedDate = DateTime.Now;
                        guestbookViewModel.CreatedBy = new Guid(ConfigurationManager.AppSettings["AdministratorUserId"]);

                        _guestbookService.AddGuestbook(SecondIINoneMC.Web.Helpers.TranslateHelper.ToGuestbookModelObject(guestbookViewModel), UnitOfWork);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        SecondIINoneMCHelper.AddErrorsToModelState(ModelState);
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Unable to add this Guestbook Post.";
            }

            // If we got this far, something failed, redisplay form
            return View(guestbookViewModel);
        }
    }
}
