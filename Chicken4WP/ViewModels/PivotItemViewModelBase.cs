using System.Globalization;
using System.Windows;
using Caliburn.Micro;
using Chicken4WP.Models;
using Chicken4WP.ViewModels.Status;

namespace Chicken4WP.ViewModels
{
    public abstract class PivotItemViewModelBase : ViewModelBase, IHandle<CultureInfo>
    {
        protected PivotItemViewModelBase()
        {
            SetLanguage();
        }

        public virtual void Handle(CultureInfo message)
        {
            SetLanguage();
        }

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

        protected abstract void SetLanguage();
    }
}
