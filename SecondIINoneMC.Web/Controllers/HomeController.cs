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
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Helpers;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ISecondIINoneMCService _secondIINoneMCService;
        private readonly ICharterService _charterService;

        public HomeController(ISecondIINoneMCService secondIINoneMCService,
            ICharterService charterService)
        {
            _secondIINoneMCService = secondIINoneMCService;
            _charterService = charterService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            var charters = _charterService.GetAllActiveCharters(UnitOfWork).ToList();

            var charter = new Charter();
            charter.Id = 0;
            charter.Name = "Second II None MC National";

            charters.Insert(0, charter);

            ViewBag.Charters = new SelectList(charters, "Id", "Name"); ;

            var randomNumber = new Random().Next(1, 5);

            var contactUsViewModel = new ContactUsViewModel();
            contactUsViewModel.CaptchaId = randomNumber;
            contactUsViewModel.CaptchaImage = _secondIINoneMCService.GetCaptchaImageFromXML(randomNumber);

            return View(contactUsViewModel);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsViewModel contactUsViewModel)
        {
            var charter = _charterService.GetCharterById(contactUsViewModel.CharterId, UnitOfWork);

            ViewBag.Charters = new SelectList(_charterService.GetAllActiveCharters(UnitOfWork), "Id", "Name");
            ViewBag.Message = @"Your email to " + charter.Name + " has been sent.";

            var captcha = _secondIINoneMCService.GetCaptchaTextFromXML(contactUsViewModel.CaptchaId);
            var enteredCaptcha = contactUsViewModel.CaptchaEntered;

            if (captcha.ToLower() != enteredCaptcha.ToLower())
            {
                var captchaError = "The code you entered is not valid.";
                ViewBag.Error = captchaError;
            }
            else
            {
                EmailHelper.SendEmail(contactUsViewModel.Email, _charterService.GetCharterNameByDisplayName(charter.Name, UnitOfWork) + "@SecondIInoneMC.com", contactUsViewModel.Subject, contactUsViewModel.Message);

                ModelState.Clear();

                var randomNumber = new Random().Next(1, 5);

                contactUsViewModel = new ContactUsViewModel();
                contactUsViewModel.CaptchaId = randomNumber;
                contactUsViewModel.CaptchaImage = _secondIINoneMCService.GetCaptchaImageFromXML(randomNumber);
            }

            return View(contactUsViewModel);
        }

        public ActionResult Sponsors()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }
    }
}
