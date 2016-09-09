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
using SecondIINoneMC.Web.Services;
using SecondIINoneMC.Web.Services.Abstract;

namespace SecondIINoneMC.Web.Services.Concrete
{
    public class GuestbookService : IGuestbookService
    {
        private IRepository<Guestbook, Int32> _guestbookRepository;

        public void AddGuestbook(Guestbook guestbook, IUnitOfWork unitOfWork)
        {
            _guestbookRepository = unitOfWork.GetRepository<Guestbook, Int32>();
            _guestbookRepository.Add(guestbook);
            unitOfWork.Save();
        }

        public void UpdateGuestbook(Guestbook guestbook, IUnitOfWork unitOfWork)
        {
            _guestbookRepository = unitOfWork.GetRepository<Guestbook, Int32>();
            _guestbookRepository.Update(guestbook);
            unitOfWork.Save();
        }

        public void DeleteGuestbook(Guestbook guestbook, IUnitOfWork unitOfWork)
        {
            _guestbookRepository = unitOfWork.GetRepository<Guestbook, Int32>();
            _guestbookRepository.Delete(guestbook);
            unitOfWork.Save();
        }

        public IEnumerable<Guestbook> GetAllGuestbookPosts(IUnitOfWork unitOfWork)
        {
            _guestbookRepository = unitOfWork.GetRepository<Guestbook, Int32>();
            var guestbookPosts = _guestbookRepository.All();

            if (guestbookPosts != null)
            {
                return guestbookPosts;
            }
            else
            {
                return null;
            }
        }

        public Guestbook GetGuestbookById(Int32 id, IUnitOfWork unitOfWork)
        {
            _guestbookRepository = unitOfWork.GetRepository<Guestbook, Int32>();
            return _guestbookRepository.GetById(id);
        }

        public List<Guestbook> GetAllActiveGuestbookPosts(IUnitOfWork unitOfWork)
        {
            _guestbookRepository = unitOfWork.GetRepository<Guestbook, Int32>();
            var guestbookPosts = _guestbookRepository.Find(x => x.Deleted == false).ToList();

            if (guestbookPosts != null)
            {
                return guestbookPosts;
            }
            else
            {
                return null;
            }
        }

        public List<Guestbook> GetGuestbookPostsByCharterId(int charterId, IUnitOfWork unitOfWork)
        {
            _guestbookRepository = unitOfWork.GetRepository<Guestbook, Int32>();
            var guestbookPosts = _guestbookRepository.Find(x => x.CharterId == charterId).ToList();

            if (guestbookPosts != null)
            {
                return guestbookPosts;
            }
            else
            {
                return null;
            }
        }

        public string GetGuestbookPostMessage(int pageSize, int pageNumber, int postCount)
        {
            var firstRow = 1;
            var lastRow = 1;
            var numberOfPostsToList = pageSize;

            var numberOfPages = (postCount / pageSize);
            var remainingPosts = (postCount % pageSize);

            if (remainingPosts < 10)
                numberOfPages = numberOfPages + 1;

            var maxRowCount = (numberOfPages * pageSize);

            firstRow = postCount - (pageSize * (pageNumber - 1));
            if (pageNumber == numberOfPages)
            {
                numberOfPostsToList = remainingPosts;
                lastRow = 1;
            }
            else
            {
                lastRow = firstRow - (pageSize) + 1;
            }

            return String.Format("Now listing {0} messages, ({1} to {2})", numberOfPostsToList, lastRow, firstRow);
        }
    }
}
