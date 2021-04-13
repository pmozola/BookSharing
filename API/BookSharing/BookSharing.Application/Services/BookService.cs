using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Application.ViewModels;
using BookSharing.Infrastructure;

namespace BookSharing.Application.Services
{
    public class BookService :IBookService
    {
        private readonly BookSharingDbContext _dbContext;

        public BookService(BookSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<BookViewModel> GetBook(string isbn)
        {
            return _dbContext.Books.Where(x => x.ISBN == isbn)
                .Select(x => new BookViewModel
                {
                    Author = x.Author,
                    ImageUrl = x.ImageUrl,
                    Title = x.Title,
                    Year = x.Year,
                    Description = x.Description
                }).ToList();
        }
    }
}
