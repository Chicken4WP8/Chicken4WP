using System.Globalization;
using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Home
{
    public class MentionViewModel : Screen, IHandle<CultureInfo>
    {
        private readonly IEventAggregator eventAggregator;

        public MentionViewModel(IEventAggregator eventAggregator)
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
            DisplayName = LanguageHelper.GetString("HomePage_Mention_Header");
        }
    }
}
