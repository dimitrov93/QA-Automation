using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Exam_Prep_3
{
    public class errors
    {
        [JsonPropertyName("error")]
        public string Id { get; set; }

    }
}
