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
using SecondIINoneMC.Web.Services;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Services.Concrete
{
    public class SecondIINoneMCService : ISecondIINoneMCService
    {
        private const String _captchaXMLFileLocation = "http://www.secondIInoneMC.com/xml/Captchas.xml";
        
        public String GetCaptchaImageFromXML(Int32 captchaId)
        {
            XDocument xdoc = XDocument.Load(_captchaXMLFileLocation);

            var captchaImage = from captcha in xdoc.Descendants(@"captcha").Where(x => Convert.ToInt32(x.Element("id").Value) == captchaId)
                               select captcha.Element("image");

            return captchaImage.FirstOrDefault().Value;
        }

        public String GetCaptchaTextFromXML(Int32 captchaId)
        {
            XDocument xdoc = XDocument.Load(_captchaXMLFileLocation);

            var captchaText = from captcha in xdoc.Descendants(@"captcha").Where(x => Convert.ToInt32(x.Element("id").Value) == captchaId)
                               select captcha.Element("text");

            return captchaText.FirstOrDefault().Value;
        }
    }
}
