using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace SecondIINoneMC.Web.ViewModels
{
    public class ContactUsViewModel
    {
        [Required]
        [Display(Name = "Charter")]
        public int CharterId { get; set; }

        [Display(Name = "Captcha:")]
        public int CaptchaId { get; set; }

        [Display(Name = "Captcha Image:")]
        public string CaptchaImage { get; set; }

        [Display(Name = "Captcha Entered:")]
        public string CaptchaEntered { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Your username must be a valid email address.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
