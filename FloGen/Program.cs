using FloGen.Functions;
using FloGen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace FloGen
{
    class Program
    {
        #region Configure
        /// <summary>
        /// Number of random orders to generate
        /// </summary>
        private const long OrdersToGenerate = 50000;

        /// <summary>
        /// Generate random SKUs using these characters
        /// </summary>
        private static readonly char[] CharactersToUse = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// Maximum number of unique customer IDs to spread across all generated orders
        /// </summary>
        private const int MaximumNumberOfCustomers = 8000;

        /// <summary>
        /// Maximum length of the SKU to use when generating cart orders
        /// </summary>
        private const int MaximumLengthOfSku = 4;

        /// <summary>
        /// Maximum quantity for each SKU in the generated cart orders
        /// </summary>
        private const int MaximumSkuQuantity = 6;

        /// <summary>
        /// Maximum quantity of cart items in the generated cart orders
        /// </summary>
        private const int MaximumCartItemQuantity = 4;

        /// <summary>
        /// Number of variable quantities for each SKU across all orders
        /// </summary>
        private const int MaximumSkuQuantityVariance = 6;
        #endregion

        /// <summary>
        /// Used for generating random quantities
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly Random _random = new Random();

        static void Main(string[] args)
        {
            // Used for outputting generation time
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // List of possible SKUs up to MaximumLength (e.g.: 0001, 0002, 0003, ...9999)
            List<string> allSkuCombinations =
              Enumerable.Range(1, MaximumLengthOfSku)
                .SelectMany(count =>
                    // Cartesian product
                    Enumerable.Repeat(CharactersToUse, count).CartesianProduct())
                .Select(combination =>
                    new string(combination.ToArray()))
                .ToList();

            // Duplicate each SKU up to MaximumSkuQuantityVariance times
            List<string> allSkuCombinationsWithVariableDuplicates =
                allSkuCombinations
                    .SelectMany(sku =>
                        Enumerable.Repeat(sku, _random.Next(1, MaximumSkuQuantityVariance)))
                    .ToList();

            // Random cart items
            CartItem[] manyRandomCartItems =
              allSkuCombinations
                .Select(sku =>
                  new CartItem
                  {
                      Sku = sku,
                      Quantity = _random.Next(1, MaximumSkuQuantity)
                  })
              .ToArray();

            // Random list of indices in order to pick random cart items from manyRandomCartItems
            int[] randomIndicesToChooseFrom =
                // Fisher-Yates shuffle 
                FisherYatesShuffle.RandomIndices(_random, manyRandomCartItems.Length);

            // Many random orders (to output)
            ManyRandomOrders manyRandomOrders = new ManyRandomOrders
            {
                Orders = new List<CartOrder>()
            };

            for (int orderIndex = 0; orderIndex < OrdersToGenerate; orderIndex++)
            {
                CartOrder cartOrder = new CartOrder
                {
                    // Randomize the customer IDs
                    CustomerId = _random.Next(1, MaximumNumberOfCustomers)
                };

                // Randomize the number of cart items purchased by this customer
                int numberOfItemsInThisOrder = _random.Next(1, MaximumCartItemQuantity);

                List<CartItem> cartItemsToAddToThisOrder = new List<CartItem>();
                for (int cartItemIndex = 0; cartItemIndex < numberOfItemsInThisOrder; cartItemIndex++)
                {
                    // The index of the 'random indices' array to select the cart item to use when constructing each order
                    int indexOfRandomIndexToChoose = _random.Next(0, randomIndicesToChooseFrom.Length - 1);

                    // Choose a random index from the 'random indices' array
                    int indexOfCartItemToUse = randomIndicesToChooseFrom[indexOfRandomIndexToChoose];

                    // Use this random index to get a random cart item
                    cartItemsToAddToThisOrder.Add(manyRandomCartItems[indexOfCartItemToUse]);
                }

                cartOrder.CartItems = cartItemsToAddToThisOrder;

                manyRandomOrders.Orders.Add(cartOrder);
            }

            #region Generation finished - write to file
            // Don't include serialization in generation time metric
            sw.Stop();

            Console.WriteLine($"Generation time: {sw.ElapsedMilliseconds} milliseconds.");

            Console.WriteLine("Output CSV or JSON? [cC]|[jJ]");
            string jsonOrCsvResponse = Console.ReadLine();

            // Default to CSV output
            bool outputCsv = true;
            
            const char useJson = 'j';
            switch (jsonOrCsvResponse?.ToLowerInvariant()[0])
            {
                case null:
                    Console.WriteLine("Invalid response. Defaulting to CSV");
                    break;
                case useJson:
                    outputCsv = false;
                    break;
            }

            string filePathToWriteTo = @$"RandomOrders-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";

            if (outputCsv)
            {
                // Write the random orders to a CSV file
                using StreamWriter streamWriter = new StreamWriter($"{filePathToWriteTo}.csv");
                using CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                var flattenedManyRandomOrders = 
                  from order in manyRandomOrders.Orders
                  from cartItem in order.CartItems
                  select new
                  {
                    CustomerId = order.CustomerId,
                    Sku = cartItem.Sku,
                    Quantity = cartItem.Quantity
                  };

                csvWriter.WriteRecords(flattenedManyRandomOrders);
            }
            else
            {
                // Serialize the random orders and write to a JSON file
                string json = JsonConvert.SerializeObject(manyRandomOrders, Formatting.Indented);
                File.WriteAllText(@$"RandomOrders-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.json", json);
            }
            #endregion
        }
    }
}
