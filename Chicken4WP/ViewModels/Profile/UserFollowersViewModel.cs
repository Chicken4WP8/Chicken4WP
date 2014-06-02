
namespace Chicken4WP.ViewModels.Profile
{
    public class UserFollowersViewModel : PivotItemViewModelBase
    {

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("ProfilePage_Followers_Header");
        }

        protected override void RefreshData()
        {
        }

        protected override void LoadData()
        {
        }
    }
}
