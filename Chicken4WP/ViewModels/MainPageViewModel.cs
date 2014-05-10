using System.Linq;
using Caliburn.Micro;
using Chicken4WP.Entities;
using Chicken4WP.ViewModels.Setting;

namespace Chicken4WP.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel(INavigationService navigationService)
        {
            var currentUser = ChickenDataContext.Instance.Accounts.FirstOrDefault();
            if (currentUser != null)
            {

            }
            else
            {
                navigationService.UriFor<ProxySettingPageViewModel>().Navigate();
            }
        }
    }
}
