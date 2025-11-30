using Newtonsoft.Json;

namespace FloGen.Models
{
    public class ManyRandomOrders
    {
        [JsonProperty("orders")]
        public ICollection<CartOrder> Orders { get; set; }
    }
}
