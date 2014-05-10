using Caliburn.Micro;

namespace Chicken4WP.ViewModels.Setting.Proxies
{
    public class BaseProxySettingPageViewModel : PropertyChangedBase
    {
        private string proxyName;
        public string ProxyName
        {
            get
            {
                return proxyName;
            }
            set
            {
                proxyName = value;
                NotifyOfPropertyChange(() => ProxyName);
            }
        }

        public void AppBar_Finish()
        {

        }
    }
}
