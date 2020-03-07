# FloGen
FloGen generates randomized data (e.g.: orders for an eCommerce website) for testing ML applications. For example, if you want to generate millions of dummy records to test your machine learning implementations, you can use FloGen to generate a configurable amount of random data to consume.

SKUs, the quantity of cart items in an order, and the quantity of each SKU in those cart items are all randomized.

The parameters can be configured and the output is serialized to a local JSON file. 

#### Example input:
- **Maximum length of the SKU to use when generating cart orders**
  - MaximumLengthOfSku = 4

- **Maximum quantity for each SKU in the generated cart orders**
  - MaximumSkuQuantity = 8

- **Maximum quantity of cart items in the generated cart orders**
  - MaximumCartItemQuantity = 5

- **Number of random orders to generate**
  - OrdersToGenerate = 100000

#### Output:
*RandomOrders-2020-03-06-21-58-57.json* (25 MB with indentation, 10 MB with no indentation)

Generation time: ~59 milliseconds (0.059s)
```
{
  "Orders": [
    {
      "CustomerId": 1,
      "CartItems": [
        {
          "Sku": "8141",
          "Quantity": 3
        },
        {
          "Sku": "9890",
          "Quantity": 3
        },
        {
          "Sku": "2254",
          "Quantity": 7
        }
      ]
    },
    {
      "CustomerId": 2,
      "CartItems": [
        {
          "Sku": "6274",
          "Quantity": 4
        }
      ]
    },
    {
      "CustomerId": 3,
      "CartItems": [
        {
          "Sku": "3545",
          "Quantity": 5
        },
        {
          "Sku": "0951",
          "Quantity": 6
        },
        {
          "Sku": "7046",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 4,
      "CartItems": [
        {
          "Sku": "9035",
          "Quantity": 5
        },
        {
          "Sku": "1307",
          "Quantity": 4
        },
        {
          "Sku": "1737",
          "Quantity": 6
        },
        {
          "Sku": "2714",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 5,
      "CartItems": [
        {
          "Sku": "2155",
          "Quantity": 7
        },
        {
          "Sku": "7253",
          "Quantity": 2
        },
        {
          "Sku": "1679",
          "Quantity": 7
        },
        {
          "Sku": "5604",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 6,
      "CartItems": [
        {
          "Sku": "599",
          "Quantity": 6
        }
      ]
    },
    {
      "CustomerId": 7,
      "CartItems": [
        {
          "Sku": "2365",
          "Quantity": 4
        },
        {
          "Sku": "4803",
          "Quantity": 1
        }
      ]
    },
    {
      "...": "..."
    },
    {
      "CustomerId": 99997,
      "CartItems": [
        {
          "Sku": "156",
          "Quantity": 4
        },
        {
          "Sku": "0943",
          "Quantity": 3
        },
        {
          "Sku": "6230",
          "Quantity": 3
        }
      ]
    },
    {
      "CustomerId": 99998,
      "CartItems": [
        {
          "Sku": "6810",
          "Quantity": 1
        },
        {
          "Sku": "7576",
          "Quantity": 5
        },
        {
          "Sku": "8050",
          "Quantity": 3
        }
      ]
    },
    {
      "CustomerId": 99999,
      "CartItems": [
        {
          "Sku": "9461",
          "Quantity": 5
        },
        {
          "Sku": "6645",
          "Quantity": 1
        }
      ]
    },
    {
      "CustomerId": 100000,
      "CartItems": [
        {
          "Sku": "8290",
          "Quantity": 6
        },
        {
          "Sku": "1081",
          "Quantity": 7
        },
        {
          "Sku": "4371",
          "Quantity": 3
        },
        {
          "Sku": "0513",
          "Quantity": 3
        }
      ]
    }
  ]
}
```
