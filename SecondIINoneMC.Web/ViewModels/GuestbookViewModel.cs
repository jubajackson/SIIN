using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;


namespace SecondIINoneMC.Web.ViewModels
{
    public class GuestbookViewModel
    {
        [Display(Name = "Current Page Number:")]
        public int CurrentPageNumber
        {
            get;
            set;
        }

        [Display(Name = "Post Header Message:")]
        public string PostHeaderMessage
        {
            get;
            set;
        }

        [Display(Name = "Post Count:")]
        public int PostCount
        {
            get;
            set;
        }

        [Display(Name = "Id:")]
        public int Id
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Chapter:")]
        public int CharterId
        {
            get;
            set;
        }

        [Display(Name = "Captcha:")]
        public int CaptchaId
        {
            get;
            set;
        }

        [Display(Name = "Captcha Image:")]
        public string CaptchaImage
        {
            get;
            set;
        }

        [Display(Name = "Captcha Entered:")]
        public string CaptchaEntered
        {
            get;
            set;
        }

        [Display(Name = "Chapter:")]
        public string CharterName
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Name:")]
        public string Name
        {
            get;
            set;
        }

        [Display(Name = "Email:")]
        public string Email
        {
            get;
            set;
        }

        [Display(Name = "Mobile Phone:")]
        public string MobilePhone
        {
            get;
            set;
        }

        [Display(Name = "Web Site:")]
        public string Website
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Your Location:")]
        public string Location
        {
            get;
            set;
        }

        [Display(Name = "Your Club:")]
        public string Club
        {
            get;
            set;
        }

        [Required]
        [Display(Name = "Message:")]
        public string Message
        {
            get;
            set;
        }

        [Display(Name = "Deleted:")]
        public Boolean Deleted
        {
            get;
            set;
        }

        [Display(Name = "Created By:")]
        public Guid CreatedBy
        {
            get;
            set;
        }


        [Display(Name = "Date Created:")]
        public DateTime CreatedDate
        {
            get;
            set;
        }

        [Display(Name = "Number:")]
        public int Number
        {
            get;
            set;
        }
    }
}
