using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Exam_Prep_3
{
    public class Events
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("topics")]
        public string topics { get; set; }

        [JsonPropertyName("thumbnail")]
        public string thumbnail { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("summary")]
        public string summary { get; set; }
    }
}
