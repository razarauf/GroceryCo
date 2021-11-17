﻿using System;
using System.Collections.Generic;
using System.IO;

namespace GroceryCo
{
    class Program
    {
        // Set the prices and discount of items from catalog
        // Product  Price   Discount
        // Apple    $0.75   $0.25
        // Banana   $1.00
        // Carrot   $1.25
        // Potato   $1.50   $0.15

        static void Main(string[] args)
        {

            string INPUT_FILE = "/Users/razarauf/Projects/GroceryCo/GroceryCo/INPUT_FILE.txt";

            //Console.WriteLine("Scan products: ");
            //INPUT_FILE = Console.ReadLine();

            string[] logFile = File.ReadAllLines(INPUT_FILE);
            //List<string> logList = new List<string>(logFile);

            Product product = new Product();

            // Group items
            List<Product> prods = product.GroupItems(logFile);

            // Assign Prices and Discount 
            prods = product.SetPricesAndDiscountFromCatalog();

            // Determine the discount and total
            float total = product.DetermineTotal();

            Console.WriteLine(String.Format("Total Due: {0:C}", total));
            Console.ReadKey();
        }

        
    }
}
