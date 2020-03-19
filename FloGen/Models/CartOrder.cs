using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FloGen.Models
{
    public class CartOrder
    {
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("cart")]
        public IEnumerable<CartItem> CartItems { get; set; }

        [JsonProperty("orderDate")]
        public DateTime OrderDate { get; set; }
    }
}
