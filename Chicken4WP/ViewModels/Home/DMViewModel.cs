using System.Globalization;
using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Home
{
    public class DMViewModel : Screen, IHandle<CultureInfo>
    {
        private readonly IEventAggregator eventAggregator;

        public DMViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);

            SetLanguage();
        }

        public void Handle(CultureInfo message)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            DisplayName = LanguageHelper.GetString("HomePage_DM_Header");
        }
    }
}
