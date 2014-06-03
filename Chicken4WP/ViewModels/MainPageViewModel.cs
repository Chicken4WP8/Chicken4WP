using Caliburn.Micro;
using Chicken4WP.ViewModels.Home;
using Chicken4WP.ViewModels.Setting;

namespace Chicken4WP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            var currentUser = storageService.GetCurrentUser();
            if (currentUser != null)
            {
                App.UpdateCurrentUser(currentUser);
                navigationService.UriFor<HomePageViewModel>().Navigate();
            }
            else
            {
                navigationService.UriFor<ProxySettingPageViewModel>().Navigate();
            }
        }
    }
}
