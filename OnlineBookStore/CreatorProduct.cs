using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineBookStore
{
    public enum ProductType
    {
        Book,
        Magazine,
        MusicCD
    }
    class CreatorProduct
    {
        public Product FactoryMethod(ProductType productType)
        {
            Product product = null;
            switch (productType)
            {
                case ProductType.Book:
                    product = new Book();
                    break;
                case ProductType.Magazine:
                    product = new Magazine();
                    break;
                case ProductType.MusicCD:
                    product = new MusicCD();
                    break;
                default:
                    break;
            }

            return product;
        }
    }
}
