
namespace Chicken4WP.ViewModels.Home
{
    public class DMViewModel : PivotItemViewModelBase
    {
        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_DM_Header");
        }

        protected override void Refresh()
        {
            //throw new System.NotImplementedException();
        }

        protected override void Load()
        {
            //throw new System.NotImplementedException();
        }
    }
}
