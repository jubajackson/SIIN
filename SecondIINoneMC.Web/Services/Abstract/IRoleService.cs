using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.Data.Repository;

namespace SecondIINoneMC.Web.Services.Abstract
{
    public interface IRoleService
    {
        void AddRole(Role role, IUnitOfWork unitOfWork);

        void UpdateRole(Role role, IUnitOfWork unitOfWork);

        void DeleteRole(Role role, IUnitOfWork unitOfWork);

        IEnumerable<Role> GetAllRoles(IUnitOfWork unitOfWork);

        Role GetRoleById(Int32 id, IUnitOfWork unitOfWork);
    }
}
