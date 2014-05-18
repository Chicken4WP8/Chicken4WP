using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Chicken4WP.Entities;
using Chicken4WP.Models;
using Chicken4WP.Services;
using Chicken4WP.Services.Interface;

namespace Chicken4WP.ViewModels.Home
{
    public class IndexViewModel : Screen, IHandle<CultureInfo>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ToastMessageService toastMessageService;
        private readonly ITweetService tweetService;

        private static BitmapImage defaultImage = new BitmapImage(new Uri("/Images/dark/cat.png", UriKind.Relative));

        public IndexViewModel(ToastMessageService toastMessageService, IEventAggregator eventAggregator)
        {
            this.toastMessageService = toastMessageService;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            var proxy = ChickenDataContext.Instance.Settings.SingleOrDefault(s => s.Type == SettingType.ProxySetting && s.IsInUsed);
            this.tweetService = (Application.Current.Resources["bootstrapper"] as AppBootstrapper).Container
                .GetInstance(typeof(ITweetService), proxy.Name) as ITweetService;
            SetLanguage();
        }

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
            var language = Application.Current.Resources["LanguageHelper"] as LanguageHelper;
            language.SetLanguage(new CultureInfo("en-US"));
        }

        public void AvatarClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
        }

        public void ItemClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
        }

        public void Handle(CultureInfo message)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            DisplayName = LanguageHelper.GetString("HomePage_Index_Header");
        }
    }
}
