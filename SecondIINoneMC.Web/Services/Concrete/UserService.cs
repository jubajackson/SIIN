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
using SecondIINoneMC.Web.Services;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Services.Concrete
{
    public class UserService : IUserService
    {
        private IRepository<User, Int32> _userRepository;

        public void AddUser(User user, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            _userRepository.Add(user);
            unitOfWork.Save();
        }

        public void UpdateUser(User user, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            _userRepository.Update(user);
            unitOfWork.Save();
        }

        public void DeleteUser(User user, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            _userRepository.Delete(user);
            unitOfWork.Save();
        }

        public IEnumerable<User> GetAllUsers(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            return _userRepository.All();
        }

        public User GetUserById(Guid userId, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            var user = _userRepository.Find(x => x.UserId == userId).FirstOrDefault();

            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public UserProfile GetUserProfileByUserId(Guid userId, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            var user = _userRepository.Find(x => x.UserId == userId).FirstOrDefault();

            if (user != null)
            {
                return user.UserProfiles.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public User GetUserByUsername(string userName, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            return _userRepository.Find(x => x.Username == userName).FirstOrDefault();
        }

        public IEnumerable<User> GetActiveUsers(IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            return _userRepository.Find(x => x.IsApproved == true && x.IsLockedOut == false);
        }

        public List<User> SearchUsers(string userSearchCriteria, IUnitOfWork unitOfWork)
        {
            _userRepository = unitOfWork.GetRepository<User, Int32>();
            var users = _userRepository.Find(user => string.Concat(user.FirstName + " " + user.LastName + "" + user.Username + " " + user.Email + " " + user.Comment).ToLower().Contains(userSearchCriteria.ToLower())).ToList();

            if (users != null)
            {
                return users;
            }
            else
            {
                return null;
            }
        }
    }
}
