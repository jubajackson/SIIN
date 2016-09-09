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
    public class CalendarEventViewModel
    {
        public int Id { get; set; }
        public bool IsPrivate { get; set; }
        public int CharterId { get; set; }
        public string Name { get; set; }
        public int EventTypeId { get; set; }
        public string Description { get; set; }
        public System.DateTime Startdate { get; set; }
        public System.DateTime Enddate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WebAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> Zip { get; set; }
        public int RecurrenceTypeId { get; set; }
        public Nullable<int> RecurrenceFrequencyTypeId { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}
