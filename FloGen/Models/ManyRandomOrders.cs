using Newtonsoft.Json;
using System.Collections.Generic;

namespace FloGen.Models
{
    public class ManyRandomOrders
    {
        [JsonProperty("orders")]
        public ICollection<CartOrder> Orders { get; set; }
    }
}
