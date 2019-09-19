using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineBookStore
{
    class ItemToPurchase
    {
        public Product m_product;
        public int m_quantity;


        public ItemToPurchase()
        {
        }

        public ItemToPurchase(Product product, int quantity)
        {
            m_product = product;
            m_quantity = quantity;

        }
        public Product getProduct()
        {
            return m_product;
        }
        public void setProduct(Product product)
        {
            m_product = product;
        }

        public int getQuantity()
        {
            return m_quantity;
        }
        public void setQuantity(int quantity)
        {
            m_quantity = quantity;
        }
    }
}
