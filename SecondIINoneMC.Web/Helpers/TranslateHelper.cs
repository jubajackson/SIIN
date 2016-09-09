using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

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
using SecondIINoneMC.Core.Enums;
using SecondIINoneMC.Web.Services.Concrete;
using SecondIINoneMC.Web.Data.EntityFramework;

namespace SecondIINoneMC.Web.Helpers
{
    public static class TranslateHelper
    {
        private static IUnitOfWork _unitOfWork = null;
        public static IUnitOfWork UnitOfWork
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

        #region MODEL OBJECT TRANSLATIONS

        //internal static SecondIINoneMC.Web.ViewModels.CalendarEventViewModel ToCalendarEventViewModel(SecondIINoneMC.Web.Data.Entities.CalendarEvent calendarEvent)
        //{
        //    return new SecondIINoneMC.Web.ViewModels.CalendarEventViewModel
        //    {
        //        Id = calendarEvent.Id,
        //        Name = calendarEvent.Name,
        //        Description = calendarEvent.Description,
        //        CharterId = calendarEvent.CharterId,
        //        Startdate = calendarEvent.Startdate,
        //        Enddate = calendarEvent.Enddate,
        //        PhoneNumber = calendarEvent.PhoneNumber,
        //        Email = calendarEvent.Email,
        //        WebAddress = calendarEvent.WebAddress,
        //        Address = calendarEvent.Address,
        //        City = calendarEvent.City,
        //        State = calendarEvent.State,
        //        Zip = Convert.ToInt32(calendarEvent.Zip),
        //        EventTypeId = calendarEvent.EventTypeId,
        //        RecurrenceTypeId = calendarEvent.RecurrenceTypeId,
        //        CreatedBy = calendarEvent.CreatedBy,
        //        CreatedDate = calendarEvent.CreatedDate,
        //        ModifiedBy = calendarEvent.ModifiedBy,
        //        ModifiedDate = calendarEvent.ModifiedDate
        //    };
        //}

        internal static SecondIINoneMC.Web.ViewModels.VideoGalleryViewModel ToVideoGalleryViewModel(dynamic item)
        {
            TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(item.contentDetails.duration.ToString());

            return new SecondIINoneMC.Web.ViewModels.VideoGalleryViewModel
            {
                Id = item.id,
                Title = item.snippet.title,
                Description = item.snippet.description,
                Published = item.snippet.publishedAt,
                Duration = youTubeDuration.ToString(),
                ViewCount = item.statistics.viewCount,
                Thumbnail = item.snippet.thumbnails.high.url
            };
        }

        internal static SecondIINoneMC.Web.ViewModels.GuestbookViewModel ToGuestbookViewModel(SecondIINoneMC.Web.Data.Entities.Guestbook guestbook)
        {
            var charter = new CharterService().GetCharterById(guestbook.CharterId, UnitOfWork);

            return new SecondIINoneMC.Web.ViewModels.GuestbookViewModel
            {
                Id = guestbook.Id,
                CharterId = guestbook.CharterId,
                CharterName = charter.Name,
                Name = guestbook.Name,
                Message = guestbook.Message,
                Club = guestbook.Club,
                Email = guestbook.Email,
                Location = guestbook.Location,
                MobilePhone = guestbook.MobilePhone,
                Website = guestbook.Website,
                Deleted = guestbook.Deleted,
                CreatedBy = guestbook.CreatedBy,
                CreatedDate = guestbook.CreatedDate,
                Number = guestbook.Id
            };
        }

        #endregion

        #region VIEWMODEL TO MODEL TRANSLATIONS

        public static SecondIINoneMC.Web.Data.Entities.Guestbook ToGuestbookModelObject(SecondIINoneMC.Web.ViewModels.GuestbookViewModel guestbook)
        {
            var guestbookObject = new SecondIINoneMC.Web.Data.Entities.Guestbook();

            if (guestbook.Id > 0)
                guestbookObject = new GuestbookService().GetGuestbookById(guestbook.Id, UnitOfWork);
            else
                guestbookObject.CreatedDate = DateTime.Now;

            guestbookObject.Id = guestbook.Id;
            guestbookObject.CharterId = guestbook.CharterId;
            guestbookObject.Name = guestbook.Name;
            guestbookObject.Message = guestbook.Message;
            guestbookObject.Club = guestbook.Club;
            guestbookObject.Email = guestbook.Email;
            guestbookObject.Location = guestbook.Location;
            guestbookObject.MobilePhone = guestbook.MobilePhone;
            guestbookObject.Website = guestbook.Website;
            guestbookObject.Deleted = guestbook.Deleted;

            return guestbookObject;
        }

        #endregion
    }
}
