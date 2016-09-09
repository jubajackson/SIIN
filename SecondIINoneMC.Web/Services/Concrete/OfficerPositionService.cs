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
    public class OfficerPositionService : IOfficerPositionService
    {
        private IRepository<OfficerPosition, Int32> _officerPositionRepository;

        public void AddOfficerPosition(OfficerPosition officerPosition, IUnitOfWork unitOfWork)
        {
            _officerPositionRepository = unitOfWork.GetRepository<OfficerPosition, Int32>();
            _officerPositionRepository.Add(officerPosition);
            unitOfWork.Save();
        }

        public void UpdateOfficerPosition(OfficerPosition officerPosition, IUnitOfWork unitOfWork)
        {
            _officerPositionRepository = unitOfWork.GetRepository<OfficerPosition, Int32>();
            _officerPositionRepository.Update(officerPosition);
            unitOfWork.Save();
        }

        public void DeleteOfficerPosition(OfficerPosition officerPosition, IUnitOfWork unitOfWork)
        {
            _officerPositionRepository = unitOfWork.GetRepository<OfficerPosition, Int32>();
            _officerPositionRepository.Delete(officerPosition);
            unitOfWork.Save();
        }

        public IEnumerable<OfficerPosition> GetAllOfficerPositions(IUnitOfWork unitOfWork)
        {
            _officerPositionRepository = unitOfWork.GetRepository<OfficerPosition, Int32>();
            return _officerPositionRepository.All();
        }

        public OfficerPosition GetOfficerPositionById(Int32 id, IUnitOfWork unitOfWork)
        {
            _officerPositionRepository = unitOfWork.GetRepository<OfficerPosition, Int32>();
            return _officerPositionRepository.GetById(id);
        }
    }
}
