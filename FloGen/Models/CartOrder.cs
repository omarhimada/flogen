using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FloGen.Models
{
  public class CartOrder
  {
    [JsonPropertyName("customerId")]
    public int CustomerId { get; set; }

    [JsonPropertyName("cart")]
    public IEnumerable<CartItem> CartItems { get; set; }
  }
}
