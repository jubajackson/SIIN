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
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Controllers
{
    public class PhotoGalleryController : BaseController
    {
        private readonly ICharterService _charterService;

        public PhotoGalleryController(ICharterService charterService)
        {
            _charterService = charterService;
        }


        public ActionResult Index()
        {
            var charters = _charterService.GetAllActiveCharters(UnitOfWork).ToList();

            charters = charters.Select(x => { x.Name = x.Name.Replace("Second II None MC ", string.Empty).ToUpper(); return x; }).ToList();

            return View(charters);
        }

        public ActionResult ViewPhotos()
        {
            return View();
        }
    }
}
