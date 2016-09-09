using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Data.EntityFramework;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;

namespace SecondIINoneMC.Web.Controllers
{
    public class BaseController : Controller
    {
        private IUnitOfWork _unitOfWork = null;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new UnitOfWork<SIINMCDataEntitiesContext>();

                return _unitOfWork;
            }
            set
            {
                _unitOfWork = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}
