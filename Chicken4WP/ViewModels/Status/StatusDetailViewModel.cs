﻿using System.Collections.ObjectModel;
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
            DisplayName = languageHelper.GetString("HomePage_Index_Header");
        }
    }
}