using System;
using System.Collections.Generic;
using System.Windows;
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
        protected readonly ToastMessageService toastMessageService;
        protected readonly LanguageHelper languageService;

        protected TweetServiceBase()
        {
            client = new RestClient();
            var container = (Application.Current.Resources["bootstrapper"] as AppBootstrapper).Container;
            this.toastMessageService = container.GetInstance(typeof(ToastMessageService), null) as ToastMessageService;
            languageService = Application.Current.Resources["LanguageHelper"] as LanguageHelper;
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
        public void GetStatusDetail(string statusId, Action<Tweet> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.STATUSES_SHOW;
            var option = new Option();
            option.Add(Const.ID, statusId);
            Execute<Tweet>(request, option, callback);
        }
        #endregion

        #region profile page
        public void GetFriendshipConnections(string userIdList, Action<Friendships> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.FRIENDSHIPS_LOOKUP;
            var option = new Option();
            option.Add(Const.USER_ID, userIdList);
            Execute<Friendships>(request, option, callback);
        }

        public void GetUserTweets(User user, Option option, Action<TweetList> callback)
        {
            var request = new RestRequest();
            request.Resource = Const.USER_TIMELINE;
            if (!string.IsNullOrEmpty(user.Id))
            {
                option.Add(Const.USER_ID, user.Id);
            }
            else if (!string.IsNullOrEmpty(user.ScreenName))
            {
                option.Add(Const.USER_SCREEN_NAME, user.ScreenName);
            }
            Execute<TweetList>(request, option, callback);
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
            where T : ModelBase, new()
        {
            #region add parameters
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
            #endregion

            client.ExecuteAsync(request, response =>
            {
                var data = new T();
                #region handle error
                try
                {
                    if (response.Content.Length == 0)
                    {
                        var msg = new ErrorMessage { Message = languageService.GetString("Toast_Msg_UnknowError") };
                        data.Errors = new List<ErrorMessage>();
                        data.Errors.Add(msg);
                        data.HasError = true;
                    }
                    else
                    {
                        data = JsonConvert.DeserializeObject<T>(response.Content, Const.JsonSettings);
                    }
                }
                catch (Exception ex)
                {
                    data.HasError = true;
                    var error = JsonConvert.DeserializeObject<ModelBase>(response.Content, Const.JsonSettings);
                    data.Errors = error.Errors;
                }
                #endregion
                #region callback
                finally
                {
                    Deployment.Current.Dispatcher.BeginInvoke(
                        () =>
                        {
                            if (data.HasError)
                            {
                                toastMessageService.HandleMessage(
                                    new ToastMessage
                                {
                                    Message = data.Errors[0].Message,
                                    Complete = () => callback(data)
                                });
                            }
                            else
                                callback(data);
                        });
                }
                #endregion
            });
        }
        #endregion
    }
}
