using FloGen.Functions;
using FloGen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    private const int MaximumLengthOfSku = 5;

    /// <summary>
    /// Maximum quantity for each SKU in the generated cart orders
    /// </summary>
    private const int MaximumSkuQuantity = 10;

    /// <summary>
    /// Maximum quantity of cart items in the generated cart orders
    /// </summary>
    private const int MaximumCartItemQuantity = 4;

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

      // Check if there is a sufficient number of random cart items to choose from
      if (manyRandomCartItems.Length < OrdersToGenerate)
      {
        Console.WriteLine("There are not enough randomized cart items to avoid duplicates. Consider increasing MaximumLengthOfSku.");
      }

      // Many random orders (to output)
      ManyRandomOrders manyRandomOrders = new ManyRandomOrders
      {
        Orders = new List<CartOrder>()
      };

      // The index of the 'random indices' array to select the cart item to use when constructing each order
      int indexOfRandomIndexToChoose = 0;

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
          // Choose a random index from the 'random indices' array
          int indexOfCartItemToUse = randomIndicesToChooseFrom[indexOfRandomIndexToChoose];

          /* The random index of the cart item to use increments with orderIndex
           * in order for each order to have randomized cart items */
          indexOfRandomIndexToChoose++;

          if (indexOfRandomIndexToChoose > randomIndicesToChooseFrom.Length - 1)
          {
            // If we ran out of random indices to choose from restart the indexOfRandomIndexToChoose
            indexOfRandomIndexToChoose = 0;
          }

          // Use this random index to get a random cart item
          cartItemsToAddToThisOrder.Add(manyRandomCartItems[indexOfCartItemToUse]);
        }

        cartOrder.CartItems = cartItemsToAddToThisOrder;

        manyRandomOrders.Orders.Add(cartOrder);
      }

      // Serialize the random orders and write to file
      string json = JsonConvert.SerializeObject(manyRandomOrders, Formatting.Indented);
      File.WriteAllText(@$"RandomOrders-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.json", json);
    }
  }
}
