using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecondIINoneMC.Web.ViewModels
{
    public class VideoGalleryViewModel
    {
        public String Id
        {
            get;
            set;
        }

        public String Title
        {
            get;
            set;
        }

        public String Description
        {
            get;
            set;
        }

        public DateTime Published
        {
            get;
            set;
        }

        public String Duration
        {
            get;
            set;
        }

        public Int32 ViewCount
        {
            get;
            set;
        }

        public Int32 LikeCount
        {
            get;
            set;
        }

        public String Thumbnail
        {
            get;
            set;
        }

        public String HtmlElementName
        {
            get;
            set;
        }

        public Int32 VideoCount
        {
            get;
            set;
        }

        public Int32 CurrentPageNumber
        {
            get;
            set;
        }

        public Int32 CategoryId
        {
            get;
            set;
        }

    }
}
