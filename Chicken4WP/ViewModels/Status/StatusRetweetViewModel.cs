using System.Globalization;
using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusRetweetViewModel : PivotItemViewModelBase
    {
        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("StatusPage_Retweets_Header");
        }

        protected override void Refresh()
        {
            throw new System.NotImplementedException();
        }

        protected override void Load()
        {
            throw new System.NotImplementedException();
        }
    }
}
