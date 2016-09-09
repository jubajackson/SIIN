using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SecondIINoneMC.Web;
using SecondIINoneMC.Web.Controllers;
using SecondIINoneMC.Web.Data;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.ViewModels;
using SecondIINoneMC.Web.Helpers;
using SecondIINoneMC.Core;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Helpers;
using SecondIINoneMC.Attributes;

namespace SecondIINoneMC.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest : BaseController
    {
        [TestMethod]
        public void Index()
        {
            
        }
    }
}
