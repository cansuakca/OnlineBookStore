using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Threading;

namespace OnlineBookStore
{
    public class Book : Product
    {
        private string m_author;
        private string m_publisher;
        private int m_page;
        private int m_ISBNnumber;
        public Book()
        {
        }
        public Book(int ID, string name, double price, string author, string publisher, int page, int ISBNnumber)
            : base(ID, name, price)
        {
            m_author = author;
            m_publisher = publisher;
            m_page = page;
            m_ISBNnumber = ISBNnumber;
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public override string[] printProperties(int ProductPlace)
        {
            Book myBook = new Book();
            StreamReader readcustomerfile;
            readcustomerfile = File.OpenText(@"Products.txt");
            string[] properties = new string[6];
            string info = "";
            string line;
            int control = 0;
            int first, last;
            while ((line = readcustomerfile.ReadLine()) != null)
            {
                control = line.IndexOf("ID : " + (100 + ProductPlace));
                if (control != -1)
                {
                    //Set ID To Book With Read Text File
                    first = line.IndexOf("ID : ") + "ID : ".Length;
                    last = line.IndexOf(",Name");
                    info = line.Substring(first, last - first);
                    myBook.setID(Convert.ToInt32(info));
                    properties[0] = myBook.getID().ToString();

                    //Set Name To Book With Read Text File
                    first = line.IndexOf("Name : ") + "Name : ".Length;
                    last = line.IndexOf(",Price");
                    info = line.Substring(first, last - first);
                    myBook.setName(info);
                    properties[1] = myBook.getName();


                    //Set Price To Book With Read Text File
                    first = line.IndexOf("Price : ") + "Price : ".Length;
                    last = line.IndexOf(",ISBN");
                    info = line.Substring(first, last - first);
                    myBook.setPrice(Convert.ToInt32(info));
                    properties[2] = myBook.getPrice().ToString();


                    //Set ISBN To Book With Read Text File
                    first = line.IndexOf("ISBN : ") + "ISBN : ".Length;
                    last = line.IndexOf(",Author");
                    info = line.Substring(first, last - first);
                    myBook.setISBN(Convert.ToInt32(info));
                    properties[3] = myBook.getISBN().ToString(); 


                    //Set Author To Book With Read Text File
                    first = line.IndexOf("Author : ") + "Author : ".Length;
                    last = line.IndexOf(",Publisher");
                    info = line.Substring(first, last - first);
                    myBook.setAuthor(info);
                    properties[4] = myBook.getAuthor();


                    //Set Publisher To Book With Read Text File
                    first = line.IndexOf("Publisher : ") + "Publisher : ".Length;
                    last = line.Length;
                    info = line.Substring(first, last - first);
                    myBook.setPublisher(info);
                    properties[5] = myBook.getPublisher();
                }
            }
            return properties;

        }
        public string getAuthor()
        {
            return m_author;
        }
        public void setAuthor(string author)
        {
            m_author = author;
        }
        public string getPublisher()
        {
            return m_publisher;
        }
        public void setPublisher(string publisher)
        {
            m_publisher = publisher;
        }
        public int getPage()
        {
            return m_page;
        }
        public void setPage(int page)
        {
            m_page = page;
        }
        public int getISBN()
        {
            return m_ISBNnumber;
        }
        public void setISBN(int ISBN)
        {
            m_ISBNnumber = ISBN;
        }

    }
}
