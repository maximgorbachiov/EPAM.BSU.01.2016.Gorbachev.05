using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace BooksCollectionLib
{
    public abstract class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CountOfPages { get; set; }

        public abstract bool Equals(Book book);
    }

    public interface ICheckByTag
    {
        bool CheckByTag(Book book);
    }

    public interface IComparator
    {
        bool Compare(Book book1, Book book2);
    }

    public interface IBookStream
    {
        Book[] ReadBooks(string filePath);
        void WriteBooks(Book[] books, string filePath);
    }

    public class BooksCollection
    {
        private Book[] books;
        private int capacityGrower = 10;
        private IBookStream bookStream;

        public int Count { get; private set; }

        public Book this[int index]
        {
            get
            {
                if ((index >= 0) && (index < Count))
                {
                    return books[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if ((index >= 0) && (index < Count))
                {
                    books[index] = value;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public BooksCollection(IBookStream bookStream)
        {
            if (bookStream != null)
            {
                books = new Book[capacityGrower];
                this.bookStream = bookStream;
            }
            else
            {
                throw new ArgumentNullException(nameof(bookStream));
            }
        }

        public BooksCollection(IBookStream bookStream, int startCount)
        {
            if (bookStream != null)
            {
                books = (startCount > 0) ? new Book[startCount]: new Book[capacityGrower];
                this.bookStream = bookStream;
            }
            else
            {
                throw new ArgumentNullException(nameof(bookStream));
            }
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("Book cant't be null");
            }


            for (int i = 0; i < Count; i++)
            {
                if (book.Equals(books[i]))
                {
                    throw new ArgumentException("This book is exist");
                }
            }

            if (Count == books.Length)
            {
                Book[] temp = new Book[Count + capacityGrower];
                books.CopyTo(temp, 0);
                books = temp;
            }
            books[Count] = book;
            Count++;
        }

        public void RemoveBook(Book book)
        {
            bool isBookDeleted = false;

            if (book == null)
            {
                throw new ArgumentNullException("Book can't be null");
            }

            for (int i = 0; i < Count; i++)
            {
                if (!book.Equals(books[i])) continue;

                for (int j = i; j < Count - 1; j++)
                {
                    books[j] = books[j + 1];
                }
                isBookDeleted = true;
                Count--;

                break;
            }

            if (!isBookDeleted)
            {
                throw new ArgumentException("This book isn't exist");
            }
        }

        public IEnumerable<Book> FindByTag(ICheckByTag checker)
        {
            if (checker != null)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (checker.CheckByTag(books[i]))
                    {
                        yield return books[i];
                    }
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(checker));
            }
        }

        public void SortBooksByTag(IComparator comparator)
        {
            if (comparator != null)
            {
                for (int i = 0; i < Count - 1; i++)
                {
                    for (int j = i; j < Count; j++)
                    {
                        if (comparator.Compare(books[i], books[j]))
                        {
                            Swap(ref books[i], ref books[j]);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(comparator));
            }
        }

        public void ReadFromStream(string filePath)
        {
            Book[] temp = bookStream.ReadBooks(filePath);

            if (temp == null)
            {
                throw new Exception("There is no data to load at " + filePath);
            }

            books = temp;
            Count = books.Length;
        }

        public void WriteToStream(string filePath)
        {
            bookStream.WriteBooks(books, filePath);
        }

        private void Swap(ref Book book1, ref Book book2)
        {
            Book temp = book1;
            book1 = book2;
            book2 = temp;
        }
    }
}
