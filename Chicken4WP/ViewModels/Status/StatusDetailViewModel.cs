using System.Collections.ObjectModel;
using System.Windows;
using Chicken4WP.Models;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusDetailViewModel : PivotItemViewModelBase
    {
        private Tweet tweet;
        public Tweet Tweet
        {
            get { return tweet; }
            set
            {
                tweet = value;
                NotifyOfPropertyChange(() => Tweet);
            }
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
            Tweet = storageService.GetTempTweet();
        }

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("StatusPage_Detail_Header");
        }

        protected override void Refresh()
        {
            throw new System.NotImplementedException();
        }

        protected override void Load()
        {
            throw new System.NotImplementedException();
        }
    }
}
