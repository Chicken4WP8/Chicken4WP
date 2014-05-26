using System.Collections.ObjectModel;
using System.Globalization;
using Chicken4WP.Common;
using Chicken4WP.Models;
using Chicken4WP.Services.Interface;

namespace Chicken4WP.ViewModels.Home
{
    public class IndexViewModel : PivotItemViewModelBase
    {
        private ObservableCollection<Tweet> items;
        public ObservableCollection<Tweet> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (Items == null)
                Items = new ObservableCollection<Tweet>();
            Refresh();
        }

        public void AppBar_Next()
        {
            languageHelper.SetLanguage(new CultureInfo("en-US"));
        }

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_Index_Header");
        }

        protected override void Refresh()
        {
            string sinceId = string.Empty;
            var option = new Option();
            if (items.Count != 0)
            {
                sinceId = items[0].Id;
                option.Add(Const.SINCE_ID, sinceId);
            }
            tweetService.GetHomeTimelineTweets(option,
                tweets =>
                {
#if !LOCAL
                    if (sinceId == tweets[tweets.Count - 1].Id)
                        tweets.RemoveAt(tweets.Count - 1);
#endif
                    for (int i = 0; i < tweets.Count; i++)
                    {
                        Items.Insert(0, tweets[i]);
                        if (items.Count >= Const.MAX_COUNT)
                            Items.RemoveAt(Items.Count - 1);
                    }
                });
        }

        protected override void Load()
        {
            if (Items.Count == 0)
                return;
            string maxId = Items[Items.Count - 1].Id;
            var option = new Option();
            option.Add(Const.MAX_ID, maxId);
            tweetService.GetHomeTimelineTweets(option,
                tweets =>
                {
#if !LOCAL
                    if (maxId == tweets[0].Id)
                        tweets.RemoveAt(0);
#endif
                    foreach (var tweet in tweets)
                    {
                        Items.Add(tweet);
                    }
                });
        }
    }
}
