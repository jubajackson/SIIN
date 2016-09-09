using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Net;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using Newtonsoft.Json;

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

namespace SecondIINoneMC.Web.Controllers
{
    public class VideoGalleryController : BaseController
    {
        private const int _pageSize = 12;

        public ActionResult Index(int? categoryId, int pageNumber = 1)
        {
            if(categoryId == null)
                categoryId = 1;

            var videos = GetVideoViewModelList(Convert.ToInt32(categoryId), pageNumber);

            return View(videos);
        }

        private List<VideoGalleryViewModel> GetVideoViewModelList(Int32 categoryId = 0, Int32 pageNumber = 1)
        {
            List<VideoGalleryViewModel> VideoDataList = new List<VideoGalleryViewModel>();

            var videos = new VideoGalleryService().GetVideos(categoryId);

            var videoCount = videos.Count();

            var videosToDisplay = videos
                .Skip(_pageSize * (pageNumber - 1))
                .Take(_pageSize)
                .ToList();

            foreach (String video in videosToDisplay)
            {
                WebRequest request = WebRequest.Create("https://www.googleapis.com/youtube/v3/videos?id=" + video + "&key=AIzaSyAkRZ2Zk6CFC9peZvrceamAICS3PucCHs4&part=snippet,statistics,contentDetails");

                request.Method = "GET";
                string postData = string.Empty;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = byteArray.Length;

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        try
                        {
                            dynamic dynObj = JsonConvert.DeserializeObject(reader.ReadToEnd());

                            foreach (var item in dynObj.items)
                            {
                                var videoViewModel = TranslateHelper.ToVideoGalleryViewModel(item);
                                videoViewModel.VideoCount = videoCount;
                                videoViewModel.CurrentPageNumber = pageNumber;
                                videoViewModel.CategoryId = categoryId;

                                VideoDataList.Add(videoViewModel);
                            }
                        }
                        catch (Exception) { }
                    }

                }
            }

            return VideoDataList;
        }
    }
}
