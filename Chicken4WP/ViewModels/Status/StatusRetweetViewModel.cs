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

        protected override void RefreshData()
        {
            throw new System.NotImplementedException();
        }

        protected override void LoadData()
        {
            throw new System.NotImplementedException();
        }
    }
}
