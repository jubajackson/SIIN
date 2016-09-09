using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SecondIINoneMC.Web.Data.Entities;

namespace SecondIINoneMC.Web.ViewModels
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
