using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.Data.Repository;

namespace SecondIINoneMC.Web.Services.Abstract
{
    public interface ISecondIINoneMCService
    {
        String GetCaptchaImageFromXML(Int32 captchaId);

        String GetCaptchaTextFromXML(Int32 captchaId);
    }
}
