using System;
using System.Collections.Generic;
using NLog;

namespace BooksServiceLib
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CountOfPages { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() == typeof (Book))
            {
                Book book = (Book)obj;
                return (Title == book.Title) && (Author == book.Author) && (CountOfPages == book.CountOfPages);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Title.Length + Author.Length + CountOfPages;
        }

        public override string ToString()
        {
            return Title + " " + Author + " " + CountOfPages;
        }
    }

    public interface IBookStream
    {
        List<Book> ReadBooks(string filePath);
        void WriteBooks(List<Book> books, string filePath);
    }

    public class BooksService
    {
        private List<Book> books;
        private IBookStream bookStream;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public int Count => books.Count;

        public BooksService(IBookStream bookStream)
        {
            if (bookStream != null)
            {
                books = new List<Book>();
                this.bookStream = bookStream;
                logger.Info("Constructor 1 was call correctly");
            }
            else
            {
                logger.Error("Constructor 1 get bookstream object == null");
                throw new ArgumentNullException(nameof(bookStream));
            }
        }

        public BooksService(IBookStream bookStream, IEnumerable<Book> source)
        {
            if (bookStream == null)
            {
                logger.Error("Constructor 2 get bookstream object == null");
                throw new ArgumentNullException(nameof(bookStream));
            }

            if (source == null)
            {
                logger.Error("Constructor 2 get source books collection object == null");
                throw new ArgumentNullException(nameof(source));
            }

            logger.Info("Constructor 2 was call correctly");
            books = new List<Book>(source);
            this.bookStream = bookStream;
        }

        public void AddBook(Book book)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (Equals(book, books[i]))
                {
                    logger.Error("On " + i + " iteration in AddBook method were find an equal book");
                    throw new ArgumentException("This book is exist");
                }
            }

            logger.Info("Add method finished correctly");
            books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            bool isBookDeleted = false;

            for (int i = 0; i < books.Count; i++)
            {
                if (!Equals(book, books[i])) continue;

                books.Remove(book);
                logger.Info("On " + i + " iteration in RemoveBook method were find and delete requested book");
                isBookDeleted = true;

                break;
            }

            if (!isBookDeleted)
            {
                logger.Error("RemoveBook method can't find the book to delete");
                throw new ArgumentException("This book isn't exist");
            }
            logger.Info("DeleteBook method finished correctly");
        }

        public List<Book> FindByTag(Predicate<Book> checker)
        {
            if (checker == null)
            {
                logger.Error("In FindByTag method checker delegate object == null");
                throw new ArgumentNullException(nameof(checker));
            }

            try
            {
                List<Book> findedBooks = books.FindAll(checker);
                logger.Info("FindByTag method finished correctly");
                return findedBooks;
            }
            catch (Exception)
            {
                logger.Error("FindByTag method can't find requested values. May be problems with parametr checker delegate");
                throw;
            }
        }

        public void SortBooksByTag(IComparer<Book> comparator)
        {
            if (comparator == null)
            {
                logger.Error("In SortBooksByTag parametr comparator has null value");
                throw new ArgumentNullException(nameof(comparator));
            }

            books.Sort(comparator);
            logger.Info("SortBooksByTag method finished correctly");
        }

        public void ReadFromStream(string filePath)
        {
            List<Book> temp = bookStream.ReadBooks(filePath);

            if (temp == null)
            {
                logger.Error("Books didn't read from file " + filePath);
                throw new Exception("There is no data to load at " + filePath);
            }

            books = temp;
            logger.Info("ReadFromStream method finished correctly");
        }

        public void WriteToStream(string filePath)
        {
            bookStream.WriteBooks(books, filePath);
        }
    }
}
