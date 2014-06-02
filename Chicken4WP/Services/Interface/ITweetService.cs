using System;
using Chicken4WP.Models;

namespace Chicken4WP.Services.Interface
{
    public interface ITweetService
    {
        #region home timeline
        void GetHomeTimelineTweets(Option option, Action<TweetList> callback);
        void GetMentions(Option option, Action<TweetList> callback);
        #endregion

        #region status page
        void GetStatusDetail(string statusId, Action<Tweet> callback);
        #endregion

        #region profile page
        void GetFriendshipConnections(string userIdList, Action<Friendships> callback);
        #endregion

        #region proxy setting
        void TestProxySetting(Action<User> callback);
        #endregion
    }
}
