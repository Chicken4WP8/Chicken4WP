using System;
using Chicken4WP.Common;
using Chicken4WP.Models;
using Chicken4WP.Services.Interface;
using Newtonsoft.Json;
using RestSharp;

namespace Chicken4WP.Services.Implemention
{
    public abstract class TweetServiceBase : ITweetService
    {
        protected RestClient client;

        protected TweetServiceBase()
        {
            client = new RestClient();
        }

        #region home timeline
        public virtual void GetHomeTimelineTweets(HomeTimelineTweetOption option, Action<TweetList> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.STATUSES_HOMETIMELINE;
            if (option != null)
            {
                if (option.SinceId != null)
                {
                    request.AddParameter(Const.SINCE_ID, option.SinceId);
                }
                if (option.MaxId != null)
                {
                    request.AddParameter(Const.MAX_ID, option.MaxId);
                }
            }
            Execute<TweetList>(request, callback);
        }
        #endregion

        #region proxy setting
        public void TestProxySetting(Action<User> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.PROFILE_MYSELF;
            Execute<User>(request, callback);
        }
        #endregion

        protected void Execute<T>(IRestRequest request, Action<T> callback)
        {
            client.ExecuteAsync(request, response =>
                {
                    var data = JsonConvert.DeserializeObject<T>(response.Content, Const.JsonSettings);
                    callback(data);
                });
        }
    }
}
