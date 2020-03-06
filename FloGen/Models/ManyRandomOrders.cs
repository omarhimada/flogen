using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FloGen.Models
{
  public class ManyRandomOrders
  {
    [JsonPropertyName("orders")]
    public ICollection<CartOrder> Orders { get; set; }
  }
}
