using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chicken4WP.Models
{
    public abstract class EntityBase
    {
        public abstract EntityType EntityType { get; }

        public int Index { get; set; }

        public virtual string DisplayText { get; private set; }
    }

    public enum EntityType
    {
        None = 0,
        Media = 1,
        HashTag = 2,
        Url = 3,
        UserMention = 4,
    }

    public class Entities
    {
        [JsonProperty("media")]
        public List<MediaEntity> Medias { get; set; }

        [JsonProperty("hashtags")]
        public List<HashTag> HashTags { get; set; }

        [JsonProperty("urls")]
        public List<UrlEntity> Urls { get; set; }

        [JsonProperty("user_mentions")]
        public List<UserMention> UserMentions { get; set; }
    }

    public class UserMention : EntityBase
    {
        public override EntityType EntityType
        {
            get
            {
                return EntityType.UserMention;
            }
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonIgnore]
        public override string DisplayText
        {
            get
            {
                return "@" + ScreenName;
            }
        }
    }

    public class HashTag : EntityBase
    {
        public override EntityType EntityType
        {
            get
            {
                return EntityType.HashTag;
            }
        }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonIgnore]
        public override string DisplayText
        {
            get
            {
                return "#" + Text;
            }
        }
    }

    public class UrlEntity : EntityBase
    {
        public override EntityType EntityType
        {
            get
            {
                return EntityType.Url;
            }
        }

        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonIgnore]
        public override string DisplayText
        { get { return Url; } }

        [JsonProperty("display_url")]
        public string DisplayUrl { get; set; }

        [JsonProperty("expanded_url")]
        public string ExpandedUrl { get; set; }

        [JsonIgnore]
        public string TruncatedUrl
        {
            get
            {
                int index = DisplayUrl.IndexOf("/");
                if (index != -1)
                    return "[" + DisplayUrl.Remove(index) + "]";
                else
                    return "[" + DisplayUrl + "]";
            }
        }
    }

    public class MediaEntity : UrlEntity
    {
        public override EntityType EntityType
        {
            get
            {
                return EntityType.Media;
            }
        }

        public string Type { get; set; }

        [JsonProperty("media_url_https")]
        public string MediaUrl { get; set; }

        [JsonIgnore]
        public string MediaUrlThumb
        {
            get
            {
                return MediaUrl + ":thumb";
            }
        }

        [JsonIgnore]
        public string MediaUrlSmall
        {
            get
            {
                return MediaUrl + ":small";
            }
        }
    }
}
