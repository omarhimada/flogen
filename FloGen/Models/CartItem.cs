using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FloGen.Models
{
  public class CartItem
  {
    [JsonPropertyName("sku")]
    public string Sku { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
  }
}
