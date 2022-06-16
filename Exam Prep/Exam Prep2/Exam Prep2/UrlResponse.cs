using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Exam_Prep2
{
    public class UrlResponse
    {
        [JsonPropertyName("url")]
        public int url { get; set; }

        [JsonPropertyName("shortCode")]
        public string shortCode { get; set; }

        [JsonPropertyName("shortUrl")]
        public string shortUrl { get; set; }

        [JsonPropertyName("dataCreated")]
        public string dataCreated { get; set; }

        [JsonPropertyName("visits")]
        public string visits { get; set; }
    }
}
