
namespace Chicken4WP.ViewModels.Home
{
    public class MentionViewModel : PivotItemViewModelBase
    {

        protected override void SetLanguage()
        {
            DisplayName = languageHelper.GetString("HomePage_Mention_Header");
        }
    }
}
