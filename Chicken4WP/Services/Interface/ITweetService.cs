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
        void GetStatusDetail(Option option, Action<Tweet> callback);
        #endregion

        #region proxy setting
        void TestProxySetting(Action<User> callback);
        #endregion
    }
}
