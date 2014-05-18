using System.Globalization;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using Chicken4WP.Resources;
using Chicken4WP.Services.Interface;

namespace Chicken4WP
{
    public class LanguageHelper : PropertyChangedBase
    {
        protected readonly IEventAggregator eventAggregator;
        protected readonly IStorageService storageService;

        public LanguageHelper()
        {
            var container = (Application.Current.Resources["bootstrapper"] as AppBootstrapper).Container;
            this.eventAggregator = container.GetInstance(typeof(IEventAggregator), null) as IEventAggregator;
            this.storageService = container.GetInstance(typeof(IStorageService), null) as IStorageService;

            CultureInfo cultureInfo = null;
            var setting = storageService.GetCurrentLanguage();
            if (setting != null)
            {
                cultureInfo = new CultureInfo(setting.Name);
            }
            else
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }
            SetLanguage(cultureInfo);
        }

        public void SetLanguage(CultureInfo cultureInfo)
        {
            storageService.UpdateLanguage(cultureInfo.Name);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            NotifyOfPropertyChange("Item[]");
            eventAggregator.Publish(cultureInfo);
        }

        public string this[string key]
        {
            get { return GetString(key); }
        }

        public string GetString(string key, params string[] parameters)
        {
            return string.Format(GetString(key), parameters);
        }

        private static string GetString(string key)
        {
            return AppResources.ResourceManager.GetString(key);
        }
    }
}
