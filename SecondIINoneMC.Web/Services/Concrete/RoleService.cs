using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.Data.Repository;
using SecondIINoneMC.Web.Services;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Services.Concrete
{
    public class RoleService : IRoleService
    {
        private IRepository<Role, Int32> _roleRepository;

        public void AddRole(Role role, IUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.GetRepository<Role, Int32>();
            _roleRepository.Add(role);
            unitOfWork.Save();
        }

        public void UpdateRole(Role role, IUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.GetRepository<Role, Int32>();
            _roleRepository.Update(role);
            unitOfWork.Save();
        }

        public void DeleteRole(Role role, IUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.GetRepository<Role, Int32>();
            _roleRepository.Delete(role);
            unitOfWork.Save();
        }

        public IEnumerable<Role> GetAllRoles(IUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.GetRepository<Role, Int32>();
            return _roleRepository.All();
        }

        public Role GetRoleById(Int32 id, IUnitOfWork unitOfWork)
        {
            _roleRepository = unitOfWork.GetRepository<Role, Int32>();
            return _roleRepository.GetById(id);
        }
    }
}
