using System.Globalization;
using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Status
{
    public class StatusDetailViewModel : PivotItemViewModelBase
    {
        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_Index_Header");
        }
    }
}
