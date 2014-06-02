using System;
using System.Globalization;
using System.Windows;
using Caliburn.Micro;
using Chicken4WP.Models;
using Chicken4WP.ViewModels.Profile;
using Chicken4WP.ViewModels.Status;

namespace Chicken4WP.ViewModels
{
    public abstract class PivotItemViewModelBase : ViewModelBase, IHandle<CultureInfo>
    {
        private Stretch stretch;

        protected PivotItemViewModelBase()
        {
            SetLanguage();
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
            }
        }

        public virtual void Handle(CultureInfo message)
        {
            SetLanguage();
        }

        public virtual void AvatarClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
            storageService.UpdateTempUser(tweet.User);
            navigationService.UriFor<ProfilePageViewModel>().Navigate();
        }

        public virtual void ItemClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
            storageService.UpdateTempTweet(tweet);
            navigationService.UriFor<StatusPageViewModel>().Navigate();
        }

        public void StretchingCompleted(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            BeginLoadData();

            switch (stretch)
            {
                case Stretch.Top:
                    RefreshData();
                    break;
                case Stretch.Bottom:
                    LoadData();
                    break;
                default:
                    break;
            }
        }

        public void StretchingBottom(object sender, EventArgs e)
        {
            stretch = Stretch.Bottom;
        }

        public void StretchingTop(object sender, EventArgs e)
        {
            stretch = Stretch.Top;
        }

        protected abstract void SetLanguage();

        protected virtual void BeginLoadData()
        {
            IsLoading = true;
            progressService.Show();
        }

        protected abstract void RefreshData();

        protected abstract void LoadData();

        protected virtual void LoadDataCompleted()
        {
            IsLoading = false;
            progressService.Hide();
        }
    }

    public enum Stretch
    {
        None,
        Top,
        Bottom,
    }
}
