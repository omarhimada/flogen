using Newtonsoft.Json;

namespace FloGen.Models
{
    /// <summary>
    /// Based on kaggle 'telecom customer' model
    /// </summary>
    public class Customer
    {
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("churned")]
        public bool Churned { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// AKA Tenure
        /// </summary>
        [JsonProperty("accountLength")]
        public float AccountLength { get; set; }

        [JsonProperty("daysSinceLastPurchase")]
        public float DaysSinceLastPurchase { get; set; }

        [JsonProperty("areaCode")]
        public string AreaCode { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("internationalPlan")]
        public bool InternationalPlan { get; set; }

        [JsonProperty("voice")]
        public bool Voice { get; set; }

        [JsonProperty("numberOfMessages")]
        public float NumberOfMessages { get; set; }

        [JsonProperty("totalDaytimeMinutes")]
        public float TotalDaytimeMinutes { get; set; }

        [JsonProperty("totalDaytimeCalls")]
        public float TotalDaytimeCalls { get; set; }

        [JsonProperty("totalDaytimeCharges")]
        public float TotalDaytimeCharges { get; set; }

        [JsonProperty("totalEveningMinutes")]
        public float TotalEveningMinutes { get; set; }

        [JsonProperty("totalEveningCalls")]
        public float TotalEveningCalls { get; set; }

        [JsonProperty("totalEveningCharges")]
        public float TotalEveningCharges { get; set; }

        [JsonProperty("totalNightMinutes")]
        public float TotalNightMinutes { get; set; }

        [JsonProperty("totalNightCalls")]
        public float TotalNightCalls { get; set; }

        [JsonProperty("totalNightCharges")]
        public float TotalNightCharges { get; set; }

        [JsonProperty("totalInternationalMinutes")]
        public float TotalInternationalMinutes { get; set; }

        [JsonProperty("totalInternationalCalls")]
        public float TotalInternationalCalls { get; set; }

        [JsonProperty("totalInternationalCharges")]
        public float TotalInternationalCharges { get; set; }

        [JsonProperty("numberOfCustomerServiceCalls")]
        public float NumberOfCustomerServiceCalls { get; set; }
    }
}
