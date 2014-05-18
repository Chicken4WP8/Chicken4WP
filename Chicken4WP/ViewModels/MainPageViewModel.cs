using Caliburn.Micro;
using Chicken4WP.ViewModels.Setting;

namespace Chicken4WP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        protected override void OnActivate()
        {
            base.OnActivate();
            var currentUser = storageService.GetCurrentUser();
            if (currentUser != null)
            {
                App.UpdateCurrentUser(currentUser);
            }
            else
            {
                navigationService.UriFor<ProxySettingPageViewModel>().Navigate();
            }
        }
    }
}
