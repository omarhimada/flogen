using FloGen.Functions;
using FloGen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FloGen
{
  class Program
  {
    #region Configurable constants
    /// <summary>
    /// Maximum length of the SKU to use when generating cart orders
    /// </summary>
    private const int MaximumLengthOfSku = 4;

    /// <summary>
    /// Maximum quantity for each SKU in the generated cart orders
    /// </summary>
    private const int MaximumSkuQuantity = 8;

    /// <summary>
    /// Maximum quantity of cart items in the generated cart orders
    /// </summary>
    private const int MaximumCartItemQuantity = 5;

    /// <summary>
    /// Number of random orders to generate
    /// </summary>
    private const long OrdersToGenerate = 100000;
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

      // Generate random SKUs using these characters
      char[] charactersToUse = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

      // List of possible SKUs up to MaximumLength (e.g.: 0001, 0002, 0003, ...9999)
      IEnumerable<string> allCombinations =
        Enumerable.Range(1, MaximumLengthOfSku)
          .SelectMany(count => Enumerable.Repeat(charactersToUse, count).CartesianProduct())
          .Select(combination => new string(combination.ToArray()));

      // Random cart items
      CartItem[] manyRandomCartItems = 
        allCombinations
          .Select(sku => 
            new CartItem
            {
              Sku = sku,
              Quantity = _random.Next(1, MaximumSkuQuantity)
            })
        .ToArray();

      // Random list of indices in order to pick random cart items from manyRandomCartItems
      int[] randomIndicesToChooseFrom = 
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
          // Each generated cart order will have a different customer ID
          CustomerId = orderIndex + 1
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

      // Don't include JSON serialization in generation time metric
      sw.Stop();

      // Serialize the random orders and write to file
      string json = JsonConvert.SerializeObject(manyRandomOrders, Formatting.Indented);
      File.WriteAllText(@$"RandomOrders-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.json", json);
      
      Console.WriteLine($"Generation time: {sw.ElapsedMilliseconds} milliseconds.");
      Console.ReadLine();
    }
  }
}
