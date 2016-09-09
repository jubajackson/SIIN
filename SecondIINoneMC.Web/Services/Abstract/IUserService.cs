using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Core;
using SecondIINoneMC.Core.Helpers;
using SecondIINoneMC.Core.Enums;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.Data.Repository;

namespace SecondIINoneMC.Web.Services.Abstract
{
    public interface IUserService
    {
        void AddUser(User user, IUnitOfWork unitOfWork);

        void UpdateUser(User user, IUnitOfWork unitOfWork);

        void DeleteUser(User user, IUnitOfWork unitOfWork);

        IEnumerable<User> GetAllUsers(IUnitOfWork unitOfWork);

        User GetUserById(Guid userId, IUnitOfWork unitOfWork);

        UserProfile GetUserProfileByUserId(Guid userId, IUnitOfWork unitOfWork);

        User GetUserByUsername(string userName, IUnitOfWork unitOfWork);

        IEnumerable<User> GetActiveUsers(IUnitOfWork unitOfWork);

        List<User> SearchUsers(string userSearchCriteria, IUnitOfWork unitOfWork);
    }
}
