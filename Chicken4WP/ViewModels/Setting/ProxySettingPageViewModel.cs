using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Chicken4WP.Common;
using Chicken4WP.ViewModels.Setting.Proxies;

namespace Chicken4WP.ViewModels.Setting
{
    public class ProxySettingPageViewModel : ViewModelBase
    {
        private ObservableCollection<Entities.Setting> items;
        public ObservableCollection<Entities.Setting> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        private Entities.Setting selectedItem;
        public Entities.Setting SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            if (Items == null)
            {
                var list = storageService.GetProxySettings();
                Items = new ObservableCollection<Entities.Setting>(list);
                SelectedItem = list.SingleOrDefault(s => s.IsInUsed) ?? list.First();
            }
        }

        public void AppBar_Next()
        {
            switch (SelectedItem.Name)
            {
                case Const.BASETWEETSERVICENAME:
                    navigationService
                        .UriFor<BaseProxySettingPageViewModel>()
                        .Navigate();
                    break;
                case Const.TWIPTWEETSERVICENAME:
                    navigationService
                        .UriFor<TwipProxySettingPageViewModel>()
                        .Navigate();
                    break;
            }
        }
    }
}
