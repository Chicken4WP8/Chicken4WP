using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Chicken4WP.Models;

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
            {
                tweetService.GetHomeTimelineTweets(null,
                    tweets =>
                    {
                        Items = new ObservableCollection<Tweet>();
                        for (int i = 0; i < tweets.Count; i++)
                        {
                            Items.Insert(0, tweets[i]);
                        }
                    });
            }
        }

        public void AppBar_Next()
        {
            languageHelper.SetLanguage(new CultureInfo("en-US"));
        }

        public void AvatarClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
        }

        public void ItemClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
        }

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_Index_Header");
        }
    }
}
