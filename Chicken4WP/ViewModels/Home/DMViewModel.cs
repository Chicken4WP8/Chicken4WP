using System.Globalization;
using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Home
{
    public class DMViewModel : ViewModelBase, IHandle<CultureInfo>
    {
        private readonly IEventAggregator eventAggregator;

        protected override void OnActivate()
        {
            base.OnActivate();
            SetLanguage();
        }

        public void Handle(CultureInfo message)
        {
            SetLanguage();
        }

        private void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_DM_Header");
        }
    }
}
