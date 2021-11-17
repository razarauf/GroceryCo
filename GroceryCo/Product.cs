using System;
using System.Collections.Generic;

namespace GroceryCo
{
    public class Product
    {
        private string name;
        private float price;
        private int quantity;
        // How many cents off:
        private float discount;
        private float quantityDiscount;
        //public int additionalDiscount;

        public List<Product> Products;

        public Product()
        {
            Products = new List<Product>();
        }

        public void GroupItems(string[] logFile)
        {
            for (int i = 0; i < logFile.Length; i++)
            {
                if (!CheckIfProductExists(logFile[i], Products))
                {
                    Products.Add(new Product { name = logFile[i] });
                }
            }

            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].quantity = GetQuantity(Products[i].name, logFile);
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
            foreach (var eachProd in Products)
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

        public float DetermineTotal()
        {
            float total = 0f;

            for (int i = 0; i < Products.Count; i++)
            {
                // Group discount
                if (Products[i].quantityDiscount > 0 && Products[i].quantity >= 3 )
                {
                    total += (float) Math.Floor(Products[i].quantity / 3f) * Products[i].quantityDiscount;
                    total += Products[i].quantity % 3f * Products[i].price;
                }
                // Original and sale price
                else
                {
                    total += (Products[i].price - Products[i].discount) * Products[i].quantity;
                }
            }

            return total;
        }

    }
}
