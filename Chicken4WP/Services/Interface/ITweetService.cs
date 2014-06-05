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
        /// <summary>
        /// the user parameter may come from user profile description,
        /// which can not get user id directly.
        /// so use user class instead of id.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="callback"></param>
        void GetUserTweets(User user,Option option, Action<TweetList> callback);
        #endregion

        #region proxy setting
        void TestProxySetting(Action<User> callback);
        #endregion
    }
}
