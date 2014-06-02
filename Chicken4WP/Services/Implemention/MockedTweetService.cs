using System;
using System.IO;
using System.Windows;
using Chicken4WP.Common;
using Chicken4WP.Models;
using Chicken4WP.Services.Interface;
using Newtonsoft.Json;

namespace Chicken4WP.Services.Implemention
{
    public class MockedTweetService : ITweetService
    {
        public void GetHomeTimelineTweets(Option option, Action<TweetList> callback)
        {
            string url = "SampleData/hometimeline.json";
            HandleWebRequest(url, callback);
        }

        public void GetMentions(Option option, Action<TweetList> callback)
        {
            string url = "SampleData/mentions.json";
            HandleWebRequest(url, callback);
        }

        public void GetStatusDetail(string statusId, Action<Tweet> callback)
        {
            string url = "SampleData/tweet.json";
            HandleWebRequest(url, callback);
        }

        public void GetFriendshipConnections(string userIdList, Action<Friendships> callback)
        {
            string url = "SampleData/friendships.json";
            //string url = "SampleData/Rate_limit_exceeded.json";
            HandleWebRequest(url, callback);
        }

        public void TestProxySetting(Action<User> callback)
        {
            string url = "SampleData/myprofile.json";
            HandleWebRequest(url, callback);
        }

        #region private method
        private void HandleWebRequest<T>(string url, Action<T> callback)
            where T : ModelBase, new()
        {
            var streamInfo = Application.GetResourceStream(new Uri(url, UriKind.Relative));
            var data = new T();
            using (var reader = new StreamReader(streamInfo.Stream))
            {
                string s = reader.ReadToEnd();
                try
                {
                    data = JsonConvert.DeserializeObject<T>(s, Const.JsonSettings);
                }
                catch (Exception exception)
                {
                    var error = JsonConvert.DeserializeObject<ModelBase>(s, Const.JsonSettings);
                    data.HasError = true;
                    data.Errors = error.Errors;
                }
            }
            Deployment.Current.Dispatcher.BeginInvoke(() => callback(data));
        }
        #endregion
    }
}
