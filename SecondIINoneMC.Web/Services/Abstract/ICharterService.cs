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
    public interface ICharterService
    {
        void AddCharter(Charter charter, IUnitOfWork unitOfWork);

        void UpdateCharter(Charter charter, IUnitOfWork unitOfWork);

        void DeleteCharter(Charter charter, IUnitOfWork unitOfWork);

        IEnumerable<Charter> GetAllCharters(IUnitOfWork unitOfWork);

        IEnumerable<Charter> GetAllActiveCharters(IUnitOfWork unitOfWork);

        Charter GetCharterById(Int32 id, IUnitOfWork unitOfWork);

        String GetCharterNameByDisplayName(string charterName, IUnitOfWork unitOfWork);
    }
}
