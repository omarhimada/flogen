# FloGen
FloGen generates randomized data (e.g.: orders for an eCommerce website) for testing ML applications. For example, if you want to generate millions of dummy records to test your machine learning implementations, you can use FloGen to generate a configurable amount of random data to consume.

Number of unique customer IDs, SKUs, the quantity of cart items in an order, and the quantity of each SKU in those cart items are all randomized.

The parameters can be configured and the output is serialized to a local JSON file. 

#### Example input:
- **Number of random orders to generate**
  - OrdersToGenerate = 50000
  
- **Generate random SKUs using these characters**
  - CharactersToUse = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }
  
- **Maximum number of unique customer IDs to spread across all generated orders**
  - MaximumNumberOfCustomers = 8000
  
- **Maximum length of the SKU to use when generating cart orders**
  - MaximumLengthOfSku = 4

- **Maximum quantity for each SKU in the generated cart orders**
  - MaximumSkuQuantity = 6

- **Maximum quantity of cart items in the generated cart orders**
  - MaximumCartItemQuantity = 4
  
- **Number of variable quantities for each SKU across all orders**
  - MaximumSkuQuantityVariance = 6
    - *Each SKU will have, at most, this number of unique quantities across every cart item it's present in*

- **The characters to choose from when generating random email prefixes**
  - AvailableCharsForRandomEmailPrefixes = "abcdefghijklmnopqrstuvwxyz0123456789";

- **The strings to choose from when generating random email suffixes**
  - AvailableStringsForRandomEmailSuffixes = ["@yahoo.ru", "@hotmail.co.uk", "@gmail.com", "@monarchy.gov"]

#### Output:
*RandomOrders-2020-03-10-20-38-50.json* (14 MB with indentation, 7 MB with no indentation)

Generation time: ~72 milliseconds (0.072s)
````JSON
{
  "Orders": [
    {
      "customerId": 2268,
      "email": "o9v1vmk57d9qmh36@hotmail.co.uk",
      "cart": [
        {
          "sku": "72",
          "quantity": 1
        },
        {
          "sku": "70",
          "quantity": 4
        }
      ],
      "orderDate": "2011-06-27T00:00:00"
    },
    {
      "customerId": 956,
      "email": "gkd76xyus1kd5ap5@gmail.com",
      "cart": [
        {
          "sku": "96",
          "quantity": 3
        },
        {
          "sku": "6",
          "quantity": 4
        }
      ],
      "orderDate": "2019-07-27T00:00:00"
    },
    {
      "customerId": 7777,
      "email": "ils0jbwa2adk3kyx@hotmail.co.uk",
      "cart": [
        {
          "sku": "66",
          "quantity": 2
        }
      ],
      "orderDate": "2009-10-02T00:00:00"
    },
    ...
```
