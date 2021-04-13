using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookSharing.Application.ViewModels;

namespace BookSharing.Application.Interface
{
    public interface IBookService
    {
        public IList<BookViewModel> GetBook(string isbn);
    }
}
