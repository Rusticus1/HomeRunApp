using System;
using Newtonsoft.Json;
namespace HomeRun.Models
{
    public class Device
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}