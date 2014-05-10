using System.Linq;
using Caliburn.Micro;
using Chicken4WP.Common;
using Chicken4WP.Entities;
using Chicken4WP.Models;
using Chicken4WP.Services;
using Chicken4WP.Services.Interface;
using Chicken4WP.ViewModels.Home;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;

namespace Chicken4WP.ViewModels.Setting.Proxies
{
    public class TwipProxySettingPageViewModel : PropertyChangedBase
    {
        private readonly ITweetService tweetService;
        private readonly ToastMessageService toastMessageService;
        private readonly INavigationService navigationService;
        private readonly WaitCursor waitCursor;

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

        public TwipProxySettingPageViewModel(ToastMessageService toastMessageService, INavigationService navigationService)
        {
            this.tweetService = AppBootstrapper.Container.GetInstance(typeof(ITweetService), Const.TWIPTWEETSERVICE) as ITweetService;
            this.toastMessageService = toastMessageService;
            this.navigationService = navigationService;
            var twip = ChickenDataContext.Instance.Settings
                           .SingleOrDefault(s => s.Type == SettingType.ProxySetting && s.IsEnabled &&
                               s.Name == "Twip4");
            if (twip != null)
            {
                Url = twip.Data;
            }
            waitCursor = WaitCursorService.WaitCursor;
            waitCursor.Text = "loading...";
        }

        public void AppBar_Finish()
        {
            waitCursor.IsVisible = true;
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
                    waitCursor.IsVisible = false;
                    navigationService.UriFor<HomePageViewModel>().Navigate();
                });
        }

        private void SaveProxySetting()
        {
            var old = ChickenDataContext.Instance.Settings.SingleOrDefault(
                s => s.Type == SettingType.ProxySetting && s.IsInUsed);
            if (old != null)
            {
                old.IsInUsed = false;
            }
            var twip = ChickenDataContext.Instance.Settings
                .SingleOrDefault(s => s.Type == SettingType.ProxySetting && s.IsEnabled && s.Name == "Twip4");
            if (twip == null)
            {
                twip = new Entities.Setting
                {
                    Type = SettingType.ProxySetting,
                    Name = "Twip4",
                };
                ChickenDataContext.Instance.Settings.InsertOnSubmit(twip);
            }
            twip.Data = Url;
            twip.IsEnabled = twip.IsInUsed = true;
            ChickenDataContext.Instance.SubmitChanges();
        }

        private void SaveCurrentUser(User user)
        {
            var account = ChickenDataContext.Instance.Accounts.FirstOrDefault();
            account = new Account
            {
                Name = user.Name,
                ScreenName = user.ScreenName,
                Data = JsonConvert.SerializeObject(user),
            };
            ChickenDataContext.Instance.SubmitChanges();
        }
    }
}
