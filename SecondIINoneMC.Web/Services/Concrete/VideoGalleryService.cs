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
using SecondIINoneMC.Web.Services;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Services.Concrete
{
    public class VideoGalleryService : IVideoGalleryService
    {
        private const String _videoXMLFileLocation = "http://www.secondIInoneMC.com/xml/Videos.xml";
        private const String _videoCategoriesXMLFileLocation = "http://www.secondIInoneMC.com/xml/VideoCategories.xml";
        private const String _charterXMLFileLocation = "http://www.secondIInoneMC.com/xml/charters.xml";
        private const String _videoIdXMLElement = "id";
        private const String _videoCategoryXMLElement = "Category";

        public List<string> GetVideos(Int32 categoryId = 0)
        {
            return GetVideosFromXML(categoryId);
        }

        public List<KeyValuePair<Int32, String>> GetVideoCategories()
        {
            List<KeyValuePair<Int32, String>> videoCategoryList = new List<KeyValuePair<Int32, String>>();

            XDocument xdoc = XDocument.Load(_videoCategoriesXMLFileLocation);

            Int32 id = 0;
            String name = string.Empty;

            var videoCategories = from videoCategory in xdoc.Descendants(@"category")
                                  select videoCategory;

            foreach (XElement x in videoCategories)
            {
                id = Convert.ToInt32(x.Element("id").Value);
                name = x.Element("name").Value;

                videoCategoryList.Add(new KeyValuePair<Int32, String>(id, name));
            }

            return videoCategoryList;
        }

        public List<String> GetVideosFromXML(Int32 categoryId = 0)
        {
            List<String> videoIdList = new List<String>();

            XDocument xdoc = XDocument.Load(_videoXMLFileLocation);

            IEnumerable<XElement> videoIds = null;

            if (categoryId > 0)
            {
                videoIds = from video in xdoc.Descendants(@"video").Where(v => Convert.ToInt32(v.Element("categoryId").Value) == categoryId)
                           select video.Element("id");
            }
            else
            {
                videoIds = from videoId in xdoc.Descendants(@"id")
                           select videoId;
            }

            foreach (String videoId in videoIds)
            {
                videoIdList.Add(videoId);
            }

            return videoIdList;
        }
    }
}
