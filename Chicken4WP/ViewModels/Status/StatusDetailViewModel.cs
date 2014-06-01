using System.Collections.ObjectModel;
using Chicken4WP.Models;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusDetailViewModel : PivotItemViewModelBase
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

        protected override void OnInitialize()
        {
            base.OnInitialize();
            if (Items == null)
                Items = new ObservableCollection<Tweet>();
            var tweet = storageService.GetTempTweet();
            Items.Add(tweet);
        }

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("StatusPage_Detail_Header");
        }

        protected override void RefreshData()
        {
            string id = Items[0].InReplyToTweetId;
            if (string.IsNullOrEmpty(id))
            {
                base.LoadDataCompleted();
                return;
            }
            tweetService.GetStatusDetail(id,
                tweet =>
                {
                    if (!tweet.HasError)
                        Items.Insert(0, tweet);
                    base.LoadDataCompleted();
                });
        }

        protected override void LoadData()
        {
            base.LoadDataCompleted();
        }
    }
}
