# FloGen
FloGen generates randomized data (e.g.: orders for an eCommerce website) for testing ML applications. For example, if you want to generate a million dummy records to test your machine learning implementations, you can use FloGen to generate a configurable amount of random data to consume.

SKUs, the quantity of cart items in an order, and the quantity of each SKU in those cart items are all randomized.

The parameters can be configured and the output is serialized to a local JSON file. 

#### Example input:
- **Maximum length of the SKU to use when generating cart orders**
  - MaximumLengthOfSku = 5

- **Maximum quantity for each SKU in the generated cart orders**
  - MaximumSkuQuantity = 10

- **Maximum quantity of cart items in the generated cart orders**
  - MaximumCartItemQuantity = 4

- **Number of random orders to generate**
  - OrdersToGenerate = 100000

#### Output:
*RandomOrders-2020-03-06-11-19-07.json* (22 MB with indentation, 9 MB with no indentation)
Generation time: ~550 milliseconds (0.55s)
```
{
  "Orders": [
    {
      "CustomerId": 1,
      "CartItems": [
        {
          "Sku": "96",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 2,
      "CartItems": [
        {
          "Sku": "91849",
          "Quantity": 9
        },
        {
          "Sku": "69728",
          "Quantity": 9
        }
      ]
    },
    {
      "...": "..."
    },
    {
      "CustomerId": 99998,
      "CartItems": [
        {
          "Sku": "62147",
          "Quantity": 2
        },
        {
          "Sku": "79487",
          "Quantity": 1
        }
      ]
    },
    {
      "CustomerId": 99999,
      "CartItems": [
        {
          "Sku": "94720",
          "Quantity": 2
        },
        {
          "Sku": "20844",
          "Quantity": 6
        },
        {
          "Sku": "11532",
          "Quantity": 1
        }
      ]
    },
    {
      "CustomerId": 100000,
      "CartItems": [
        {
          "Sku": "86725",
          "Quantity": 3
        },
        {
          "Sku": "94505",
          "Quantity": 8
        }
      ]
    }
  ]
}
```
