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
    public interface IAddressService
    {
        void AddAddress(Address address, IUnitOfWork unitOfWork);

        void UpdateAddress(Address address, IUnitOfWork unitOfWork);

        void DeleteAddress(Address address, IUnitOfWork unitOfWork);

        IEnumerable<Address> GetAllAddresss(IUnitOfWork unitOfWork);

        IEnumerable<Address> GetAddressesByUserId(Guid userId, IUnitOfWork unitOfWork);

        Address GetAddressById(Int32 id, IUnitOfWork unitOfWork);
    }
}
