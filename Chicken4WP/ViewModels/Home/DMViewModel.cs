
namespace Chicken4WP.ViewModels.Home
{
    public class DMViewModel : PivotItemViewModelBase
    {
        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_DM_Header");
        }
    }
}
