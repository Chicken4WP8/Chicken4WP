using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chicken4WP.Models
{
    public class ModelBase
    {
        [JsonIgnore]
        public bool HasError { get; set; }

        [JsonProperty("errors")]
        public List<ErrorMessage> Errors { get; set; }
    }

    public class ErrorMessage
    {
        public string Message { get; set; }

        public string Code { get; set; }
    }
}
