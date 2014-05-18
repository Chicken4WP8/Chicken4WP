using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Caliburn.Micro;
using Chicken4WP.Models;

namespace Chicken4WP.ViewModels.Home
{
    public class IndexViewModel : ViewModelBase, IHandle<CultureInfo>
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
            SetLanguage();
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

        public void Handle(CultureInfo message)
        {
            SetLanguage();
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

        private void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_Index_Header");
        }
    }
}
