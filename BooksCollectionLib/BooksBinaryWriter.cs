using System;
using System.Collections.Generic;
using System.IO;

namespace BooksServiceLib
{
    public class BooksBinaryWriter : IBookStream
    {
        public List<Book> ReadBooks(string filePath)
        {
            List<Book> books = new List<Book>();
            int countOfPages;

            if (!string.IsNullOrEmpty(filePath))
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open)))
                {
                    while (reader.PeekChar() > -1)
                    {
                        Book book = new Book();
                        book.Title = reader.ReadString();
                        book.Author = reader.ReadString();
                        int.TryParse(reader.ReadString(), out countOfPages);
                        book.CountOfPages = countOfPages;
                        books.Add(book);
                    }
                }

                return books;
            }
            throw new ArgumentNullException(nameof(filePath));
        }

        public void WriteBooks(List<Book> books, string filePath)
        {
            if (filePath == "" && filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (BinaryWriter writer = new BinaryWriter(new FileStream(filePath, FileMode.Create)))
            {
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i] != null)
                    {
                        writer.Write(CheckStringForWrite(books[i].Title));
                        writer.Write(CheckStringForWrite(books[i].Author));
                        writer.Write(books[i].CountOfPages.ToString());
                    }
                }
            }           
        }

        private string CheckStringForWrite(string checkedString)
        {
            return checkedString ?? "";
        }
    }
}
