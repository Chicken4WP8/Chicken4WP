using System;
using System.Collections.Generic;
using Chicken4WP.Common;
using Newtonsoft.Json;

namespace Chicken4WP.Models
{
    public class User : ModelBase
    {
        [JsonProperty("id_str")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("entities")]
        public UserProfileEntities Entities { get; set; }

        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        [JsonProperty("followers_count")]
        public string FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public string FollowingCount { get; set; }

        [JsonProperty("listed_count")]
        public string ListedCount { get; set; }

        [JsonProperty("created_at")]
        public string createdAt { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate
        { get { return Utils.ParseToDateTime(createdAt); } }

        [JsonProperty("favourites_count")]
        public string FavoritesCount { get; set; }

        [JsonProperty("verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("statuses_count")]
        public string StatusesCount { get; set; }

        [JsonProperty("is_translator")]
        public bool IsTranslator { get; set; }

        [JsonProperty("profile_banner_url")]
        public string UserProfileBannerUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("following")]
        [JsonConverter(typeof(StringToBooleanConverter))]
        public bool IsFollowing { get; set; }

        [JsonProperty("follow_request_sent")]
        public bool IsFollowRequestSent { get; set; }
    }

    public class UserProfileEntities
    {
        [JsonProperty("url")]
        public UserProfileUrlEntities UserProfileUrlEntities { get; set; }

        [JsonProperty("description")]
        public UserProfileDescriptionEntities DescriptionEntities { get; set; }
    }

    public class UserProfileUrlEntities
    {
        [JsonProperty("urls")]
        public List<UrlEntity> Urls { get; set; }
    }

    public class UserProfileDescriptionEntities
    {
        [JsonProperty("urls")]
        public List<UrlEntity> Urls { get; set; }
    }
}
