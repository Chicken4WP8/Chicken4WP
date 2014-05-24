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
        public void GetHomeTimelineTweets(HomeTimelineTweetOption option, Action<TweetList> callback)
        {
            string url = "SampleData/hometimeline.json";
            HandleWebRequest(url, callback);
        }

        public void GetMentions(MentionOption option, Action<TweetList> callback)
        {
            string url = "SampleData/mentions.json";
            HandleWebRequest(url, callback);
        }

        public void TestProxySetting(Action<User> callback)
        {
            string url = "SampleData/myprofile.json";
            HandleWebRequest(url, callback);
        }

        #region private method
        private void HandleWebRequest<T>(string url, Action<T> callback)
        {
            var streamInfo = Application.GetResourceStream(new Uri(url, UriKind.Relative));
            var result = default(T);
            using (var reader = new StreamReader(streamInfo.Stream))
            {
                string s = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<T>(s, Const.JsonSettings);
            }
            Deployment.Current.Dispatcher.BeginInvoke(() => callback(result));
        }
        #endregion
    }
}
