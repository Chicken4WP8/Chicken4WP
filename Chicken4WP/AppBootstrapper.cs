using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using Chicken4WP.Common;
using Chicken4WP.Services;
using Chicken4WP.Services.Implemention;
using Chicken4WP.Services.Interface;
using Chicken4WP.ViewModels;
using Chicken4WP.ViewModels.Home;
using Chicken4WP.ViewModels.Setting;
using Chicken4WP.ViewModels.Setting.Proxies;
using Microsoft.Phone.Controls;

namespace Chicken4WP
{
    public class AppBootstrapper : PhoneBootstrapperBase
    {
        private PhoneContainer container = new PhoneContainer();
        public PhoneContainer Container
        {
            get
            {
                return container;
            }
        }

        public AppBootstrapper()
        {
            Start();
        }

        protected override void Configure()
        {
            if (!Execute.InDesignMode)
                container.RegisterPhoneServices(RootFrame);

            #region services
            container.Instance<ProgressService>(new ProgressService(RootFrame));
            container.Singleton<StorageService>();
            container.PerRequest<ToastMessageService>();
            container.PerRequest<ImageCacheService>();
#if LOCAL
            container.RegisterPerRequest(typeof(ITweetService), Const.TWIPTWEETSERVICE, typeof(MockedTweetService));
#else
            container.RegisterPerRequest(typeof(ITweetService), Const.TWIPTWEETSERVICENAME, typeof(TwipTweetService));
            //container.RegisterPerRequest(typeof(ITweetService), "baseTweetService", typeof(BaseTweetService));
#endif
            #endregion

            #region view models
            container.PerRequest<MainPageViewModel>();
            container.PerRequest<ProxySettingPageViewModel>();
            container.PerRequest<BaseProxySettingPageViewModel>();
            container.PerRequest<TwipProxySettingPageViewModel>();
            #endregion

            #region pivot
            container.PerRequest<HomePageViewModel>();
            container.PerRequest<IndexViewModel>();
            container.PerRequest<MentionViewModel>();
            container.PerRequest<DMViewModel>();
            #endregion

            AddCustomConventions();
        }

        private static void AddCustomConventions()
        {
            //appbar:
            ConventionManager.AddElementConvention<BindableAppBarButton>(
            Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
            Control.IsEnabledProperty, "DataContext", "Click");
            //list picker
            ConventionManager.AddElementConvention<ListPicker>(
                ItemsControl.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
                .ApplyBinding =
                (viewModelType, path, property, element, convention) =>
                {
                    if (ConventionManager.GetElementConvention(typeof(ItemsControl))
                        .ApplyBinding(viewModelType, path, property, element, convention))
                    {
                        ConventionManager
                            .ConfigureSelectedItem(element, ListPicker.SelectedItemProperty, viewModelType, path);
                        return true;
                    }
                    return false;
                };
            #region pivot
            ConventionManager.AddElementConvention<Pivot>(Pivot.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
        .ApplyBinding =
        (viewModelType, path, property, element, convention) =>
        {
            if (ConventionManager
                .GetElementConvention(typeof(ItemsControl))
                .ApplyBinding(viewModelType, path, property, element, convention))
            {
                ConventionManager
                    .ConfigureSelectedItem(element, Pivot.SelectedItemProperty, viewModelType, path);
                ConventionManager
                    .ApplyHeaderTemplate(element, Pivot.HeaderTemplateProperty, null, viewModelType);
                return true;
            }
            return false;
        };
            #endregion
            //long list selector
            ConventionManager.AddElementConvention<LongListSelector>(LongListSelector.ItemsSourceProperty, "DataContext", "Loaded");

            //ConventionManager.AddElementConvention<LongListSelector>(LongListSelector.ItemsSourceProperty, "SelectedItem", "SelectionChanged")
            //    .ApplyBinding =
            //   (viewModelType, path, property, element, convention) =>
            //   {
            //       if (!ConventionManager.SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, LongListSelector.ItemsSourceProperty))
            //           return false;
            //       ConventionManager.ConfigureSelectedItem(element, LongListSelector.SelectedItemProperty, viewModelType, path);
            //       return true;
            //   };
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
