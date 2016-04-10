using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BooksServiceLib;

namespace BooksCollectionLib
{
    public class BooksBinarySearizator: IBookStream
    {
        private BinaryFormatter formatter;

        public BooksBinarySearizator()
        {
            formatter = new BinaryFormatter();
        }

        public List<Book> ReadBooks(string filePath)
        {
            List<Book> books = new List<Book>();

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                books = (List<Book>)formatter.Deserialize(fs);
            }

            return books;
        }

        public void WriteBooks(List<Book> books, string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, books);
            }
        }
    }
}
