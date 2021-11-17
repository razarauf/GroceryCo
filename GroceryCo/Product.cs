using System;
using System.Collections.Generic;

namespace GroceryCo
{
    public class Product
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        // How many cents off:
        public float Discount { get; set; }
        public float Total { get; set; }
        public float QuantityDiscount { get; set; }
        //public int AdditionalDiscount { get; set; 

        public List<Product> Products;

        public Product()
        {
            Products = new List<Product>();
        }

        public List<Product> GroupItems(string[] logFile)
        {
            for (int i = 0; i < logFile.Length; i++)
            {
                if (!CheckIfProductExists(logFile[i], Products))
                {
                    Products.Add(new Product { Name = logFile[i] });
                }
            }

            for (int i = 0; i < Products.Count; i++)
            {
                Products[i].Quantity = GetQuantity(Products[i].Name, logFile);
            }

            return Products;
        }

        public bool CheckIfProductExists(string currentProd, List<Product> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == currentProd)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetQuantity(string product, string[] logFile)
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

        public List<Product> SetPricesAndDiscountFromCatalog()
        {
            foreach (var eachProd in Products)
            {
                if (eachProd.Name.ToLower() == "apple")
                {
                    eachProd.Price = 0.75f;
                    eachProd.Discount = 0.25f;
                }
                else if (eachProd.Name.ToLower() == "banana")
                {
                    eachProd.Price = 1.0f;
                }
                else if (eachProd.Name.ToLower() == "carrot")
                {
                    eachProd.Price = 4f;
                    eachProd.QuantityDiscount = 2f;
                }
                else if (eachProd.Name.ToLower() == "potato")
                {
                    eachProd.Price = 1.50f;
                    eachProd.Discount = 0.15f;
                }
            }
            return Products;
        }

        public float DetermineTotal()
        {
            float total = 0f;

            for (int i = 0; i < Products.Count; i++)
            {
                // Group discount
                if (Products[i].Quantity >= 3 && Products[i].Quantity % 3 == 0 && Products[i].Name == "carrot")
                {
                    float tmp = Products[i].Quantity / 3 * Products[i].QuantityDiscount;
                    total += (Products[i].Price) * Products[i].Quantity/3 * Products[i].QuantityDiscount;
                }
                else
                {
                    total += (Products[i].Price - Products[i].Discount) * Products[i].Quantity;
                }
            }

            return total;
        }

    }
}
