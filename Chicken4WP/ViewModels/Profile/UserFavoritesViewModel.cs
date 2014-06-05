
namespace Chicken4WP.ViewModels.Profile
{
    public class UserFavoritesViewModel : PivotItemViewModelBase
    {
        protected override void Initialize()
        {           
        }

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("ProfilePage_Favorites_Header");
        }

        protected override void RefreshData()
        {
        }

        protected override void LoadData()
        {
        }
    }
}
