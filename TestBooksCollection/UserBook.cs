using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksCollectionLib;

namespace TestBooksCollection
{
    class UserBook : Book, ICheckByTag
    {
        public UserBook()
        {
            
        }

        public UserBook(string title, string author, int countOfPages)
        {
            Title = title;
            Author = author;
            CountOfPages = countOfPages;
        }

        public override bool Equals(Book book)
        {
            return (Title == book.Title) && (Author == book.Author) && (CountOfPages == book.CountOfPages);
        }

        public bool CheckByTag(Book book)
        {
            return this.CountOfPages == book.CountOfPages;
        }

        public override string ToString()
        {
            return Title + " " + Author + " " + CountOfPages;
        } 
    }

    class Comparator: IComparator
    {
        public bool Compare(Book book1, Book book2)
        {
            return book1.CountOfPages > book2.CountOfPages;
        }
    }
}
