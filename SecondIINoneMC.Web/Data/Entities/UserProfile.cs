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
    
    public partial class UserProfile
    {
        public int Id { get; set; }
        public System.Guid UserId { get; set; }
        public Nullable<int> CharterId { get; set; }
        public string RidingName { get; set; }
        public Nullable<int> OfficerPositionId { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Picture { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual Charter Charter { get; set; }
        public virtual OfficerPosition OfficerPosition { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
