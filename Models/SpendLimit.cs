using Newtonsoft.Json;

namespace Version3.Models
{
    public class SpendLimit
    {
        [JsonProperty("limit")]
        public double Limit { get; set; }
    }
}