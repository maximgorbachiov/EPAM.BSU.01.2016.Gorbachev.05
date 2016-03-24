using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksCollectionLib;

namespace TestBooksCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            BooksCollection books = new BooksCollection(new BooksBinaryWriter<UserBook>());

            books.AddBook(new UserBook("Harry Potter and the Philosopher's Stone", "J. Rolling", 200));
            books.AddBook(new UserBook("Harry Potter and the Chamber of Secrets", "J. Rolling", 300));
            books.AddBook(new UserBook("Harry Potter and the Prisoner of Azkaban", "J. Rolling", 400));
            books.AddBook(new UserBook("Harry Potter and the Goblet of Fire", "J. Rolling", 500));
            books.AddBook(new UserBook("Harry Potter and the Order of the Phoenix", "J. Rolling", 800));
            books.AddBook(new UserBook("Harry Potter and the Half-Blood Prince", "J. Rolling", 500));
            books.AddBook(new UserBook("Harry Potter and the Deathly Hallows", "J. Rolling", 600));

            Console.WriteLine("Test ADD");
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine(books[i].ToString());
            }

            books.RemoveBook(books[books.Count - 1]);
            books.RemoveBook(books[books.Count - 1]);
            books.RemoveBook(books[books.Count - 1]);

            Console.WriteLine("Test DELETE");
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine(books[i].ToString());
            }

            books.AddBook(new UserBook("Harry Potter and the Order of the Phoenix", "J. Rolling", 800));
            books.AddBook(new UserBook("Harry Potter and the Half-Blood Prince", "J. Rolling", 500));
            books.AddBook(new UserBook("Harry Potter and the Deathly Hallows", "J. Rolling", 600));

            IEnumerable<Book> repeatingElements = books.FindByTag(new UserBook("Harry Potter and the Half-Blood Prince", "J. Rolling", 500));

            Console.WriteLine("Test FIND_BY_TAG");
            Console.WriteLine();
            Console.WriteLine();

            foreach (Book book in repeatingElements)
            {
                Console.WriteLine(book.ToString());
            }

            books.SortBooksByTag(new Comparator());

            Console.WriteLine("Test SORT_BY_TAG");
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine(books[i].ToString());
            }

            books.WriteToStream(@"D:\temp.txt");

            books.ReadFromStream(@"D:\temp.txt");

            Console.WriteLine("Test READ_FROM_STREAM");
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine(books[i].ToString());
            }

            Console.ReadLine();
        }
    }
}
