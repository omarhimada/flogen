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
        /// Customer IDs will start at this number 
        /// </summary>
        private const int StartCustomersAt = 10000;

        /// <summary>
        /// Maximum length of the SKU to use when generating cart orders
        /// </summary>
        private const int MaximumLengthOfSku = 2;

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

        /// <summary>
        /// The characters to choose from when generating random email prefixes
        /// </summary>
        private const string AvailableCharsForRandomEmailPrefixes = "abcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// The digits to choose from when generating random phone numbers
        /// </summary>
        private const string AvailableDigitsForPhoneNumbers = "0123456789";

        /// <summary>
        /// The strings to choose from when generating random email suffixes
        /// </summary>
        private static readonly string[] AvailableStringsForRandomEmailSuffixes = new[]
        {
            "@yahoo.ru",
            "@hotmail.co.uk",
            "@gmail.com",
            "@monarchy.gov"
        };
        #endregion

        /// <summary>
        /// Used for generating random quantities
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly Random _random = new Random();

        /// <summary>
        /// Generate random emails
        /// </summary>
        private static string RandomEmail()
        {
            string prefix = new string(
              Enumerable.Repeat(AvailableCharsForRandomEmailPrefixes, 16)
                .Select(s => s[_random.Next(s.Length)])
                .ToArray());

            string suffix =
              AvailableStringsForRandomEmailSuffixes[_random.Next(AvailableStringsForRandomEmailSuffixes.Length - 1)];

            return $"{prefix}{suffix}";
        }

        private static bool RandomBoolean() =>
          _random.NextDouble() >= 0.5;

        /// <summary>
        /// Generate a random phone number and return its area code along with it
        /// </summary>
        /// <returns>Tuple of strings</returns>
        private static (string, string) RandomAreaCodeAndPhoneNumber()
        {
            string phoneNumber = new string(
              Enumerable.Repeat(AvailableDigitsForPhoneNumbers, 10)
                .Select(s => s[_random.Next(s.Length)])
                .ToArray());

            string areaCode = phoneNumber.Substring(0, 3);

            return (areaCode, phoneNumber);
        }

        /// <summary>
        /// Pick a random DateTime between today and a specified earliest date
        /// </summary>
        private static DateTime RandomDateTime()
        {
            DateTime earliestDateTime = new DateTime(2018, 2, 21);
            int range = (DateTime.Today - earliestDateTime).Days;
            return earliestDateTime.AddDays(_random.Next(range));
        }

        /// <summary>
        /// Generate a random float that is 2/3 proportional to the float given
        /// </summary>
        private static float RandomFloatTwoThirdsProportional(float numberOfDays) =>
          (float)(_random.NextDouble() * numberOfDays * 0.66f);

        /// <summary>
        /// Generate a random float that is 1/4 proportional to the float given
        /// </summary>
        private static float RandomFloatOneQuarterProportional(float number) =>
          (float)(_random.NextDouble() * number * 0.25f);

        /// <summary>
        /// Generate a random float that is 2/1 proportional to the float given
        /// </summary>
        private static float RandomFloatTwiceProportional(float number) =>
          (float)(_random.NextDouble() * number * 2f);

        /// <summary>
        /// Generation complete - output either CSV or JSON
        /// </summary>
        private static void OutputData(ManyRandomOrders ordersToOutput, List<Customer> customersToOutput)
        {
            Console.WriteLine("Output CSV or JSON? [cC]|[jJ]");
            string jsonOrCsvResponse = Console.ReadLine();

            // Default to CSV output
            bool outputCsv = true;

            const char useJson = 'j';
            const char useCsv = 'c';
            switch (jsonOrCsvResponse?.ToLowerInvariant()[0])
            {
                case useJson:
                    outputCsv = false;
                    break;
                case useCsv:
                    break;
                default:
                    Console.WriteLine("Invalid response. Defaulting to CSV");
                    break;
            }

            string ordersFilePathToWriteTo = @$"RandomOrders-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
            string customersFilePathToWriteTo = @$"RandomCustomers-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";

            if (outputCsv)
            {
                // Write the random orders to a CSV file
                using StreamWriter ordersStreamWriter = new StreamWriter($"{ordersFilePathToWriteTo}.csv");
                using CsvWriter ordersCsvWriter = new CsvWriter(ordersStreamWriter, CultureInfo.InvariantCulture);
                {
                    var flattenedManyRandomOrders =
                      from order in ordersToOutput.Orders
                      from cartItem in order.CartItems
                      select new
                      {
                          CustomerId = order.CustomerId,
                          Sku = cartItem.Sku,
                          Quantity = cartItem.Quantity,
                          OrderDate = order.OrderDate
                      };

                    ordersCsvWriter.WriteRecords(flattenedManyRandomOrders);
                }

                if (customersToOutput != null)
                {
                    // Write the customers to a CSV file
                    using StreamWriter customersStreamWriter = new StreamWriter($"{customersFilePathToWriteTo}.csv");
                    using CsvWriter customersCsvWriter = new CsvWriter(customersStreamWriter, CultureInfo.InvariantCulture);
                    {
                        customersCsvWriter.WriteRecords(customersToOutput);
                    }
                }
            }
            else
            {
                // Serialize the random orders and write to a JSON file
                string ordersJson = JsonConvert.SerializeObject(ordersToOutput, Formatting.None); // None for file size
                File.WriteAllText(@$"{ordersFilePathToWriteTo}.json", ordersJson);

                if (customersToOutput != null)
                {
                    string customersJson = JsonConvert.SerializeObject(customersToOutput, Formatting.None); // None for file size
                    File.WriteAllText(@$"{customersFilePathToWriteTo}.json", customersJson);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="args">true to use retail.dat (from http://fimi.uantwerpen.be/data/)</param>
        static void Main(string[] args)
        {
            // Used for outputting generation time
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (args.Length == 1 && bool.TryParse(args[0], out bool useRetailDat))
            {
                #region Use retail.dat file to generate orders with not-so-random customer IDs, emails and order dates
                if (useRetailDat)
                {
                    ManyRandomOrders notSoRandomOrders = new ManyRandomOrders
                    {
                        Orders = new List<CartOrder>()
                    };

                    string line;
                    // Read the retail.dat file
                    StreamReader file = new StreamReader(@"retail.dat");
                    while ((line = file.ReadLine()) != null)
                    {
                        int[] skusInTransaction = line.Trim().Split(" ").Select(int.Parse).ToArray();
                        int[] distinctSkus = skusInTransaction.Distinct().ToArray();

                        List<CartItem> cartItems = new List<CartItem>();
                        foreach (int sku in distinctSkus)
                        {
                            cartItems.Add(new CartItem
                            {
                                Sku = sku.ToString(),
                                Quantity = skusInTransaction.Count(i => i == sku)
                            });
                        }

                        CartOrder cartOrder = new CartOrder
                        {
                            CustomerId = _random.Next(StartCustomersAt, StartCustomersAt + MaximumNumberOfCustomers),
                            Email = RandomEmail(),
                            OrderDate = RandomDateTime(),
                            CartItems = cartItems
                        };

                        notSoRandomOrders.Orders.Add(cartOrder);
                    }

                    // Don't include serialization in generation time metric
                    sw.Stop();

                    Console.WriteLine($"Generation time: {sw.ElapsedMilliseconds} milliseconds.");

                    OutputData(notSoRandomOrders, null);
                }
                #endregion
            }
            else
            {
                // Don't use retail.dat (random SKUs & quantities)

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
                  allSkuCombinationsWithVariableDuplicates
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
                        CustomerId = _random.Next(StartCustomersAt, StartCustomersAt + MaximumNumberOfCustomers),
                        OrderDate = RandomDateTime()
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

                // Group orders by customer
                List<Customer> manyRandomCustomers =
                  (from order in manyRandomOrders.Orders
                   group order by order.CustomerId
                   into grouped
                   select new Customer
                   {
                       CustomerId = grouped.Key,
                       AccountLength = (float)(DateTime.Today - grouped.Min(i => i.OrderDate)).TotalDays,
                       DaysSinceLastPurchase = (float)(DateTime.Today - grouped.Max(i => i.OrderDate)).TotalDays,
                       Email = RandomEmail()
                   }).ToList();

                foreach (Customer customer in manyRandomCustomers)
                {
                    (string areaCode, string phoneNumber) = RandomAreaCodeAndPhoneNumber();
                    customer.AreaCode = areaCode;
                    customer.PhoneNumber = phoneNumber;

                    // Assume churned if they haven't purchased in 6 months
                    customer.Churned = customer.DaysSinceLastPurchase > 182.5;

                    customer.Voice = RandomBoolean();

                    if (customer.Voice)
                    {
                        bool internationalPlan = RandomBoolean();
                        if (internationalPlan)
                        {
                            #region International calls
                            // Calls max 2/3 of account length
                            customer.TotalInternationalCalls = RandomFloatTwoThirdsProportional(customer.AccountLength);
                            // Minutes max 2/1 of number of calls
                            customer.TotalInternationalMinutes = RandomFloatTwiceProportional(customer.TotalInternationalCalls);
                            // Charges max 1/4 of number of minutes
                            customer.TotalInternationalCharges = RandomFloatOneQuarterProportional(customer.TotalInternationalMinutes);
                            #endregion
                        }

                        #region Daytime calls
                        // Calls max 2/3 of account length
                        customer.TotalDaytimeCalls = RandomFloatTwoThirdsProportional(customer.AccountLength);
                        // Minutes max 2/1 of number of calls
                        customer.TotalDaytimeMinutes = RandomFloatTwiceProportional(customer.TotalDaytimeCalls);
                        // Charges max 1/4 of number of minutes
                        customer.TotalDaytimeCharges = RandomFloatOneQuarterProportional(customer.TotalDaytimeMinutes);
                        #endregion

                        #region Evening calls
                        // Calls max 2/3 of account length
                        customer.TotalEveningCalls = RandomFloatTwoThirdsProportional(customer.AccountLength);
                        // Minutes max 2/1 of number of calls
                        customer.TotalEveningMinutes = RandomFloatTwiceProportional(customer.TotalEveningCalls);
                        // Charges max 1/4 of number of minutes
                        customer.TotalEveningCharges = RandomFloatOneQuarterProportional(customer.TotalEveningMinutes);
                        #endregion

                        #region Night calls
                        // Calls max 2/3 of account length
                        customer.TotalNightCalls = RandomFloatTwoThirdsProportional(customer.AccountLength);
                        // Minutes max 2/1 of number of calls
                        customer.TotalNightMinutes = RandomFloatTwiceProportional(customer.TotalNightCalls);
                        // Charges max 1/4 of number of minutes
                        customer.TotalNightCharges = RandomFloatOneQuarterProportional(customer.TotalNightMinutes);
                        #endregion
                    }

                    customer.NumberOfCustomerServiceCalls = RandomFloatTwoThirdsProportional(customer.AccountLength);
                    customer.NumberOfMessages = RandomFloatTwoThirdsProportional(customer.AccountLength);
                }

                // Don't include serialization in generation time metric
                sw.Stop();

                Console.WriteLine($"Generation time: {sw.ElapsedMilliseconds} milliseconds.");

                OutputData(manyRandomOrders, manyRandomCustomers);
            }
        }
    }
}
