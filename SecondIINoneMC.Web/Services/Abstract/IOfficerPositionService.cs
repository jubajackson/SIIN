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
    public interface IOfficerPositionService
    {
        void AddOfficerPosition(OfficerPosition officerPosition, IUnitOfWork unitOfWork);

        void UpdateOfficerPosition(OfficerPosition officerPosition, IUnitOfWork unitOfWork);

        void DeleteOfficerPosition(OfficerPosition officerPosition, IUnitOfWork unitOfWork);

        IEnumerable<OfficerPosition> GetAllOfficerPositions(IUnitOfWork unitOfWork);

        OfficerPosition GetOfficerPositionById(Int32 id, IUnitOfWork unitOfWork);
    }
}
