using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Transactions;
using System.IO;

using SecondIINoneMC.Web.Data;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Core;
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Helpers;

namespace SecondIINoneMC.Web.Services.Abstract
{
    public interface IVideoGalleryService
    {
        List<string> GetVideos(Int32 categoryId = 0);

        List<KeyValuePair<Int32, String>> GetVideoCategories();

        List<String> GetVideosFromXML(Int32 categoryId = 0);
    }
}
