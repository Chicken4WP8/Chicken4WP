using System;
using System.Globalization;
using System.Windows;
using Caliburn.Micro;
using Chicken4WP.Models;
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

        public virtual void Handle(CultureInfo message)
        {
            SetLanguage();
        }

        protected abstract void SetLanguage();

        public virtual void AvatarClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
        }

        public virtual void ItemClick(object sender, RoutedEventArgs e)
        {
            var tweet = sender as Tweet;
            storageService.UpdateTempTweet(tweet);
            navigationService.UriFor<StatusPageViewModel>().Navigate();
        }

        public void StretchingCompleted(object sender, EventArgs e)
        {
            switch (stretch)
            {
                case Stretch.Top:
                    Refresh();
                    break;
                case Stretch.Bottom:
                    Load();
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

        protected abstract void Refresh();
        protected abstract void Load();
    }

    public enum Stretch
    {
        None,
        Top,
        Bottom,
    }
}
