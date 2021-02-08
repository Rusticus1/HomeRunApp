using System;
using Newtonsoft.Json;
namespace HomeRun.Models
{
    public class Room
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}