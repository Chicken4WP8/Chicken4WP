using System.Globalization;
using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusRetweetViewModel : PivotItemViewModelBase
    {
        protected override void SetLanguage()
        {
            DisplayName = "Retweet";
        }
    }
}
