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
        public virtual void GetHomeTimelineTweets(Option option, Action<TweetList> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.STATUSES_HOMETIMELINE;
            Execute<TweetList>(request, option, callback);
        }

        public void GetMentions(Option option, Action<TweetList> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.STATUSES_MENTIONS_TIMELINE;
            Execute<TweetList>(request, option, callback);
        }
        #endregion

        #region status page
        public void GetStatusDetail(Option option, Action<Tweet> callback)
        {

        }
        #endregion

        #region proxy setting
        public void TestProxySetting(Action<User> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.PROFILE_MYSELF;
            Execute<User>(request, null, callback);
        }
        #endregion

        #region private method
        protected void Execute<T>(IRestRequest request, Option option, Action<T> callback)
        {
            if (option != null)
            {
                foreach (var item in option)
                {
                    request.AddParameter(item.Key, item.Value);
                }
                if (option.ContainsKey(Const.SINCE_ID) || option.ContainsKey(Const.MAX_ID))
                    option.Add(Const.COUNT, Const.DEFAULT_COUNT_VALUE_PLUS_ONE);
                else
                    option.Add(Const.COUNT, Const.DEFAULT_COUNT_VALUE);
            }

            client.ExecuteAsync(request, response =>
            {
                var data = JsonConvert.DeserializeObject<T>(response.Content, Const.JsonSettings);
                callback(data);
            });
        }
        #endregion
    }
}
