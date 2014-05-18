using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using Chicken4WP.Common;
using Chicken4WP.Services;
using Chicken4WP.Services.Interface;
using Microsoft.Phone.Controls;

namespace Chicken4WP.ViewModels
{
    public abstract class ViewModelBase : Screen
    {
        protected readonly INavigationService navigationService;
        protected readonly IEventAggregator eventAggregator;
        protected readonly IStorageService storageService;
        protected readonly ITweetService tweetService;
        protected readonly ProgressService progressService;
        protected readonly ToastMessageService toastMessageService;
        protected readonly LanguageHelper languageHelper;

        protected BitmapImage defaultImage = new BitmapImage(new Uri("/Images/dark/cat.png", UriKind.Relative));

        public ViewModelBase()
        {
            var container = (Application.Current.Resources["bootstrapper"] as AppBootstrapper).Container;

            this.navigationService = container.GetInstance(typeof(INavigationService), null) as INavigationService;
            this.eventAggregator = container.GetInstance(typeof(IEventAggregator), null) as IEventAggregator;
            this.storageService = container.GetInstance(typeof(IStorageService), null) as IStorageService;
            var proxy = storageService.GetCurrentProxySetting();
            if (proxy != null)
            {
                this.tweetService = container.GetInstance(typeof(ITweetService), proxy.Name) as ITweetService;
            }
            this.progressService = container.GetInstance(typeof(ProgressService), null) as ProgressService;
            this.toastMessageService = container.GetInstance(typeof(ToastMessageService), null) as ToastMessageService;
            
            this.languageHelper = Application.Current.Resources["LanguageHelper"] as LanguageHelper;
        }
    }
}
