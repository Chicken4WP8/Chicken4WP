using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using Caliburn.Micro;
using Chicken4WP.Common;
using Chicken4WP.Models;
using Chicken4WP.Services;
using Chicken4WP.Services.Interface;
using System.Linq;

namespace Chicken4WP.ViewModels.Home
{
    public class IndexViewModel : Screen, IHandle<CultureInfo>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ToastMessageService toastMessageService;
        private readonly ITweetService tweetService;

        public IndexViewModel(ToastMessageService toastMessageService, IEventAggregator eventAggregator)
        {
            this.toastMessageService = toastMessageService;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
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
