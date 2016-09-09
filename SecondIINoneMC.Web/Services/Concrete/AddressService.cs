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
    public class AddressService : IAddressService
    {
        private IRepository<Address, Int32> _addressRepository;

        public void AddAddress(Address address, IUnitOfWork unitOfWork)
        {
            _addressRepository = unitOfWork.GetRepository<Address, Int32>();
            _addressRepository.Add(address);
            unitOfWork.Save();
        }

        public void UpdateAddress(Address address, IUnitOfWork unitOfWork)
        {
            _addressRepository = unitOfWork.GetRepository<Address, Int32>();
            _addressRepository.Update(address);
            unitOfWork.Save();
        }

        public void DeleteAddress(Address address, IUnitOfWork unitOfWork)
        {
            _addressRepository = unitOfWork.GetRepository<Address, Int32>();
            _addressRepository.Delete(address);
            unitOfWork.Save();
        }

        public IEnumerable<Address> GetAllAddresss(IUnitOfWork unitOfWork)
        {
            _addressRepository = unitOfWork.GetRepository<Address, Int32>();
            return _addressRepository.All();
        }

        public IEnumerable<Address> GetAddressesByUserId(Guid userId, IUnitOfWork unitOfWork)
        {
            _addressRepository = unitOfWork.GetRepository<Address, Int32>();
            return _addressRepository.Find(x => x.User.UserId == userId);
        }

        public Address GetAddressById(Int32 id, IUnitOfWork unitOfWork)
        {
            _addressRepository = unitOfWork.GetRepository<Address, Int32>();
            return _addressRepository.GetById(id);
        }
    }
}
