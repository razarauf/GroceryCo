using System;
using System.Collections.Generic;

namespace GroceryCo
{
    public class Product
    {
        private string name;
        private float price;
        private int quantity;
        private float discount;
        private float quantityDiscount;

        private List<Product> products;

        public float Total { get; private set; }

        public Product()
        {
            products = new List<Product>();
        }

        public void GroupItems(string[] logFile)
        {
            for (int i = 0; i < logFile.Length; i++)
            {
                if (!CheckIfProductExists(logFile[i], products))
                {
                    products.Add(new Product { name = logFile[i] });
                }
            }

            for (int i = 0; i < products.Count; i++)
            {
                products[i].quantity = GetQuantity(products[i].name, logFile);
            }
        }

        private bool CheckIfProductExists(string currentProd, List<Product> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].name == currentProd)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetQuantity(string product, string[] logFile)
        {
            int quantity = 0;

            for (int i = 0; i < logFile.Length; i++)
            {
                if (product == logFile[i])
                {
                    quantity++;
                }
            }

            return quantity;
        }

        public void SetPricesAndDiscountFromCatalog()
        {
            foreach (var eachProd in products)
            {
                if (eachProd.name.ToLower() == "apple")
                {
                    eachProd.price = 0.75f;
                    eachProd.discount = 0.25f;
                }
                else if (eachProd.name.ToLower() == "banana")
                {
                    eachProd.price = 1.0f;
                }
                else if (eachProd.name.ToLower() == "carrot")
                {
                    eachProd.price = 4f;
                    eachProd.quantityDiscount = 2f;
                }
                else if (eachProd.name.ToLower() == "potato")
                {
                    eachProd.price = 1.50f;
                    eachProd.discount = 0.15f;
                }
            }
        }

        public void DetermineTotal()
        {
            Total = 0f;

            for (int i = 0; i < products.Count; i++)
            {
                // Group discount
                if (products[i].quantityDiscount > 0 && products[i].quantity >= 3 )
                {
                    Total += (float) Math.Floor(products[i].quantity / 3f) * products[i].quantityDiscount;
                    Total += products[i].quantity % 3f * products[i].price;
                }
                // Original and sale price
                else
                {
                    Total += (products[i].price - products[i].discount) * products[i].quantity;
                }
            }
        }
    }
}
