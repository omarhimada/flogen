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
*RandomOrders-2020-03-06-21-54-21.json* (22 MB with indentation, 9 MB with no indentation)

Generation time: ~135 milliseconds (0.135s)
```
{
  "Orders": [
    {
      "CustomerId": 1,
      "CartItems": [
        {
          "Sku": "51897",
          "Quantity": 7
        },
        {
          "Sku": "78627",
          "Quantity": 6
        },
        {
          "Sku": "38305",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 2,
      "CartItems": [
        {
          "Sku": "95623",
          "Quantity": 1
        },
        {
          "Sku": "50623",
          "Quantity": 2
        }
      ]
    },
    {
      "CustomerId": 3,
      "CartItems": [
        {
          "Sku": "49493",
          "Quantity": 7
        },
        {
          "Sku": "92484",
          "Quantity": 4
        },
        {
          "Sku": "42596",
          "Quantity": 9
        }
      ]
    },
    {
      "CustomerId": 4,
      "CartItems": [
        {
          "Sku": "21725",
          "Quantity": 6
        }
      ]
    },
    {
      "CustomerId": 5,
      "CartItems": [
        {
          "Sku": "36967",
          "Quantity": 3
        },
        {
          "Sku": "666",
          "Quantity": 2
        },
        {
          "Sku": "86247",
          "Quantity": 6
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
          "Sku": "95793",
          "Quantity": 1
        }
      ]
    },
    {
      "CustomerId": 99998,
      "CartItems": [
        {
          "Sku": "73585",
          "Quantity": 3
        },
        {
          "Sku": "67201",
          "Quantity": 1
        }
      ]
    },
    {
      "CustomerId": 99999,
      "CartItems": [
        {
          "Sku": "38096",
          "Quantity": 5
        },
        {
          "Sku": "1727",
          "Quantity": 1
        },
        {
          "Sku": "01051",
          "Quantity": 5
        }
      ]
    },
    {
      "CustomerId": 100000,
      "CartItems": [
        {
          "Sku": "86191",
          "Quantity": 9
        }
      ]
    }
  ]
}
```
