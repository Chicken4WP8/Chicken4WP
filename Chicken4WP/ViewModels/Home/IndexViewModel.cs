using System.Globalization;
using Caliburn.Micro;
using System.Windows;

namespace Chicken4WP.ViewModels.Home
{
    public class IndexViewModel : Screen, IHandle<CultureInfo>
    {
        private readonly IEventAggregator eventAggregator;

        public IndexViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);

            SetLanguage();
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
