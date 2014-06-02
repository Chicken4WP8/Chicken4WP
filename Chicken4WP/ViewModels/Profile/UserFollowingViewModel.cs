
namespace Chicken4WP.ViewModels.Profile
{
    public class UserFollowingViewModel : PivotItemViewModelBase
    {
        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("ProfilePage_Following_Header");
        }

        protected override void RefreshData()
        {
        }

        protected override void LoadData()
        {
        }
    }
}
