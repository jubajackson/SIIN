using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecondIINoneMC.Web.Data.Repository.Abstract;
using SecondIINoneMC.Web.Data.Repository.Concrete;
using SecondIINoneMC.Web.Data.Entities;
using SecondIINoneMC.Web.Data.Repository;

namespace SecondIINoneMC.Web.Services.Abstract
{
    public interface IGuestbookService
    {
        void AddGuestbook(Guestbook guestbook, IUnitOfWork unitOfWork);

        void UpdateGuestbook(Guestbook guestbook, IUnitOfWork unitOfWork);

        void DeleteGuestbook(Guestbook guestbook, IUnitOfWork unitOfWork);

        IEnumerable<Guestbook> GetAllGuestbookPosts(IUnitOfWork unitOfWork);

        Guestbook GetGuestbookById(Int32 id, IUnitOfWork unitOfWork);

        List<Guestbook> GetAllActiveGuestbookPosts(IUnitOfWork unitOfWork);

        List<Guestbook> GetGuestbookPostsByCharterId(int charterId, IUnitOfWork unitOfWork);

        string GetGuestbookPostMessage(int pageSize, int pageNumber, int postCount);
    }
}
