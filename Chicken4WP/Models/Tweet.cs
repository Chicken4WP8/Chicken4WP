using System;
using System.Collections.Generic;
using Chicken4WP.Common;
using Newtonsoft.Json;

namespace Chicken4WP.Models
{
    public class Tweet : ModelBase
    {
        [JsonProperty("created_at")]
        public string createdAt { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate
        { get { return Utils.ParseToDateTime(createdAt); } }

        [JsonProperty("id_str")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("source")]
        public string sourceText { get; set; }

        [JsonIgnore]
        public string Source
        { get { return Utils.ParseToSource(sourceText); } }

        [JsonIgnore]
        public string SourceUri
        { get { return Utils.ParseToSourceUrl(sourceText); } }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToTweetId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string InReplayToUserId { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplayToScreenName { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonProperty("retweet_count")]
        public string RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public string FavoriteCount { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("favorited")]
        public bool Favorited { get; set; }

        [JsonProperty("retweeted")]
        public bool Retweeted { get; set; }

        [JsonProperty("retweeted_status")]
        public Retweet RetweetStatus { get; set; }
    }

    public class Retweet : Tweet
    {
    }

    public class Coordinates
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] points { get; set; }

        [JsonIgnore]
        public double X
        { get { return points[0]; } }

        [JsonIgnore]
        public double Y
        { get { return points[1]; } }

        public override string ToString()
        {
            return (int)X + ", " + (int)Y;
        }
    }

    public class TweetList : List<Tweet>
    { }
}
