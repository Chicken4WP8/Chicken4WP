
namespace Chicken4WP.ViewModels.Home
{
    public class DMViewModel : PivotItemViewModelBase
    {
        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_DM_Header");
        }

        protected override void RefreshData()
        {
            //throw new System.NotImplementedException();
        }

        protected override void LoadData()
        {
            //throw new System.NotImplementedException();
        }
    }
}
