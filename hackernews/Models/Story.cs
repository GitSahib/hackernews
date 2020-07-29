using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace hackernews.Models
{
    public class Story
    {
        public int id { get; set; }
        public string title { get; set; }
        [JsonProperty("url")]
        public string link { get; set; }
    }
}
