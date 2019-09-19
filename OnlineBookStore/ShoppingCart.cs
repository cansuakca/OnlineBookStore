using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Configuration; 


namespace OnlineBookStore
{
    class ShoppingCart
    {
        private int m_customerId;
        private double m_paymentAmount;
        private string m_paymentType;
        private ArrayList m_itemsToPurchase = new ArrayList();

        private static ShoppingCart instance;
        public static ShoppingCart getInstance()
        {
            if (instance == null)
            {
                instance = new ShoppingCart();
            }
            return instance;
        }
        public ArrayList getItemsToPurchase()
        {
            return m_itemsToPurchase;
        }
        public void setItemsToPurchase(ArrayList ItemsToPurchase)
        {
            m_itemsToPurchase = ItemsToPurchase;
        }
        public int getCustomerId()
        {
            return m_customerId;
        }
        public void setCustomerId(int customerId)
        {
            m_customerId = customerId;
        }

        public double getPaymentAmount()
        {
            return m_paymentAmount;
        }
        public void setPaymentAmount(double paymentAmount)
        {
            m_paymentAmount = paymentAmount;
        }

        public string getPaymentType()
        {
            return m_paymentType;
        }
        public void setPaymentType(string paymentType)
        {
            m_paymentType = paymentType;

        }
        public void addProduct(ItemToPurchase product)
        {
            m_itemsToPurchase.Add(product);
        }

        public void removeProduct(ItemToPurchase product)
        {
            m_itemsToPurchase.Remove(product);
        }

        public void placeOrder()
        {


        }

        public void cancelOrder()
        {
            
        }

        public void sendInvoicebySMS()
        {

        }
        public void sendInvoidcebyEmail()
        {

        }
        public string[] printProducts(ItemToPurchase myitem)
        {
            //string[] row = { (this.m_customerId).ToString(), this.m_paymentAmount.ToString(), this.m_paymentType, myitem.getProduct(), myitem.getQuantity().ToString() };
            //return row;
            string[] row = { myitem.getProduct().getID().ToString() , myitem.getProduct().getName() , myitem.getProduct().getPrice().ToString() + "TL", myitem.getQuantity().ToString() };
            return row;
        }
    }
}
