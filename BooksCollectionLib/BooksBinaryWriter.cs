using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksCollectionLib
{
    public class BooksBinaryWriter<T> : IBookStream where T : Book, new()
    {
        public Book[] ReadBooks(string filePath)
        {
            List<Book> books = new List<Book>();
            int countOfPages;

            if ((filePath != "") && (filePath != null))
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        Book book = new T();
                        book.Title = reader.ReadString();
                        book.Author = reader.ReadString();
                        int.TryParse(reader.ReadString(), out countOfPages);
                        book.CountOfPages = countOfPages;
                        books.Add(book);
                    }
                }

                return books.ToArray();
            }
            throw new ArgumentNullException(nameof(filePath));
        }

        public void WriteBooks(Book[] books, string filePath)
        {
            if ((filePath == "") && (filePath == null))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.OpenOrCreate)))
            {
                for (int i = 0; i < books.Length; i++)
                {
                    if (books[i] != null)
                    {
                        writer.Write(books[i].Title);
                        writer.Write(books[i].Author);
                        writer.Write(books[i].CountOfPages.ToString());
                    }
                }
            }           
        }
    }
}
