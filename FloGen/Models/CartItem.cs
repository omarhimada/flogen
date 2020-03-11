using Newtonsoft.Json;

namespace FloGen.Models
{
    public class CartItem
    {
        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
