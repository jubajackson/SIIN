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
    
    public partial class OfficerPosition
    {
        public OfficerPosition()
        {
            this.UserProfiles = new HashSet<UserProfile>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.Guid ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}