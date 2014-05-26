using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Chicken4WP.Common
{
    public class Const
    {
        #region defalut value
        public const string BASETWEETSERVICENAME = "Base";
        public const string TWIPTWEETSERVICENAME = "Twip4";

        public const string CHICKEN_FAMOUS = "https://raw.github.com/george674834080/Chicken/master/Chicken/Data/famous.json";
        public static string testAPI = "http://";

        public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        public const string DEFAULTSOURCE = "web";
        public const string DEFAULTSOURCEURL = "https://github.com/";
        #endregion

        #region default settings
        public static int MAX_COUNT = 200;
        public static int DEFAULT_COUNT_VALUE = 20;
        public static int DEFAULT_COUNT_VALUE_PLUS_ONE = DEFAULT_COUNT_VALUE + 1;
        public static string QUOTECHARACTER = "RT";
        #endregion

        #region rest api
        public const string DEFAULT_VALUE_TRUE = "true";
        public const string DEFAULT_VALUE_FALSE = "false";
        public const string FOLLOWED_BY = "followed_by";

        #region rest api parameters
        public const string ID = "id";
        public const string USER_ID = "user_id";
        public const string USER_SCREEN_NAME = "screen_name";
        public const string COUNT = "count";
        public const string SINCE_ID = "since_id";
        public const string MAX_ID = "max_id";
        public const string INCLUDE_ENTITIES = "include_entities";
        public const string DIRECT_MESSAGE_SKIP_STATUS = "skip_status";
        public const string CURSOR = "cursor";
        public const string STATUS = "status";
        public const string IN_REPLY_TO_STATUS_ID = "in_reply_to_status_id";
        public const string TEXT = "text";
        public const string SKIP_STATUS = "skip_status";
        //update my profile
        public const string USER_NAME = "name";
        public const string URL = "url";
        public const string LOCATION = "location";
        public const string DESCRIPTION = "description";
        //search page
        public const string SEARCH_QUERY = "q";
        public const string PAGE = "page";
        #endregion

        #region home page
        public const string STATUSES_HOMETIMELINE = "statuses/home_timeline.json";
        public const string STATUSES_MENTIONS_TIMELINE = "statuses/mentions_timeline.json";
        public const string DIRECT_MESSAGES = "direct_messages.json";
        public const string DIRECT_MESSAGES_SENT_BY_ME = "direct_messages/sent.json";
        #endregion

        #region profile page
        public const string USERS_SHOW = "users/show.json";
        public const string FRIENDSHIPS_CREATE = "friendships/create.json";
        public const string FRIENDSHIPS_DESTROY = "friendships/destroy.json";
        public const string USER_TIMELINE = "statuses/user_timeline.json";
        public const string USER_FAVORITE = "favorites/list.json";
        public const string USER_FOLLOWING_IDS = "friends/ids.json";
        public const string USER_FOLLOWER_IDS = "followers/ids.json";
        public const string USERS_LOOKUP = "users/lookup.json";
        public const string FRIENDSHIPS_LOOKUP = "friendships/lookup.json";
        #endregion

        #region status page
        public const string STATUSES_SHOW = "statuses/show.json";
        public const string STATUSES_RETWEET_IDS = "statuses/retweeters/ids.json";
        public const string ADD_TO_FAVORITES_CREATE = "favorites/create.json";
        public const string ADD_TO_FAVORITES_DESTROY = "favorites/destroy.json";
        public const string RETWEET_CREATE = "{0}statuses/retweet/{1}.json";
        public const string STATUSES_DESTROY = "{0}statuses/destroy/{1}.json";
        #endregion

        #region post new tweet
        public const string STATUS_POST_NEW_TWEET = "statuses/update.json";
        public const string STATUS_POST_NEW_TWEET_WITH_MEDIA = "statuses/update_with_media.json";
        #endregion

        #region new message
        public const string DIRECT_MESSAGE_POST_NEW_MESSAGE = "direct_messages/new.json";
        #endregion

        #region my profile
        public const string PROFILE_MYSELF = "account/verify_credentials.json";
        public const string PROFILE_UPDATE_MYPROFILE = "account/update_profile.json";
        #endregion

        #region search page
        public const string SEARCH_FOR_TWEETS = "search/tweets.json";
        public const string SEARCH_FOR_USERS = "users/search.json";
        #endregion

        #region api settings
        public const string TWEET_CONFIGURATION = "help/configuration.json";
        #endregion

        #endregion
    }
}
