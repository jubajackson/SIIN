//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecondIINoneMC.Web.Data.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Guestbook
    {
        public int Id { get; set; }
        public int CharterId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Website { get; set; }
        public string Location { get; set; }
        public string Club { get; set; }
        public string Message { get; set; }
        public bool Deleted { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Charter Charter { get; set; }
    }
}
