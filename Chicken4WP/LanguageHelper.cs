using System.Globalization;
using System.Linq;
using System.Threading;
using Caliburn.Micro;
using Chicken4WP.Entities;
using Chicken4WP.Resources;

namespace Chicken4WP
{
    public class LanguageHelper : PropertyChangedBase
    {
        public LanguageHelper()
        {
            CultureInfo cultureInfo = null;
            var setting = ChickenDataContext.Instance.Settings.FirstOrDefault(
                s => s.Type == SettingType.LanguageSetting && s.IsInUsed && s.IsEnabled);
            if (setting != null)
            {
                cultureInfo = new CultureInfo(setting.Name);
            }
            else
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }
            Helper.SetLanguage(cultureInfo);
        }

        public void SetLanguage(CultureInfo cultureInfo)
        {
            Helper.SetLanguage(cultureInfo);
            NotifyOfPropertyChange("Item[]");
            var eventAggregator = AppBootstrapper.Container.GetInstance(typeof(IEventAggregator), null) as IEventAggregator;
            eventAggregator.Publish(cultureInfo);
        }

        public string this[string key]
        {
            get { return Helper.GetString(key); }
        }

        public static string GetString(string key, params string[] parameters)
        {
            return string.Format(Helper.GetString(key), parameters);
        }

        private class Helper
        {
            public static void SetLanguage(CultureInfo cultureInfo)
            {
                var old = ChickenDataContext.Instance.Settings.FirstOrDefault(
                    s => s.Type == SettingType.LanguageSetting && s.IsInUsed);
                if (old != null)
                {
                    old.IsInUsed = false;
                }
                var setting = ChickenDataContext.Instance.Settings.FirstOrDefault(
                    s => s.Type == SettingType.LanguageSetting && s.IsEnabled && s.Name == cultureInfo.Name);
                if (setting == null)
                {
                    setting = new Setting
                    {
                        Type = SettingType.LanguageSetting,
                        Name = cultureInfo.Name,
                    };
                    ChickenDataContext.Instance.Settings.InsertOnSubmit(setting);
                }
                setting.IsEnabled = setting.IsInUsed = true;
                ChickenDataContext.Instance.SubmitChanges();

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }

            public static string GetString(string key)
            {
                return AppResources.ResourceManager.GetString(key);
            }
        }
    }
}
