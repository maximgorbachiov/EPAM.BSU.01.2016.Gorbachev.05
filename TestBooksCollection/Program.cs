using System;
using System.Collections.Generic;
using BooksCollectionLib;
using BooksServiceLib;

namespace TestBooksCollection
{
    class Program
    {
        class Comparer : IComparer<Book>
        {
            public int Compare(Book x, Book y)
            {
                if ((x != null) && (y != null))
                {
                    if (x.CountOfPages == y.CountOfPages)
                    {
                        return 0;
                    }
                    return (x.CountOfPages > y.CountOfPages) ? 1 : -1;
                }

                if ((x == null) && (y == null))
                {
                    return 0;
                }
                return (x != null) ? 1 : -1;
            }
        }

        static void Main(string[] args)
        {
            BooksService books = new BooksService(new BooksBinaryWriter());
            BooksService booksXML = new BooksService(new BooksXMLStream());
            BooksService booksSerializer = new BooksService(new BooksBinarySearizator());
            List<Book> findedBooks;

            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Philosopher's Stone", CountOfPages = 200 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Chamber of Secrets", CountOfPages = 300 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Prisoner of Azkaban", CountOfPages = 400 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Goblet of Fire", CountOfPages = 500 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Order of the Phoenix", CountOfPages = 800 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Half-Blood Prince", CountOfPages = 500 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Deathly Hallows", CountOfPages = 600 });


            booksXML.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Philosopher's Stone", CountOfPages = 200 });
            booksXML.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Chamber of Secrets", CountOfPages = 300 });
            booksXML.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Prisoner of Azkaban", CountOfPages = 400 });
            booksXML.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Goblet of Fire", CountOfPages = 500 });
            booksXML.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Order of the Phoenix", CountOfPages = 800 });
            booksXML.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Half-Blood Prince", CountOfPages = 500 });
            booksXML.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Deathly Hallows", CountOfPages = 600 });

            booksSerializer.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Philosopher's Stone", CountOfPages = 200 });
            booksSerializer.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Chamber of Secrets", CountOfPages = 300 });
            booksSerializer.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Prisoner of Azkaban", CountOfPages = 400 });
            booksSerializer.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Goblet of Fire", CountOfPages = 500 });
            booksSerializer.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Order of the Phoenix", CountOfPages = 800 });
            booksSerializer.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Half-Blood Prince", CountOfPages = 500 });
            booksSerializer.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Deathly Hallows", CountOfPages = 600 });

            booksXML.WriteToStream("temp.xml");
            booksXML.ReadFromStream("temp.xml");

            booksSerializer.WriteToStream("temp.txt");
            booksSerializer.ReadFromStream("temp.txt");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Test READ_FROM_STREAM");
            Console.WriteLine();
            Console.WriteLine();

            findedBooks = booksSerializer.FindByTag((book => book.CountOfPages > 100));

            for (int i = 0; i < findedBooks.Count; i++)
            {
                Console.WriteLine(findedBooks[i].ToString());
            }

            Console.ReadLine();

            Console.WriteLine("Test ADD");
            Console.WriteLine();
            Console.WriteLine();

            findedBooks = books.FindByTag((book => book.CountOfPages > 100));

            for (int i = 0; i < findedBooks.Count; i++)
            {
                Console.WriteLine(findedBooks[i].ToString());
            }

            findedBooks = books.FindByTag((book => book.CountOfPages > 400));

            for (int i = 0; i < findedBooks.Count; i++)
            {
                books.RemoveBook(findedBooks[i]);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Test DELETE");
            Console.WriteLine();
            Console.WriteLine();

            findedBooks = books.FindByTag((book => book.CountOfPages > 100));

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine(findedBooks[i].ToString());
            }

            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Order of the Phoenix", CountOfPages = 800 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Half-Blood Prince", CountOfPages = 500 });
            books.AddBook(new Book() { Author = "J. Rolling", Title = "Harry Potter and the Deathly Hallows", CountOfPages = 600 });

            findedBooks = books.FindByTag((book => book.CountOfPages > 500));

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Test FIND_BY_TAG");
            Console.WriteLine();
            Console.WriteLine();

            foreach (Book book in findedBooks)
            {
                Console.WriteLine(book.ToString());
            }

            books.SortBooksByTag(new Comparer());

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Test SORT_BY_TAG");
            Console.WriteLine();
            Console.WriteLine();

            findedBooks = books.FindByTag((book => book.CountOfPages > 100));

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine(findedBooks[i].ToString());
            }

            books.WriteToStream(@"D:\temp1.txt");

            books.ReadFromStream(@"D:\temp1.txt");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Test READ_FROM_STREAM");
            Console.WriteLine();
            Console.WriteLine();

            findedBooks = books.FindByTag((book => book.CountOfPages > 100));

            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine(findedBooks[i].ToString());
            }

            Console.ReadLine();
        }
    }
}
