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

#### Output:
*RandomOrders-2020-03-06-23-45-53.json* (10 MB with indentation, 4 MB with no indentation)

Generation time: ~29 milliseconds (0.029s)
```
{
  "Orders": [
    {
      "CustomerId": 6374,
      "CartItems": [
        {
          "Sku": "6124",
          "Quantity": 3
        },
        {
          "Sku": "8010",
          "Quantity": 4
        },
        {
          "Sku": "1714",
          "Quantity": 3
        }
      ]
    },
    {
      "CustomerId": 3201,
      "CartItems": [
        {
          "Sku": "174",
          "Quantity": 4
        },
        {
          "Sku": "6226",
          "Quantity": 4
        },
        {
          "Sku": "5907",
          "Quantity": 4
        }
      ]
    },
    {
      "CustomerId": 6912,
      "CartItems": [
        {
          "Sku": "9289",
          "Quantity": 2
        },
        {
          "Sku": "8048",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 1452,
      "CartItems": [
        {
          "Sku": "1576",
          "Quantity": 4
        },
        {
          "Sku": "5930",
          "Quantity": 3
        },
        {
          "Sku": "7660",
          "Quantity": 1
        }
      ]
    },
    {
      "...": "..."
    },
    {
      "CustomerId": 5036,
      "CartItems": [
        {
          "Sku": "1314",
          "Quantity": 2
        },
        {
          "Sku": "0728",
          "Quantity": 4
        },
        {
          "Sku": "8227",
          "Quantity": 3
        }
      ]
    },
    {
      "CustomerId": 7162,
      "CartItems": [
        {
          "Sku": "883",
          "Quantity": 2
        }
      ]
    },
    {
      "CustomerId": 1403,
      "CartItems": [
        {
          "Sku": "9103",
          "Quantity": 2
        },
        {
          "Sku": "4015",
          "Quantity": 1
        },
        {
          "Sku": "6077",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 7364,
      "CartItems": [
        {
          "Sku": "622",
          "Quantity": 1
        }
      ]
    },
    {
      "CustomerId": 1019,
      "CartItems": [
        {
          "Sku": "1895",
          "Quantity": 4
        }
      ]
    }
  ]
}
```
