using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Caliburn.Micro;
using Chicken4WP.Common;
using Chicken4WP.Models;
using Chicken4WP.Services;
using Chicken4WP.Services.Interface;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;

namespace Chicken4WP.ViewModels.Home
{
    public class IndexViewModel : Screen, IHandle<CultureInfo>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ToastMessageService toastMessageService;
        private readonly ITweetService tweetService;
        private readonly ImageCacheService imageCacheService;

        private static BitmapImage defaultImage = new BitmapImage(new Uri("/Images/dark/cat.png", UriKind.Relative));

        public IndexViewModel(ToastMessageService toastMessageService, IEventAggregator eventAggregator, ImageCacheService imageCacheService)
        {
            this.toastMessageService = toastMessageService;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
            this.imageCacheService = imageCacheService;
            this.tweetService = AppBootstrapper.Container.GetInstance(typeof(ITweetService), Const.TWIPTWEETSERVICE) as ITweetService;

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

        private Tweet selectedItem;
        public Tweet SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
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
            var button = sender as Button;
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
