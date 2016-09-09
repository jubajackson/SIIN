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
    public class CharterService : ICharterService
    {
        private IRepository<Charter, Int32> _charterRepository;

        public void AddCharter(Charter charter, IUnitOfWork unitOfWork)
        {
            _charterRepository = unitOfWork.GetRepository<Charter, Int32>();
            _charterRepository.Add(charter);
            unitOfWork.Save();
        }

        public void UpdateCharter(Charter charter, IUnitOfWork unitOfWork)
        {
            _charterRepository = unitOfWork.GetRepository<Charter, Int32>();
            _charterRepository.Update(charter);
            unitOfWork.Save();
        }

        public void DeleteCharter(Charter charter, IUnitOfWork unitOfWork)
        {
            _charterRepository = unitOfWork.GetRepository<Charter, Int32>();
            _charterRepository.Delete(charter);
            unitOfWork.Save();
        }

        public IEnumerable<Charter> GetAllCharters(IUnitOfWork unitOfWork)
        {
            _charterRepository = unitOfWork.GetRepository<Charter, Int32>();
            return _charterRepository.All();
        }

        public IEnumerable<Charter> GetAllActiveCharters(IUnitOfWork unitOfWork)
        {
            _charterRepository = unitOfWork.GetRepository<Charter, Int32>();
            return _charterRepository.Find(x => x.Active);
        }

        public Charter GetCharterById(Int32 id, IUnitOfWork unitOfWork)
        {
            _charterRepository = unitOfWork.GetRepository<Charter, Int32>();
            return _charterRepository.GetById(id);
        }

        public String GetCharterNameByDisplayName(string charterName, IUnitOfWork unitOfWork)
        {
            _charterRepository = unitOfWork.GetRepository<Charter, Int32>();
            return charterName.Replace("Second II None MC ", string.Empty).Replace(" ", string.Empty);
        }
    }
}
