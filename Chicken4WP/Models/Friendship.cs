using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chicken4WP.Models
{
    public class Friendship : ModelBase
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        public List<string> Connections { get; set; }
    }

    public class Friendships : ModelBaseList<Friendship>
    { }
}
