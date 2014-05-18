using System.Globalization;
using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusRetweetViewModel : ViewModelBase, IHandle<CultureInfo>
    {
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
            DisplayName = languageHelper.GetString("HomePage_Index_Header");
        }
    }
}
