using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        private string _userId;
        public BookmarkService(ReadLaterDataContext readLaterDataContext, IHttpContextAccessor httpContextAccessor)
        {
            _ReadLaterDataContext = readLaterDataContext;

            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            bookmark.CreateDate = DateTime.Now;

            if (string.IsNullOrWhiteSpace(bookmark.Category.Name))
            {
                bookmark.Category = null;
            }

            bookmark.ApplicationUserId = _userId;
            bookmark.Category.ApplicationUserId = _userId;

            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public Bookmark GetBookmark(int Id)
        {
            return _ReadLaterDataContext.Bookmark.Where(x => x.ID == Id && x.ApplicationUserId == _userId).Include(x => x.Category).FirstOrDefault();
        }

        public Bookmark GetBookmark(string Description)
        {
            return _ReadLaterDataContext.Bookmark.Where(x => x.ShortDescription.Contains(Description) && x.ApplicationUserId == _userId).FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks()
        {
            return _ReadLaterDataContext.Bookmark.Where(x => x.ApplicationUserId == _userId).Include(x => x.Category).ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
