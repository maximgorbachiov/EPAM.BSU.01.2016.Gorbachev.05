using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using BooksServiceLib;

namespace BooksCollectionLib
{
    public class BooksXMLStream : IBookStream
    {
        public List<Book> ReadBooks(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            XDocument xdoc = XDocument.Load(filePath);

            List<Book> books = (from xmlElement in xdoc.Element("books").Elements("book")
                            select new Book()
                            {
                                Title = xmlElement.Element(nameof(Book.Title)).Value,
                                Author = xmlElement.Element(nameof(Book.Author)).Value,
                                CountOfPages = int.Parse(xmlElement.Element(nameof(Book.CountOfPages)).Value)
                            }).ToList();

            return books;
        }

        public void WriteBooks(List<Book> bookCollection, string filePath)
        {
            if (filePath == "" && filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            XDocument xdoc = new XDocument();
            XElement books = new XElement("books");

            for (int i = 0; i < bookCollection.Count; i++)
            {
                if (bookCollection[i] != null)
                {
                    XElement book = new XElement("book");

                    book.Add(new XElement(nameof(Book.Title), CheckStringForWrite(bookCollection[i].Title)));
                    book.Add(new XElement(nameof(Book.Author), CheckStringForWrite(bookCollection[i].Author)));
                    book.Add(new XElement(nameof(Book.CountOfPages), bookCollection[i].CountOfPages.ToString()));

                    books.Add(book);
                }
            }

            xdoc.Add(books);
            xdoc.Save(filePath);
        }

        private string CheckStringForWrite(string checkedString)
        {
            return checkedString ?? "";
        }
    }
}
