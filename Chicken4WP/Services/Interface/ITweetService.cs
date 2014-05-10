using System;
using Chicken4WP.Models;

namespace Chicken4WP.Services.Interface
{
    public interface ITweetService
    {
        #region home timeline
        void GetHomeTimelineTweets(HomeTimelineTweetOption option, Action<TweetList> callback); 
        #endregion

        #region proxy setting
        void TestProxySetting(Action<User> callback);
        #endregion
    }
}
