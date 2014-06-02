using System;

namespace Chicken4WP.ViewModels.Profile
{
    public class UserTweetsViewModel : PivotItemViewModelBase
    {

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("ProfilePage_Tweets_Header");
        }

        protected override void RefreshData()
        {
        }

        protected override void LoadData()
        {
        }
    }
}
