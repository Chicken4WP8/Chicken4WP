using Caliburn.Micro;
using Chicken4WP.Common;
using Chicken4WP.Entities;
using Chicken4WP.Models;
using Chicken4WP.ViewModels.Home;
using Microsoft.Phone.Controls;

namespace Chicken4WP.ViewModels.Setting.Proxies
{
    public class TwipProxySettingPageViewModel : ViewModelBase
    {
        protected readonly WaitCursor waitCursorService;

        public TwipProxySettingPageViewModel()
        {
            waitCursorService = WaitCursorService.WaitCursor;
            waitCursorService.Text = "loading...";
        }

        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                NotifyOfPropertyChange(() => Url);
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            var twip = storageService.GetCurrentProxySetting();
            if (twip != null)
            {
                Url = twip.Data;
            }
        }

        public void AppBar_Finish()
        {
            waitCursorService.IsVisible = true;
            TestService();
        }

        private void TestService()
        {
            tweetService.TestProxySetting(
                user =>
                {
                    SaveProxySetting();
                    SaveCurrentUser(user);
                    toastMessageService.HandleMessage("hello, " + user.ScreenName);
                    waitCursorService.IsVisible = false;
                    navigationService.UriFor<HomePageViewModel>().Navigate();
                });
        }

        private void SaveProxySetting()
        {
            var twip = new Entities.Setting
                {
                    Type = SettingType.ProxySetting,
                    Name = Const.TWIPTWEETSERVICENAME,
                    Data = Url,
                };
            storageService.UpdageProxySetting(twip);
        }

        private void SaveCurrentUser(User user)
        {
            storageService.UpdateCurrentUser(user);
        }
    }
}
