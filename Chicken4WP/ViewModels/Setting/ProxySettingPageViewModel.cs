using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Chicken4WP.Entities;
using Chicken4WP.Services;
using Chicken4WP.ViewModels.Setting.Proxies;

namespace Chicken4WP.ViewModels.Setting
{
    public class ProxySettingPageViewModel : Screen
    {
        private readonly ProgressService progressService;
        private readonly INavigationService navigationService;

        public ProxySettingPageViewModel(INavigationService navigationService, ProgressService progressService)
        {
            this.navigationService = navigationService;
            this.progressService = progressService;
        }

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
                var list = ChickenDataContext.Instance.Settings.Where(s => s.Type == SettingType.ProxySetting);
                Items = new ObservableCollection<Entities.Setting>(list);
                SelectedItem = list.SingleOrDefault(s => s.IsInUsed) ?? list.First();
            }
        }

        public void AppBar_Next()
        {
            switch (SelectedItem.Name)
            {
                case "Base":
                    navigationService
                        .UriFor<BaseProxySettingPageViewModel>()
                        .Navigate();
                    break;
                case "Twip4":
                    navigationService
                        .UriFor<TwipProxySettingPageViewModel>()
                        .Navigate();
                    break;
            }
        }


    }
}
