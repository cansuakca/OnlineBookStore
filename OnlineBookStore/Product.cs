using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore
{
    public abstract class Product: IDisposable
    {

        protected int m_ID;
        protected string m_name;
        protected double m_price;
        public Product()
        {
        }

        public Product(int ID, string name, double price)
        {
            m_ID = ID;
            m_name = name;
            m_price = price;

        }
        public virtual void Dispose()
        {

        }
        public int getID()
        {
            return m_ID;
        }
        public void setID(int ID)
        {
            m_ID = ID;
        }
        public string getName()
        {
            return m_name;
        }
        public void setName(string name)
        {
            m_name = name;
        }
        public double getPrice()
        {
            return m_price;
        }
        public void setPrice(double price)
        {
            m_price = price;
        }
        public virtual string[] printProperties(int place)
        {
            return null;
        }

    }
}
